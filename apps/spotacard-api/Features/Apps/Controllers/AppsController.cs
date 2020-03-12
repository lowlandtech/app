using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotacard.Features.Apps.Commands;
using Spotacard.Features.Apps.Types;
using Spotacard.Infrastructure.Security;
using System;
using System.Threading.Tasks;

namespace Spotacard.Features.Apps.Controllers
{
    [Route("apps")]
    public class AppsController : Controller
    {
        private readonly IMediator _mediator;

        public AppsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<AppsEnvelope> Get([FromQuery] string author, [FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await _mediator.Send(new List.Query(author, limit, offset));
        }

        [HttpGet("{id}")]
        public async Task<AppEnvelope> Get(Guid id)
        {
            return await _mediator.Send(new ListById.Query(id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<AppEnvelope> Create([FromBody] Create.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<AppEnvelope> Edit(Guid id, [FromBody] Edit.Command command)
        {
            command.AppId = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task Delete(Guid id)
        {
            await _mediator.Send(new Delete.Command(id));
        }
    }
}
