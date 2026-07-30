namespace GamePond.Application.Games.Models;

public sealed record UpdateGameCommand(
    string Title,
    string? Description,
    DateOnly? ReleaseDate);