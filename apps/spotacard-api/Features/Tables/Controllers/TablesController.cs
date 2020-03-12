using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotacard.Features.Tables.Commands;
using Spotacard.Features.Tables.Types;
using Spotacard.Infrastructure.Security;
using System;
using System.Threading.Tasks;

namespace Spotacard.Features.Tables.Controllers
{
    [Route("tables")]
    public class TablesController : Controller
    {
        private readonly IMediator _mediator;

        public TablesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<TablesEnvelope> Get([FromQuery] string author, [FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await _mediator.Send(new List.Query(author, limit, offset));
        }

        [HttpGet("{id}")]
        public async Task<TableEnvelope> Get(Guid id)
        {
            return await _mediator.Send(new ListById.Query(id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<TableEnvelope> Create([FromBody] Create.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<TableEnvelope> Edit(Guid id, [FromBody] Edit.Command command)
        {
            command.TableId = id;
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
