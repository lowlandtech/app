using NUnit.Framework;
using RazorLight;
using Spotacard.Domain;
using Spotacard.Features.Templates.Actions;
using Spotacard.Features.Templates.Artifacts;
using Spotacard.Features.Templates.Types;
using Spotacard.TestCases;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Spotacard.Features.Templates
{
    public class TemplateTests
    {
        [Test]
        public async Task Generate_Result_With_Command()
        {
            var fixture = new TestFixture(context => new TestCase80(context));
            try
            {
                var command = new Generate.Command
                {
                    Data = new Generate.GenerateData
                    {
                        AppId = new Guid("bee3223a-276d-40a5-ad25-d8ac3967132e"),
                        TemplateId = new Guid("6cc277d5-253e-48e0-8a9a-8fe3cae17e5b")
                    }
                };

                // For key as a GUID
                var result = await TemplateHelper.Generate(fixture, command);

                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Not.Null.Or.Empty);
                Assert.That(result.Files.First().Name, Is.EqualTo("d://repositories//tmp//John Doe"));
                Assert.That(await result.Files.First().GetContent(), Is.EqualTo("Hello, John Doe"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Create_Card_Description_With_Card_Model()
        {
            var fixture = new TestFixture(context => new TestCase80(context));
            try
            {
                var project = new Project(fixture.GetContext());
                var engine = new RazorLightEngineBuilder()
                    .UseProject(project)
                    .UseMemoryCachingProvider()
                    .Build();

                // For key as a GUID
                var result = await engine.CompileRenderAsync("6cc277d5-253e-48e0-8a9a-8fe3cae17e5b", new Card { Title = "John Doe" });
                Assert.That(result, Is.EqualTo("Hello, John Doe"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Create_Card_Description_From_Database()
        {
            var fixture = new TestFixture(context => new TestCase80(context));
            try
            {
                var project = new Project(fixture.GetContext());
                var engine = new RazorLightEngineBuilder()
                    .UseProject(project)
                    .UseMemoryCachingProvider()
                    .Build();

                // For key as a GUID
                var result = await engine.CompileRenderAsync("6cc277d5-253e-48e0-8a9a-8fe3cae17e5b", new { Title = "John Doe" });
                Assert.That(result, Is.EqualTo("Hello, John Doe"));
            }
            finally
            {
                fixture.Dispose();
            }
        }

        [Test]
        public async Task Create_Card_Description()
        {
            var engine = new RazorLightEngineBuilder()
                // required to have a default RazorLightProject type, but not required to create a template from string.
                .UseEmbeddedResourcesProject(typeof(Program))
                .UseMemoryCachingProvider()
                .Build();

            var template = "Hello, @Model.Name";
            var model = new ViewModel { Name = "John Doe" };

            var result = await engine.CompileRenderStringAsync("templateKey", template, model);
            Assert.That(result, Is.EqualTo("Hello, John Doe"));
        }

        [Test]
        public async Task Create_Card_Description_Cached()
        {
            var engine = new RazorLightEngineBuilder()
                // required to have a default RazorLightProject type, but not required to create a template from string.
                .UseEmbeddedResourcesProject(typeof(Program))
                .UseMemoryCachingProvider()
                .Build();

            var template = "Hello, @Model.Name";
            var model = new ViewModel { Name = "John Doe" };
            await engine.CompileRenderStringAsync("templateKey", template, model);

            var cacheResult = engine.Handler.Cache.RetrieveTemplate("templateKey");
            if (cacheResult.Success)
            {
                var result = await engine.RenderTemplateAsync(cacheResult.Template.TemplatePageFactory(), model);
                Assert.That(result, Is.EqualTo("Hello, John Doe"));
            }
        }

        [Test]
        public async Task Create_Card_Description_From_File()
        {
            var root = Path.GetDirectoryName(typeof(ViewModel).Assembly.Location);
            var engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(root)
                .UseMemoryCachingProvider()
                .Build();

            var result = await engine.CompileRenderAsync(Path.Combine(root,"Features/Templates/Artifacts/View.cshtml"), new { Name = "John Doe" });
            Assert.That(result, Is.EqualTo("Hello, John Doe"));
        }

    }
}
