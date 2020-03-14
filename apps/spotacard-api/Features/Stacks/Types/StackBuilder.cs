using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Stacks.Commands;
using Spotacard.Features.Stacks.Contracts;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Stacks.Types
{
    internal class StackBuilder : IStackBuilder
    {
        private Stack _stack = new Stack();
        private readonly GraphContext _context;

        public StackBuilder(GraphContext context)
        {
            _context = context;
        }

        public IStackBuilder UseCreate(Create.StackData data, Guid cardId)
        {
            _stack.Name = data.Name;
            _stack.ContentId = data.ContentId;
            _stack.CardId = cardId;

            _context.Stacks.Add(_stack);

            return this;
        }

        public IStackBuilder UseEdit(Edit.StackData data, Guid stackId)
        {
            _stack = _context.Stacks
                .FirstOrDefault(x => x.Id == stackId);

            if (_stack == null)
                throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });

            _stack.Name = data.Name ?? _stack.Name;
            _stack.ContentId = data.ContentId == _stack.ContentId ? _stack.ContentId : data.ContentId;

            return this;
        }

        public IStackBuilder UseUser(ICurrentUser currentUser)
        {

            return this;
        }

        public async Task<StackEnvelope> BuildAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            return new StackEnvelope(await _context.Stacks
                .Where(x => x.Id == _stack.Id)
                .FirstOrDefaultAsync(cancellationToken));
        }
    }
}
