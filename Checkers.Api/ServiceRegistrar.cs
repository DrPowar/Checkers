using Checkers.Application.Mediator.Boards.Handlers;
using Checkers.Application.Mediator.Games.Handlers;
using Checkers.Application.Mediator.Players.Handers;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Interfaces.Repositories;
using Checkers.Domain.Models;
using Checkers.Infrastructure.Data;
using Checkers.Infrastructure.Repositories;
using Checkers.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using System.Reflection;

namespace Checkers.Api
{
    public static class ServiceRegistrar
    {
        public static WebApplication RegisterServices(this WebApplicationBuilder builder)
        {
            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddDbContext<CheckersDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


            builder.Services.AddSwaggerGen();

            builder.Services.TryAddTransient<IBaseRepository<Game>, BaseRepository<Game>>();
            builder.Services.TryAddTransient<IBoardService, BoardService>();


            builder.Services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(typeof(InitializeBoardHandler).Assembly);
                options.RegisterServicesFromAssemblies(typeof(GetBoardHandler).Assembly);
                options.RegisterServicesFromAssemblies(typeof(CreateGameHandler).Assembly);
                options.RegisterServicesFromAssemblies(typeof(EndGameHandler).Assembly);
                options.RegisterServicesFromAssemblies(typeof(GetGameByIdHandler).Assembly);
                options.RegisterServicesFromAssemblies(typeof(GetGameStatusHandler).Assembly);
                options.RegisterServicesFromAssemblies(typeof(StartGameHandler).Assembly);
                options.RegisterServicesFromAssemblies(typeof(AssignPlayerHandler).Assembly);
                options.RegisterServicesFromAssemblies(typeof(CreatePlayerHandler).Assembly);
                options.RegisterServicesFromAssemblies(typeof(GetPlayerByIdHander).Assembly);
            });

            return builder.Build();
        }
    }
}
