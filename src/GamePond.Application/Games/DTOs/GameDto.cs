namespace GamePond.Application.Games.DTOs;

public sealed record GameDto(
    Guid Id,
    string Title,
    string? Description,
    DateOnly? ReleaseDate,
    DateTimeOffset CreatedAt);