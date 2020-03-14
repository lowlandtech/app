using System;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Features.Contents.Commands;
using Spotacard.Features.Contents.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Contents.Contracts
{
    public interface IContentBuilder
    {
        IContentBuilder UseCreate(Create.ContentData data, Guid cardId);
        IContentBuilder UseEdit(Edit.ContentData data, Guid contentId);
        IContentBuilder UseUser(ICurrentUser currentUser);
        Task<ContentEnvelope> BuildAsync(CancellationToken cancellationToken);
    }
}
