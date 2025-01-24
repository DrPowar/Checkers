
using Checkers.Api;
using Checkers.Api.Hubs;
using Checkers.Api.Middleware;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

WebApplication app = builder.ConfigurWebAppBuilder();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<GameHub>("/gamehub");

app.UseCors("AllowFrontend");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
