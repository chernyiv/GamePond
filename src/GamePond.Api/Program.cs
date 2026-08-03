using FluentValidation;
using GamePond.Application.Games.Models;
using GamePond.Application.Games.Repositories;
using GamePond.Application.Games.Services;
using GamePond.Application.Games.Validators;
using GamePond.Infrastructure.Games.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddValidatorsFromAssemblyContaining<CreateGameCommandValidator>();

builder.Services.AddSingleton<
    IGameRepository,
    InMemoryGameRepository>();

builder.Services.AddScoped<
    IGameService,
    GameService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();


