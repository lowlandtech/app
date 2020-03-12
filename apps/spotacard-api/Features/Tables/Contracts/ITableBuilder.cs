using System;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Features.Tables.Commands;
using Spotacard.Features.Tables.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Tables.Contracts
{
    public interface ITableBuilder
    {
        ITableBuilder UseCreate(Create.TableData data, Guid appId);
        ITableBuilder UseEdit(Edit.TableData data, Guid tableId);
        ITableBuilder UseUser(ICurrentUser currentUser);
        Task<TableEnvelope> BuildAsync(CancellationToken cancellationToken);
    }
}
