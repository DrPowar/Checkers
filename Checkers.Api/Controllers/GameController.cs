using Checkers.Application.Mediator.Games.Commands;
using Checkers.Application.Mediator.Games.Queries;
using Checkers.Domain.Enums;
using Checkers.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.Api.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        [HttpPost]
        public async Task<IActionResult> CreateGame()
        {
            Game game = await _mediator.Send(new CreateGameCommand());

            return CreatedAtRoute("GetGame", new { id = game.Id }, game);
        }

        [HttpGet("{gameId}", Name = "GetGame")]
        public async Task<IActionResult> GetGame([FromRoute] Guid gameId)
        {
            GetGameByIdQuery query = new GetGameByIdQuery(gameId);
            Game? game = await _mediator.Send(query);
            
            return Ok(game);
        }

        [HttpPost("{gameId}")]
        public async Task<IActionResult> StartGame([FromRoute] Guid gameId)
        {
            StartGameCommand command = new StartGameCommand(gameId);
            GameStatus gameStatus = await _mediator.Send(command);
            
            return Ok(gameStatus);
        }
    }
}
