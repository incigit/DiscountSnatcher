namespace DataProvider.Models;

public class PageRoot
{
    public int Limit { get; init; }
    public int Offset { get; init; }
    public int Total { get; init; }
    public required List<JsonProductEntry> Items { get; init; }
}

public class JsonProductEntry
{
    public string? Id { get; init; }
    public string? Title { get; init; }
    public string? Icon { get; init; }
    public double? Price { get; init; }
    public double? OldPrice { get; init; }
    public string? OfferId { get; init; }
    public string? Ratio { get; init; } // units measurement
    public string? SectionSlug { get; init; }
    public string? CompanyId { get; init; }
    public string? BranchId { get; init; }
    public string? DeliveryType { get; init; }
    public int? ExternalProductId { get; init; }
    public List<Promotion>? Promotions { get; init; }
    public List<SpecialPrice>? SpecialPrices { get; init; }
    public DateTime? CreatedAt { get; init; }
    public string? Slug { get; init; }
    public double? AddToBasketStep { get; init; }
    public double? Stock { get; init; }
    // seem to duplicate "price" field above
    public double? DisplayPrice { get; init; }
    public double? DisplayOldPrice { get; init; }
    public string? DisplayRatio { get; init; }
    public double? GuestProductRating { get; init; }
    public int? GuestProductRatingCount { get; init; }
    public string? ClassifierSapId { get; init; }
    public object? OriginType { get; init; }
    public string? BrandId { get; init; }
    public string? BrandTitle { get; init; }
    public bool? Weighted { get; init; }
    public bool? BlurForUnderAged { get; init; }
    public object? Modifier { get; init; }
    public List<object>? Promos { get; init; } // seems unused for now
    public object? UntappdRating  { get; init; }
}

public class Promotion
{
    public string? Id { get; init; }
    public string? IconPath { get; init; }
    public string? Type { get; init; }
    public int? ClassActivity { get; init; }
}

public class SpecialPrice
{
    public double? Price { get; init; }
    public double? Count { get; init; }
    public string? Type { get; init; }
}