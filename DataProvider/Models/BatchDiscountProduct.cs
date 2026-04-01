namespace DataProvider.Models;

public record BatchDiscountProduct(GeneralProductData data, List<SpecialPrice> offers) : Product(data)
{
    public List<SpecialPrice> Offers { get; init; } = offers;
}