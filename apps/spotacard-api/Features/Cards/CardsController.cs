using System.Threading.Tasks;
using Spotacard.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Spotacard.Features.Cards
{
    [Route("cards")]
    public class CardsController : Controller
    {
        private readonly IMediator _mediator;

        public CardsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<CardsEnvelope> Get([FromQuery] string tag, [FromQuery] string author, [FromQuery] string favorited, [FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await _mediator.Send(new List.Query(tag, author, favorited, limit, offset));
        }

        [HttpGet("feed")]
        public async Task<CardsEnvelope> GetFeed([FromQuery] string tag, [FromQuery] string author, [FromQuery] string favorited, [FromQuery] int? limit, [FromQuery] int? offset)
        {
            return await _mediator.Send(new List.Query(tag, author, favorited, limit, offset)
            {
                IsFeed = true
            });
        }

        [HttpGet("{slug}")]
        public async Task<CardEnvelope> Get(string slug)
        {
            return await _mediator.Send(new Details.Query(slug));
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<CardEnvelope> Create([FromBody]Create.Command command)
        {
            return await _mediator.Send(command);
        }

        [HttpPut("{slug}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<CardEnvelope> Edit(string slug, [FromBody]Edit.Command command)
        {
            command.Slug = slug;
            return await _mediator.Send(command);
        }

        [HttpDelete("{slug}")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task Delete(string slug)
        {
            await _mediator.Send(new Delete.Command(slug));
        }
    }
}