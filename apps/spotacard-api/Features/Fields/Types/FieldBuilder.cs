using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Fields.Commands;
using Spotacard.Features.Fields.Contracts;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Fields.Types
{
    internal class FieldBuilder : IFieldBuilder
    {
        private Field _field = new Field();
        private readonly GraphContext _context;

        public FieldBuilder(GraphContext context)
        {
            _context = context;
        }

        public IFieldBuilder UseCreate(Create.FieldData data, Guid tableId)
        {
            _field.Name = data.Name;
            _field.TableId = tableId;

            _context.Fields.Add(_field);

            return this;
        }

        public IFieldBuilder UseEdit(Edit.FieldData data, Guid fieldId)
        {
            _field = _context.Fields
                .FirstOrDefault(x => x.Id == fieldId);

            if (_field == null)
                throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });

            _field.Name = data.Name ?? _field.Name;

            return this;
        }

        public IFieldBuilder UseUser(ICurrentUser currentUser)
        {

            return this;
        }

        public async Task<FieldEnvelope> BuildAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            return new FieldEnvelope(await _context.Fields
                .Where(x => x.Id == _field.Id)
                .FirstOrDefaultAsync(cancellationToken));
        }
    }
}
