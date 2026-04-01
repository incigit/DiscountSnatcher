namespace Program.Models;

public record GeneralProductData // composition helper class
{
    public required string Title { get; init; }

    public required double Price { get; init; }

    public required string Units { get; init; }

    public required string SectionSlug { get; init; }

    public required double LeftInStock { get; init; }

    public required string PriceForAmount { get; init; } // renamed from DisplayRatio

    public required string Brand { get; init; } // renamed from BrandTitle

    public required bool Weighted { get; init; }
}