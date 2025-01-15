using Checkers.Application.Mediator.Games.Commands;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Application.Mediator.Games.Handlers
{
    internal class CreateGameHandler : IRequestHandler<CreateGameCommand, Game>
    {
        private readonly IGameService _gameService;
        private readonly IBoardService _boardService;

        public async Task<Game> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            List<Piece> board = _boardService.InitializeBoard();

            return await _gameService.CreateGame(board);
        }
    }
}
