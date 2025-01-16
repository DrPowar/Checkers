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

namespace Checkers.Api
{
    public static class ServiceRegistrar
    {
        public static WebApplication RegisterServices(this WebApplicationBuilder builder)
        {
            // Add basic services
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Configure database
            builder.Services.AddDbContext<CheckersDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Register additional services
            builder = AddRepositories(builder);
            builder = AddServices(builder);
            builder = AddMediators(builder);

            return builder.Build();
        }

        private static WebApplicationBuilder AddRepositories(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IBaseRepository<Game>, BaseRepository<Game>>();
            builder.Services.AddTransient<IBaseRepository<Player>, BaseRepository<Player>>();

            return builder;
        }

        private static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
        {
            builder.Services.AddTransient<IBoardService, BoardService>();
            builder.Services.AddTransient<IGameService, GameService>();
            builder.Services.AddTransient<IPlayerService, PlayerService>();

            return builder;
        }

        private static WebApplicationBuilder AddMediators(WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(options =>
            {
                options.RegisterServicesFromAssemblies(
                    typeof(InitializeBoardHandler).Assembly,
                    typeof(GetBoardHandler).Assembly,
                    typeof(CreateGameHandler).Assembly,
                    typeof(EndGameHandler).Assembly,
                    typeof(GetGameByIdHandler).Assembly,
                    typeof(GetGameStatusHandler).Assembly,
                    typeof(StartGameHandler).Assembly,
                    typeof(AssignPlayerHandler).Assembly,
                    typeof(CreatePlayerHandler).Assembly,
                    typeof(GetPlayerByIdHander).Assembly
                );
            });

            return builder;
        }
    }
}
