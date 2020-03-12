using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotacard.Features.Fields.Commands;
using Spotacard.Features.Fields.Types;
using Spotacard.Infrastructure.Security;
using System;
using System.Threading.Tasks;

namespace Spotacard.Features.Fields.Controllers
{
    [Route("fields")]
    public class FieldsController : Controller
    {
        private readonly IMediator _mediator;

        public FieldsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<FieldsEnvelope> Get([FromQuery] string author, [FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await _mediator.Send(new List.Query(author, limit, offset));
        }

        [HttpGet("{id}")]
        public async Task<FieldEnvelope> Get(Guid id)
        {
            return await _mediator.Send(new ListById.Query(id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<FieldEnvelope> Create([FromBody] Create.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<FieldEnvelope> Edit(Guid id, [FromBody] Edit.Command command)
        {
            command.FieldId = id;
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
