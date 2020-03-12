using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotacard.Features.Widgets.Commands;
using Spotacard.Features.Widgets.Types;
using Spotacard.Infrastructure.Security;
using System;
using System.Threading.Tasks;

namespace Spotacard.Features.Widgets.Controllers
{
    [Route("widgets")]
    public class WidgetsController : Controller
    {
        private readonly IMediator _mediator;

        public WidgetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<WidgetsEnvelope> Get([FromQuery] string author, [FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await _mediator.Send(new List.Query(author, limit, offset));
        }

        [HttpGet("{id}")]
        public async Task<WidgetEnvelope> Get(Guid id)
        {
            return await _mediator.Send(new ListById.Query(id));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<WidgetEnvelope> Create([FromBody] Create.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<WidgetEnvelope> Edit(Guid id, [FromBody] Edit.Command command)
        {
            command.WidgetId = id;
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