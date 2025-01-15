using Checkers.Application.Mediator.Boards.Query;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Models;
using MediatR;

namespace Checkers.Application.Mediator.Boards.Handlers
{
    internal class GetBoardHandler : IRequestHandler<GetBoardQuery, List<Piece>>
    {
        private readonly IBoardService _boardService;

        public Task<List<Piece>> Handle(GetBoardQuery request, CancellationToken cancellationToken)
        {
            return _boardService.GetBoard(request.GameId);
        }
    }
}
