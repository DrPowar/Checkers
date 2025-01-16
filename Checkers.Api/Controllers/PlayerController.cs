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
        public async Task<IActionResult> CreatePlayer([FromBody] CreateaPlayerCommand createPlayerResponse)
        {
            Player player = await _mediator.Send(createPlayerResponse);

            return Ok(player);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignPlayerToGame([FromBody] AssignPlayerCommand assignRequest)
        {
            bool result = await _mediator.Send(assignRequest);

            return Ok(result);
        }
    }
}
