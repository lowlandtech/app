using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Relations.Commands;
using Spotacard.Features.Relations.Contracts;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Relations.Types
{
    internal class RelationBuilder : IRelationBuilder
    {
        private Relation _relation = new Relation();
        private readonly GraphContext _context;

        public RelationBuilder(GraphContext context)
        {
            _context = context;
        }

        public IRelationBuilder UseCreate(Create.RelationData data)
        {
            _relation.Name = data.Name;
            _relation.PkFieldId = data.PkFieldId;
            _relation.PkName = data.PkName;
            _relation.FkFieldId = data.FkFieldId;
            _relation.FkName = data.FkName;
            _context.Relations.Add(_relation);

            return this;
        }

        public IRelationBuilder UseEdit(Edit.RelationData data, Guid relationId)
        {
            _relation = _context.Relations
                .FirstOrDefault(relation => relation.Id == relationId);

            if (_relation == null)
                throw new RestException(HttpStatusCode.NotFound, new { Relation = Constants.NOT_FOUND });

            _relation.Name = data.Name ?? _relation.Name;
            _relation.PkFieldId = data.PkFieldId == _relation.PkFieldId ? _relation.PkFieldId : data.PkFieldId;
            _relation.PkName = data.PkName ?? _relation.PkName;
            _relation.FkFieldId = data.FkFieldId == _relation.FkFieldId ? _relation.FkFieldId : data.FkFieldId;
            _relation.FkName = data.FkName ?? _relation.FkName;

            return this;
        }

        public IRelationBuilder UseUser(ICurrentUser currentUser)
        {
            return this;
        }

        public async Task<RelationEnvelope> BuildAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            return new RelationEnvelope(await _context.Relations
                .Where(relation => relation.Id == _relation.Id)
                .FirstOrDefaultAsync(cancellationToken));
        }
    }
}
