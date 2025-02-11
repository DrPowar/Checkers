﻿using Checkers.Application.Mediator.Games.Commands;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Games.Handlers
{
    public class CreateGameHandler(IBoardService boardService, IGameService gameService)
        : IRequestHandler<CreateGameCommand, Game>
    {
        private readonly IGameService
            _gameService = gameService ?? throw new ArgumentNullException(nameof(gameService));

        private readonly IBoardService _boardService =
            boardService ?? throw new ArgumentNullException(nameof(boardService));

        public async Task<Game> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            List<Piece> board = await _boardService.InitializeBoard();

            return await _gameService.CreateGameAndSave(board);
        }
    }
}
