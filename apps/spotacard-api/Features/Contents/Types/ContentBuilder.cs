using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Contents.Commands;
using Spotacard.Features.Contents.Contracts;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Spotacard.Migrations;

namespace Spotacard.Features.Contents.Types
{
    internal class ContentBuilder : IContentBuilder
    {
        private Content _content = new Content();
        private readonly GraphContext _context;

        public ContentBuilder(GraphContext context)
        {
            _context = context;
        }

        public IContentBuilder UseCreate(Create.ContentData data, Guid cardId)
        {
            _content.Name = data.Name;
            _content.Text = data.Text;
            _content.Data = data.Data;
            _content.Example = data.Example;
            _content.CardId = cardId;
            _content.Index = _context.Contents.Count(card=>card.Id == cardId);

            _context.Contents.Add(_content);

            return this;
        }

        public IContentBuilder UseEdit(Edit.ContentData data, Guid contentId)
        {
            _content = _context.Contents
                .FirstOrDefault(x => x.Id == contentId);

            if (_content == null)
                throw new RestException(HttpStatusCode.NotFound, new { Content = Constants.NOT_FOUND });

            _content.Name = data.Name ?? _content.Name;
            _content.Text = data.Text ?? _content.Text;
            _content.Data = data.Data ?? _content.Data;
            _content.Example = data.Name ?? _content.Example;
            _content.Index = data.Index == _content.Index ? _content.Index : data.Index;

            return this;
        }

        public IContentBuilder UseUser(ICurrentUser currentUser)
        {

            return this;
        }

        public async Task<ContentEnvelope> BuildAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            return new ContentEnvelope(await _context.Contents
                .Where(content => content.Id == _content.Id)
                .FirstOrDefaultAsync(cancellationToken));
        }
    }
}
