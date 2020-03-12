using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Cells.Commands;
using Spotacard.Features.Cells.Contracts;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Cells.Types
{
    internal class CellBuilder : ICellBuilder
    {
        private Cell _cell = new Cell();
        private readonly GraphContext _context;

        public CellBuilder(GraphContext context)
        {
            _context = context;
        }

        public ICellBuilder UseCreate(Create.CellData data, Guid tableId)
        {
            _cell.Name = data.Name;
            _cell.Area = data.Area;
            _cell.PageId = tableId;

            _context.Cells.Add(_cell);

            return this;
        }

        public ICellBuilder UseEdit(Edit.CellData data, Guid cellId)
        {
            _cell = _context.Cells
                .FirstOrDefault(x => x.Id == cellId);

            if (_cell == null)
                throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });

            _cell.Name = data.Name ?? _cell.Name;
            _cell.Area = data.Area ?? _cell.Area;

            return this;
        }

        public ICellBuilder UseUser(ICurrentUser currentUser)
        {

            return this;
        }

        public async Task<CellEnvelope> BuildAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            return new CellEnvelope(await _context.Cells
                .Where(x => x.Id == _cell.Id)
                .FirstOrDefaultAsync(cancellationToken));
        }
    }
}
