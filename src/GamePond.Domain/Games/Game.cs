namespace GamePond.Domain.Games;

public sealed record Game()
{
    public required Guid Id { get; init; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public DateOnly? ReleaseDate { get; set; }
    public required DateTimeOffset CreationDate { get; init; }
}