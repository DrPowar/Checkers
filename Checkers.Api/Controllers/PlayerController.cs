using Checkers.Application.Mediator.Players.Commands;
using Checkers.Application.Mediator.Players.Queries;
using Checkers.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.Api.Controllers
{
    [ApiController]
    [Route("api/players")]
    public class PlayerController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = 
            mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody] CreateaPlayerCommand createPlayerResponse)
        {
            Player player = await _mediator.Send(createPlayerResponse);

            return Ok(player);
        }

        [HttpGet("{playerId}")]
        public async Task<IActionResult> GetPlayer([FromRoute] Guid playerId)
        {
            GetPlayerByIdQuery query = new GetPlayerByIdQuery(playerId);
            Player player = await _mediator.Send(query);
            
            return Ok(player);
        }
    }
}
