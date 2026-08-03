using System.Collections.Concurrent;
using GamePond.Application.Games.Repositories;
using GamePond.Domain.Games;

namespace GamePond.Infrastructure.Games.Repositories;

public sealed class InMemoryGameRepository : IGameRepository
{
    private readonly ConcurrentDictionary<Guid, Game> _games = new();

    public Task<IReadOnlyCollection<Game>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyCollection<Game>>(_games.Values.ToList());
    }

    public Task<Game?> GetByIdAsync(Guid id)
    {
        _games.TryGetValue(id, out var game);
        return Task.FromResult(game);
    }

    public Task<bool> AddAsync(Game game)
    {
        bool added = _games.TryAdd(game.Id, game);
        return Task.FromResult(added);
    }

    public Task<bool> UpdateAsync(Game game)
    {
        if (!_games.TryGetValue(game.Id, out var existing))
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(
            _games.TryUpdate(game.Id, game, existing));
    }

    public Task<bool> DeleteAsync(Guid id)
    {
        bool res = _games.TryRemove(id, out _);
        return Task.FromResult(res);
    }

    public Task<bool> TitleExistsAsync(string title)
    {
        var normalizedTitle = title.Trim();

        return Task.FromResult(_games.Values.Any(g =>
            string.Equals(
                g.Title,
                normalizedTitle,
                StringComparison.OrdinalIgnoreCase)));
    }
}