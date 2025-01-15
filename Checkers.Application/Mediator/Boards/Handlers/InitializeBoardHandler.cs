﻿using Checkers.Application.Mediator.Boards.Command;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Boards.Handlers
{
    internal class InitializeBoardHandler : IRequestHandler<InitializeBoardCommand, List<Piece>>
    {
        private readonly IBoardService _boardService;

        public async Task<List<Piece>> Handle(InitializeBoardCommand request, CancellationToken cancellationToken)
        {
            return await _boardService.InitializeBoard();
        }
    }
}
