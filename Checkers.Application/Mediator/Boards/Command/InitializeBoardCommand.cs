using Checkers.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Application.Mediator.Boards.Command
{
    public record InitializeBoardCommand() : IRequest<List<Piece>>;
}
