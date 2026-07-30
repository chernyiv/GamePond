namespace GamePond.Application.Games.Models;

public sealed record CreateGameCommand(
    string Title,
    string? Description,
    DateOnly? ReleaseDate);