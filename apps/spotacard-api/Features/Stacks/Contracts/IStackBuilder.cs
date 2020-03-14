using System;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Features.Stacks.Commands;
using Spotacard.Features.Stacks.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Stacks.Contracts
{
    public interface IStackBuilder
    {
        IStackBuilder UseCreate(Create.StackData data, Guid cardId);
        IStackBuilder UseEdit(Edit.StackData data, Guid stackId);
        IStackBuilder UseUser(ICurrentUser currentUser);
        Task<StackEnvelope> BuildAsync(CancellationToken cancellationToken);
    }
}
