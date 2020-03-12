using System;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Features.Pages.Commands;
using Spotacard.Features.Pages.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Pages.Contracts
{
    public interface IPageBuilder
    {
        IPageBuilder UseCreate(Create.PageData data, Guid appId);
        IPageBuilder UseEdit(Edit.PageData data, Guid pageId);
        IPageBuilder UseUser(ICurrentUser currentUser);
        Task<PageEnvelope> BuildAsync(CancellationToken cancellationToken);
    }
}
