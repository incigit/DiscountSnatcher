using Program.Interfaces;
using Program.Models;

namespace Program;

public class ProductFactory : IProductFactory
{
    public Product Create(JsonProductEntry entry)
    {
        var data = new GeneralProductData()
        {
            Title = entry.Title ?? throw new ArgumentNullException(nameof(entry.Title)),
            PriceForAmount = entry.DisplayRatio ?? "Інформація відсутня",
            Price = entry.Price ?? throw new ArgumentNullException(nameof(entry.Price)),
            LeftInStock = entry.Stock ?? throw new ArgumentNullException(nameof(entry.Stock)),
            Brand = entry.BrandTitle ?? "Інформація відсутня",
            Units = entry.Ratio ?? "",
            SectionSlug = entry.SectionSlug ?? throw new ArgumentNullException(nameof(entry.SectionSlug)),
            Weighted = entry.Weighted ?? throw new ArgumentNullException(nameof(entry.Weighted)),
        };

        if (entry.OldPrice is not null)
        {
            return new DiscountProduct(data, (double)entry.OldPrice);
        }
        if (entry.SpecialPrices is not null && entry.SpecialPrices.Count > 0){
            return new BatchDiscountProduct(data, entry.SpecialPrices);
        }

        return null;
    }
}