using DataProvider.Models;

namespace DataProvider
{
    abstract class Program
    {
        static readonly HttpClient HttpClient = new HttpClient();
        
        // ReSharper disable once ArrangeTypeMemberModifiers
        static async Task Main(string[] args)
        {
            SectionMapper.Initialize();
            var maidanClient = new ApiClient(HttpClient, new ProductFactory());
            var allProducts = await maidanClient.LoadAllPromotedProducts(StoreGuid.KyivskiMaidan);
            
            Console.WriteLine($"Products count in local collection: {allProducts.Count}");
            
            
            Console.ReadKey(true);
            
            SectionMapper.SaveMissingSections();
        }
    }
}