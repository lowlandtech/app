using NUnit.Framework;
using RazorLight;
using System.IO;
using System.Threading.Tasks;
using Spotacard.Features.Templates.Artifacts;

namespace Spotacard.Features.Templates
{
    public class RazorTests
    {
        [Test]
        public async Task Create_Content()
        {
            var engine = new RazorLightEngineBuilder()
                // required to have a default RazorLightProject type, but not required to create a template from string.
                .UseEmbeddedResourcesProject(typeof(Program))
                .UseMemoryCachingProvider()
                .Build();

            var template = "Hello, @Model.Name";
            var model = new ViewModel() { Name = "John Doe" };

            var result = await engine.CompileRenderStringAsync("templateKey", template, model);
            Assert.That(result, Is.EqualTo("Hello, John Doe"));
        }

        [Test]
        public async Task Create_Content_Cached()
        {
            var engine = new RazorLightEngineBuilder()
                // required to have a default RazorLightProject type, but not required to create a template from string.
                .UseEmbeddedResourcesProject(typeof(Program))
                .UseMemoryCachingProvider()
                .Build();

            var template = "Hello, @Model.Name";
            var model = new ViewModel() { Name = "John Doe" };
            await engine.CompileRenderStringAsync("templateKey", template, model);

            var cacheResult = engine.Handler.Cache.RetrieveTemplate("templateKey");
            if (cacheResult.Success)
            {
                var result = await engine.RenderTemplateAsync(cacheResult.Template.TemplatePageFactory(), model);
                Assert.That(result, Is.EqualTo("Hello, John Doe"));
            }
        }

        [Test]
        public async Task Create_Content_From_File()
        {
            var root = Path.GetDirectoryName(typeof(ViewModel).Assembly.Location);
            var engine = new RazorLightEngineBuilder()
                .UseFileSystemProject(root)
                .UseMemoryCachingProvider()
                .Build();

            var result = await engine.CompileRenderAsync(Path.Combine(root,"Features/Templates/Artifacts/View.cshtml"), new { Name = "John Doe" });
            Assert.That(result, Is.EqualTo("Hello, John Doe"));
        }

        [Test]
        public async Task Create_Content_From_Database()
        {
            var fixture = new TestFixture(context => new TemplateData(context));
            try
            {
                var project = new Project(fixture.GetContext());
                var engine = new RazorLightEngineBuilder()
                    .UseProject(project)
                    .UseMemoryCachingProvider()
                    .Build();

                // For key as a GUID
                var result = await engine.CompileRenderAsync("6cc277d5-253e-48e0-8a9a-8fe3cae17e5b", new { Name = "John Doe" });
                Assert.That(result, Is.EqualTo("Hello, John Doe"));
            }
            finally
            {
                fixture.Dispose();
            }
        }
    }
}
