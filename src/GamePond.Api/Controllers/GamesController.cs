using GamePond.Application.Games.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamePond.Api.Controllers;

[ApiController]
[Route("api/games")]
public sealed class GamesController(IGameService gameService) : ControllerBase
{
    private readonly IGameService _gameService = gameService;
}