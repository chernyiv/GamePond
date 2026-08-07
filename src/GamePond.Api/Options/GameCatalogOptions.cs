using System.ComponentModel.DataAnnotations;

namespace GamePond.Api.Options;

public sealed class GameCatalogOptions
{
    public const string SectionName = "GameCatalog";
    
    [Range(1, 500)]
    public int MaximumTitleLength { get; set; } = 500;
    
    [Range(1, 10_000)]
    public int MaximumDescriptionLength { get; init; } = 2_000;
}