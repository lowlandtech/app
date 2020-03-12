using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Tables.Commands;
using Spotacard.Features.Tables.Contracts;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Tables.Types
{
    internal class TableBuilder : ITableBuilder
    {
        private Table _table = new Table();
        private readonly GraphContext _context;

        public TableBuilder(GraphContext context)
        {
            _context = context;
        }

        public ITableBuilder UseCreate(Create.TableData data, Guid appId)
        {
            _table.Name = data.Name;
            _table.AppId = appId;

            _context.Tables.Add(_table);

            return this;
        }

        public ITableBuilder UseEdit(Edit.TableData data, Guid tableId)
        {
            _table = _context.Tables
                .FirstOrDefault(x => x.Id == tableId);

            if (_table == null)
                throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });

            _table.Name = data.Name ?? _table.Name;

            return this;
        }

        public ITableBuilder UseUser(ICurrentUser currentUser)
        {

            return this;
        }

        public async Task<TableEnvelope> BuildAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            return new TableEnvelope(await _context.Tables
                .Where(x => x.Id == _table.Id)
                .FirstOrDefaultAsync(cancellationToken));
        }
    }
}
