using Checkers.Application.Mediator.Players.Commands;
using Checkers.Domain.DTOs;
using Checkers.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.Api.Controllers
{
    [ApiController]
    [Route("api/players")]
    public class PlayerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PlayerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePlayer([FromBody]string name)
        {
            Player player = await _mediator.Send(new CreateaPlayerCommand(name));

            return Ok(player);
        }

        public async Task<IActionResult> AssignPlayerToGame([FromBody]AssignPlayerToGameRequest assignRequest)
        {
            bool result = await _mediator.Send(new AssignPlayerCommand(assignRequest.PlayerId, assignRequest.GameId));

            return Ok(result);
        }
    }
}
