using Spotacard.Domain;

namespace Spotacard.Features.Apps.Types
{
    public class AppEnvelope
    {
        public App App { get; }

        public AppEnvelope(App app)
        {
            App = app;
        }
    }
}
