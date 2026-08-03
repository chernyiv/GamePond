using GamePond.Domain.Games;

namespace GamePond.Application.Games.Repositories;

public interface IGameRepository
{
    Task<IReadOnlyCollection<Game>> GetAllAsync();
    Task<Game?> GetByIdAsync(Guid id);
    Task<bool> AddAsync(Game game);
    Task<bool> UpdateAsync(Game game);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> TitleExistsAsync(string title);
}