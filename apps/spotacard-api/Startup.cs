using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Spotacard.Core.Contracts;
using Spotacard.Extensions;
using Spotacard.Features.Graphs;
using Spotacard.Features.Profiles;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using Spotacard.Infrastructure.Security;
using System.Collections.Generic;
using System.Reflection;

namespace Spotacard
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Settings = new Settings(configuration, env);
        }

        public Settings Settings { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<ISettings>(Settings);
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(DBContextTransactionPipelineBehavior<,>));

            services.AddProvider(Settings);
            services.AddControllers()
                    .AddNewtonsoftJson(options =>
                        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                    );

            services.AddLocalization(x => x.ResourcesPath = "Resources");
            services.AddSwagger();

            services.AddCors();
            services.AddMvc(opt =>
            {
                opt.Conventions.Add(new GroupByApiRootConvention());
                opt.Filters.Add(typeof(ValidatorActionFilter));
                opt.EnableEndpointRouting = false;
            })
            .AddJsonOptions(opt => { opt.JsonSerializerOptions.IgnoreNullValues = true; })
            .AddFluentValidation(cfg => { cfg.RegisterValidatorsFromAssemblyContaining<Startup>(); });

            services.AddAutoMapper(GetType().Assembly);
            services.AddServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddSerilogLogging();
            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseCors(builder =>
                builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseMvc();

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });

            // Enable middleware to serve swagger-ui assets(HTML, JS, CSS etc.)
            app.UseSwaggerUI(x => { x.SwaggerEndpoint("/swagger/v1/swagger.json", "Spotacard API V1"); });
        }
    }
}
