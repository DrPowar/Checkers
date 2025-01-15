using Checkers.Application.Mediator.Boards.Command;
using Checkers.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Checkers.Api.Controllers
{
    [ApiController]
    [Route("api/games")]
    public class GameController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GameController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> InitializeBoard()
        {
            List<Piece> board = await _mediator.Send(new InitializeBoardCommand());

            return Ok();
        }
    }
}
