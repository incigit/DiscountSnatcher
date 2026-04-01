using System.Diagnostics.CodeAnalysis;

namespace DataProvider.Models;

public record DiscountProduct : Product
{
    public required double OldPrice { get; init; }
    
    [SetsRequiredMembers]
    public DiscountProduct(GeneralProductData data, double oldPrice) : base(data)
    {
        OldPrice = oldPrice;
    }
}