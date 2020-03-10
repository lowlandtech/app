using System.IO;
using System.Text;
using RazorLight.Razor;

namespace Spotacard.Features.Templates.Types
{
    public class Template : RazorLightProjectItem
    {
        private readonly string _content;

        public Template(string key, string content)
        {
            Key = key;
            _content = content;
        }

        public override string Key { get; }

        public override bool Exists => _content != null;

        public override Stream Read()
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(_content));
        }
    }
}
