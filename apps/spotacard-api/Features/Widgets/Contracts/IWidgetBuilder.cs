using System;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Features.Widgets.Commands;
using Spotacard.Features.Widgets.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Widgets.Contracts
{
    public interface IWidgetBuilder
    {
        IWidgetBuilder UseCreate(Create.WidgetData data);
        IWidgetBuilder UseEdit(Edit.WidgetData data, Guid widgetId);
        IWidgetBuilder UseUser(ICurrentUser currentUser);
        Task<WidgetEnvelope> BuildAsync(CancellationToken cancellationToken);
    }
}
