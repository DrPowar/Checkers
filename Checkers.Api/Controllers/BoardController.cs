using Checkers.Application.Mediator.Boards.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.Api.Controllers;

[ApiController]
[Route("api/games/{gameId}/board")]
public class BoardController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = 
        mediator ?? throw new ArgumentNullException(nameof(mediator));

    [HttpPost]
    [Route("move")]
    public async Task<IActionResult> MakeMove(
        [FromRoute] Guid gameId,
        [FromBody] MakeMoveCommand moveCommand)
    {
        MakeMoveCommand command = moveCommand with { GameId = gameId };
        bool isSuccess = await _mediator.Send(command);
        
        return Ok(isSuccess);
    }
}