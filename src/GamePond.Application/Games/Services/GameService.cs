using FluentValidation;
using GamePond.Application.Games.DTOs;
using GamePond.Application.Games.Models;
using GamePond.Application.Games.Repositories;
using GamePond.Application.Games.Validators;
using GamePond.Domain.Games;
using Microsoft.Extensions.Logging;

namespace GamePond.Application.Games.Services;

public class GameService(
    IGameRepository gameRepository,
    CreateGameCommandValidator createGameCommandValidator,
    UpdateGameCommandValidator updateGameCommandValidator,
    ILogger<GameService> logger)
    : IGameService
{
    public async Task<IReadOnlyCollection<GameDto>> GetAllAsync()
    {
        var games =  await gameRepository.GetAllAsync();
        return games.Select(Map).ToList();
    }

    public async Task<GameDto?> GetByIdAsync(Guid id)
    {
        var game = await gameRepository.GetByIdAsync(id);
        
        if (game is null)
        {
            logger.LogError("The game with id {GameId} was not found.", id);
            return null;
        }

        logger.LogInformation("The game with id {GameId} was found.", id);
        return Map(game);
    }

    public async Task<GameDto> CreateAsync(CreateGameCommand command)
    {
        await createGameCommandValidator.ValidateAndThrowAsync(command);
        
        var normalizedTitle = command.Title.Trim();

        if (await gameRepository.TitleExistsAsync(normalizedTitle))
        {
            logger.LogError("The title {GameTitle} already exists.", normalizedTitle);
            throw new InvalidOperationException(
                $"Game with title '{normalizedTitle}' already exists.");
        }
        
        var game = new Game
        {
            Id = Guid.NewGuid(),
            Title = normalizedTitle,
            Description = command.Description?.Trim(),
            ReleaseDate = command.ReleaseDate,
            CreationDate = DateTimeOffset.UtcNow
        };
        
        var added = await gameRepository.AddAsync(game);

        if (!added)
        {
            logger.LogError("The game could not be added.");
            throw new InvalidOperationException(
                "The game could not be added.");
        }
        
        logger.LogInformation("The game with id {GameId} was created.", game.Id);

        return Map(game);
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateGameCommand command)
    {
        await updateGameCommandValidator.ValidateAndThrowAsync(command);
        
        var game = await gameRepository.GetByIdAsync(id);

        if (game is null)
        {
            logger.LogError("The game with {GameId} could not be found.", id);
            return false;
        }

        game.Title = command.Title.Trim();
        game.Description = command.Description?.Trim();
        game.ReleaseDate = command.ReleaseDate;

        return await gameRepository.UpdateAsync(game);
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        var result = gameRepository.DeleteAsync(id);
        if (result.Result)
        {
            logger.LogInformation("The game with id {GameId} was deleted.", id);
            return result;
        }
        
        logger.LogError("The game with id {GameId} was not found.", id);
        return result;
    }
    
    private static GameDto Map(Game game)
    {
        return new GameDto(
            game.Id,
            game.Title,
            game.Description,
            game.ReleaseDate,
            game.CreationDate);
    }
}