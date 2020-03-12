using System;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Features.Apps.Commands;
using Spotacard.Features.Apps.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Apps.Contracts
{
    public interface IAppBuilder
    {
        IAppBuilder UseCreate(Create.AppData data);
        IAppBuilder UseEdit(Edit.AppData data, Guid appId);
        IAppBuilder UseUser(ICurrentUser currentUser);
        Task<AppEnvelope> BuildAsync(CancellationToken cancellationToken);
    }
}
