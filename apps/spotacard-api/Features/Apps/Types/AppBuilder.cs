using Microsoft.EntityFrameworkCore;
using Spotacard.Domain;
using Spotacard.Features.Apps.Commands;
using Spotacard.Features.Apps.Contracts;
using Spotacard.Infrastructure;
using Spotacard.Infrastructure.Errors;
using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Spotacard.Features.Apps.Types
{
    internal class AppBuilder : IAppBuilder
    {
        private App _app = new App();
        private readonly GraphContext _context;

        public AppBuilder(GraphContext context)
        {
            _context = context;
        }

        public IAppBuilder UseCreate(Create.AppData data)
        {
            _app.Name = data.Name;
            _app.Namingspace = data.Namingspace;
            _app.Prefix = data.Prefix;
            _app.Organization = data.Organization;

            _context.Apps.Add(_app);

            return this;
        }

        public IAppBuilder UseEdit(Edit.AppData data, Guid appId)
        {
            _app = _context.Apps
                .FirstOrDefault(x => x.Id == appId);

            if (_app == null)
                throw new RestException(HttpStatusCode.NotFound, new { Card = Constants.NOT_FOUND });

            _app.Name = data.Name ?? _app.Name;
            _app.Namingspace = data.Namingspace ?? _app.Namingspace;
            _app.Prefix = data.Prefix ?? _app.Prefix;
            _app.Organization = data.Organization ?? _app.Organization;

            return this;
        }

        public IAppBuilder UseUser(ICurrentUser currentUser)
        {

            return this;
        }

        public async Task<AppEnvelope> BuildAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);

            return new AppEnvelope(await _context.Apps
                .Where(x => x.Id == _app.Id)
                .FirstOrDefaultAsync(cancellationToken));
        }
    }
}
