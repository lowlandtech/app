using System.Threading.Tasks;
using Spotacard.Features.Cards;
using Spotacard.Infrastructure.Security;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Spotacard.Features.Favorites
{
    [Route("cards")]
    public class FavoritesController : Controller
    {
        private readonly IMediator _mediator;

        public FavoritesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{slug}/favorite")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<CardEnvelope> FavoriteAdd(string slug)
        {
            return await _mediator.Send(new Add.Command(slug));
        }

        [HttpDelete("{slug}/favorite")]
        [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
        public async Task<CardEnvelope> FavoriteDelete(string slug)
        {
            return await _mediator.Send(new Delete.Command(slug));
        }
    }
}