using GamePond.Application.Games.DTOs;
using GamePond.Application.Games.Models;
using GamePond.Application.Games.Services;
using Microsoft.AspNetCore.Mvc;

namespace GamePond.Api.Controllers;

[ApiController]
[Route("api/games")]
public sealed class GamesController(IGameService gameService) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<IEnumerable<GameDto>>> GetAll()
    {
        var games = await gameService.GetAllAsync();
        return Ok(games);
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GameDto>> GetById(Guid id)
    {
        var game = await gameService.GetByIdAsync(id);

        if (game is null)
        {
            return NotFound();
        }

        return Ok(game);
    }

    [HttpPost]
    public async Task<ActionResult<GameDto>> CreateGame(
        CreateGameCommand command)
    {
        var game = await gameService.CreateAsync(command);

        return CreatedAtAction(
            nameof(GetById),
            new { id = game.Id },
            game);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateGame(
        Guid id,
        UpdateGameCommand command)
    {
        var updated = await gameService.UpdateAsync(id, command);

        if (!updated)
        {
            return NotFound();
        }

        return NoContent();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteGame(Guid id)
    {
        var deleted = await gameService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}