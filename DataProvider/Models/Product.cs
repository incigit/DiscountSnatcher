using System.Text.Json.Serialization;

namespace DataProvider.Models;

[JsonPolymorphic]
[JsonDerivedType(typeof(DiscountProduct), "discount")]
[JsonDerivedType(typeof(BatchDiscountProduct), "batch")]
public abstract record Product(GeneralProductData data)
{
    public GeneralProductData Data { get; init; } = data;
}