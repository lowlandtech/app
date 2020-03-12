using System;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Features.Cells.Commands;
using Spotacard.Features.Cells.Types;
using Spotacard.Infrastructure;

namespace Spotacard.Features.Cells.Contracts
{
    public interface ICellBuilder
    {
        ICellBuilder UseCreate(Create.CellData data, Guid tableId);
        ICellBuilder UseEdit(Edit.CellData data, Guid cellId);
        ICellBuilder UseUser(ICurrentUser currentUser);
        Task<CellEnvelope> BuildAsync(CancellationToken cancellationToken);
    }
}
