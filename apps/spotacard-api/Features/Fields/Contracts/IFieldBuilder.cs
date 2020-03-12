using System;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Features.Fields.Commands;
using Spotacard.Features.Fields.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Fields.Contracts
{
    public interface IFieldBuilder
    {
        IFieldBuilder UseCreate(Create.FieldData data, Guid tableId);
        IFieldBuilder UseEdit(Edit.FieldData data, Guid fieldId);
        IFieldBuilder UseUser(ICurrentUser currentUser);
        Task<FieldEnvelope> BuildAsync(CancellationToken cancellationToken);
    }
}
