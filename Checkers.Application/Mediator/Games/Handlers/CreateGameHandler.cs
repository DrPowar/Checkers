﻿using Checkers.Application.Mediator.Games.Commands;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Games.Handlers
{
    public class CreateGameHandler : IRequestHandler<CreateGameCommand, Game>
    {
        private readonly IGameService _gameService;
        private readonly IBoardService _boardService;

        public CreateGameHandler(IBoardService boardService, IGameService gameService)
        {
            _boardService = boardService;
            _gameService = gameService;
        }

        public async Task<Game> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            List<Piece> board = await _boardService.InitializeBoard();

            return await _gameService.CreateGame(board);
        }
    }
}
