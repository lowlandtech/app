using System;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Features.Layouts.Commands;
using Spotacard.Features.Layouts.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Layouts.Contracts
{
    public interface ILayoutBuilder
    {
        ILayoutBuilder UseCreate(Create.LayoutData data);
        ILayoutBuilder UseEdit(Edit.LayoutData data, Guid layoutId);
        ILayoutBuilder UseUser(ICurrentUser currentUser);
        Task<LayoutEnvelope> BuildAsync(CancellationToken cancellationToken);
    }
}
