using GamePond.Application.Games.DTOs;
using GamePond.Application.Games.Models;

namespace GamePond.Application.Games.Services;

public interface IGameService
{
    Task<IReadOnlyCollection<GameDto>> GetAllAsync();

    Task<GameDto?> GetByIdAsync(Guid id);

    Task<GameDto> CreateAsync(CreateGameCommand command);

    Task<bool> UpdateAsync(Guid id, UpdateGameCommand command);

    Task<bool> DeleteAsync(Guid id);
}