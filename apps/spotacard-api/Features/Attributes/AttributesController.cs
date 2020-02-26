using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spotacard.Infrastructure.Security;
using System;
using System.Threading.Tasks;

namespace Spotacard.Features.Attributes
{
  [Route("cards")]
  public class AttributesController : Controller
  {
    private readonly IMediator _mediator;

    public AttributesController(IMediator mediator)
    {
      _mediator = mediator;
    }

    [HttpPost("{cardId}/attributes")]
    [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public async Task<AttributeEnvelope> Create(Guid cardId, [FromBody] Create.Command command)
    {
      command.CardId = cardId;
      return await _mediator.Send(command);
    }

    [HttpGet("{cardId}/attributes")]
    public async Task<AttributesEnvelope> Get(Guid cardId)
    {
      return await _mediator.Send(new List.Query(cardId));
    }

    [HttpPut("{cardId}/attributes")]
    [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public async Task<AttributeEnvelope> Edit(Guid cardId, [FromBody]Edit.Command command)
    {
      command.CardId = cardId;
      return await _mediator.Send(command);
    }

    [HttpDelete("{cardId}/attributes/{id}")]
    [Authorize(AuthenticationSchemes = JwtIssuerOptions.Schemes)]
    public async Task Delete(Guid id)
    {
      await _mediator.Send(new Delete.Command(id));
    }
  }
}
