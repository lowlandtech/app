using System;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Features.Relations.Commands;
using Spotacard.Features.Relations.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Relations.Contracts
{
    public interface IRelationBuilder
    {
        IRelationBuilder UseCreate(Create.RelationData data);
        IRelationBuilder UseEdit(Edit.RelationData data, Guid relationId);
        IRelationBuilder UseUser(ICurrentUser currentUser);
        Task<RelationEnvelope> BuildAsync(CancellationToken cancellationToken);
    }
}
