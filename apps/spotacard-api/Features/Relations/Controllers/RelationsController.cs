using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotacard.Features.Relations.Commands;
using Spotacard.Features.Relations.Types;
using Spotacard.Infrastructure.Security;
using System;
using System.Threading.Tasks;

namespace Spotacard.Features.Relations.Controllers
{
    [Route("relations")]
    public class RelationsController : Controller
    {
        private readonly IMediator _mediator;

        public RelationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<RelationsEnvelope> Get([FromQuery] string author, [FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await _mediator.Send(new List.Query(author, limit, offset));
        }

        [HttpGet("{id}")]
        public async Task<RelationEnvelope> Get(Guid id)
        {
            return await _mediator.Send(new ListById.Query(id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<RelationEnvelope> Create([FromBody] Create.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<RelationEnvelope> Edit(Guid id, [FromBody] Edit.Command command)
        {
            command.RelationId = id;
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
