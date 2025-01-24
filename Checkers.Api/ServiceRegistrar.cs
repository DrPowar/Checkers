using System.Reflection;
using Checkers.Api.Hubs;
using Checkers.Application.Behaviors;
using Checkers.Application.Mediator.Boards.Handlers;
using Checkers.Application.Mediator.Games.Handlers;
using Checkers.Application.Mediator.Players.Handers;
using Checkers.Application.Mediator.Players.Handlers;
using Checkers.Domain.Interfaces;
using Checkers.Domain.Interfaces.Repositories;
using Checkers.Domain.Models;
using Checkers.Infrastructure.Data;
using Checkers.Infrastructure.Repositories;
using Checkers.Infrastructure.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Checkers.Api
{
    public static class ServiceRegistrar
    {
        public static WebApplication ConfigurWebAppBuilder(this WebApplicationBuilder builder)
        {
            // Add basic services
            builder.Services.AddControllers();
            builder.Services.AddOpenApi();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(setupAction =>
            {
                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);
                
                setupAction.IncludeXmlComments(xmlCommentsFullPath);
            });

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins("http://localhost:3000")
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });


            // Configure database
            builder.Services.AddDbContext<CheckersDbContext>(options =>
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
                   sqlOptions => sqlOptions.MigrationsAssembly("Checkers.Infrastructure")));

            // Register additional services
            builder = AddRepositories(builder);
            builder = AddServices(builder);
            builder = AddMediators(builder);

            // Add Error Handling Behavior for MediatorR
            builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ErrorHandlingBehavior<,>));
            
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddSignalR();
            
            return builder.Build();
        }

        private static WebApplicationBuilder AddRepositories(WebApplicationBuilder builder)
        {
            builder.Services.TryAddScoped<IBaseRepository<Game>, GameRepository>();
            builder.Services.TryAddScoped<IBaseRepository<Player>, BaseRepository<Player>>();

            return builder;
        }

        private static WebApplicationBuilder AddServices(WebApplicationBuilder builder)
        {
            builder.Services.TryAddTransient<IBoardService, BoardService>();
            builder.Services.TryAddTransient<IPlayerService, PlayerService>();
            builder.Services.TryAddTransient<IGameService, GameService>();
            builder.Services.TryAddTransient<IGameEngineService, GameEngineService>();
            builder.Services.TryAddTransient<IMoveValidationService, MoveValidationService>();
            builder.Services.TryAddTransient<IStatusService, GameStatusService>();
            builder.Services.TryAddTransient<IGameHub, GameHub>();

            return builder;
        }

        private static WebApplicationBuilder AddMediators(WebApplicationBuilder builder)
        {
            builder.Services.AddMediatR(options =>
            {
                // Add Game mediators
                options.RegisterServicesFromAssemblies(
                    typeof(CreateGameHandler).Assembly,
                    typeof(GetGameByIdHandler).Assembly,
                    typeof(ChangeStatusHandler).Assembly,
                    typeof(GetGameStatusHandler).Assembly,
                    typeof(AssignPlayerToGameHandler).Assembly
                );
                
                // Add Player mediators
                options.RegisterServicesFromAssemblies(                    
                    typeof(CreatePlayerHandler).Assembly,
                    typeof(GetPlayerByIdHandler).Assembly
                );
                
                // Add Board mediators
                options.RegisterServicesFromAssemblies(
                    typeof(InitializeBoardHandler).Assembly,
                    typeof(GetBoardHandler).Assembly
                );
            });

            return builder;
        }
    }
}
