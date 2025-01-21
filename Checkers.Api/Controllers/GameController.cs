using Checkers.Application.Mediator.Games.Commands;
using Checkers.Application.Mediator.Games.Queries;
using Checkers.Domain.Enums;
using Checkers.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.Api.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = 
            mediator ?? throw new ArgumentNullException(nameof(mediator));

        /// <summary>
        /// Create a game
        /// </summary>
        /// <response code="200">Returns the created game</response>
        /// <returns>A new game</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateGame()
        {
            Game game = await _mediator.Send(new CreateGameCommand());

            return CreatedAtRoute("GetGame", new { gameId = game.Id }, game);
        }
        
        /// <summary>
        /// Get information about special game
        /// </summary>
        /// <param name="gameId">The identifier of the game</param>
        /// <response code="200">Returns the requested game</response>
        /// <returns>An information about game</returns>

        [HttpGet("{gameId}", Name = "GetGame")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetGame([FromRoute] Guid gameId)
        {
            GetGameByIdQuery query = new GetGameByIdQuery(gameId);
            Game? game = await _mediator.Send(query);
            
            return Ok(game);
        }

        /// <summary>
        /// Change game status
        /// </summary>
        /// <param name="gameId">The identifier of the game</param>
        /// <param name="changeStatusCommand">Game status</param>
        /// <returns>A new game status</returns>
        [HttpPut("{gameId}")]
        public async Task<IActionResult> ChangeGameStatus(
            [FromRoute] Guid gameId,
            [FromBody] ChangeStatusCommand changeStatusCommand)
        {
            ChangeStatusCommand command = changeStatusCommand with { GameId = gameId };
            GameStatus gameStatus = await _mediator.Send(command);
            
            return Ok(gameStatus);
        }
        
        /// <summary>
        /// Assign player to the existing game
        /// </summary>
        /// <param name="gameId">The identifier of the game</param>
        /// <param name="assignRequest">Request contains gameId and playerId</param>
        /// <returns></returns>
        [HttpPut("{gameId}/players")]
        public async Task<IActionResult> AssignPlayerToGame(
            [FromRoute] Guid gameId, 
            [FromBody] AssignPlayerToGameCommand assignRequest)
        {
            AssignPlayerToGameCommand command = assignRequest with { GameId = gameId };
            bool result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
