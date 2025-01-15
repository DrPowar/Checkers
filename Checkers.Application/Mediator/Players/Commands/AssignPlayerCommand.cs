using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkers.Application.Mediator.Players.Commands
{
    public record AssignPlayerCommand(Guid PlayerId, Guid GameId) : IRequest<bool>;
}
