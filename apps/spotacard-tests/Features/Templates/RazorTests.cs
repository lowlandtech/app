using NUnit.Framework;
using RazorLight;
using System.Threading.Tasks;

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
    }

    public class ViewModel
    {
        public string Name { get; set; }
    }
}
