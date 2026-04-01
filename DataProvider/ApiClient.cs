using System.Text.Json;
using DataProvider.Interfaces;
using DataProvider.Models;

namespace DataProvider;

public class ApiClient
{
    private HttpClient Client { get; set; }
    private IProductFactory ProductFactory { get; set; }

    private static readonly JsonSerializerOptions JsonOptionsWeb = new JsonSerializerOptions(JsonSerializerDefaults.Web);


    public ApiClient(HttpClient httpClient, IProductFactory factory)
    {
        Client = httpClient;
        ProductFactory = factory;
    }

    private async Task<HttpResponseMessage> GetPageAsync(StoreGuid silpoStore, int pageSize = 100, int offset = 0)
    {
        string url = $"https://sf-ecom-api.silpo.ua/v1/uk/branches/{silpoStore}/products?limit={pageSize}&offset={offset}&includeChildCategories=true&sortBy=popularity&sortDirection=desc&mustHavePromotion=true";
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Add("authority", "sf-ecom-api.silpo.ua");
        request.Headers.Add("accept", "application/json");
        request.Headers.Add("accept-language", "en-US,en;q=0.9,ru;q=0.8,uk;q=0.7");
        request.Headers.Add("dnt", "1");
        request.Headers.Add("origin", "https://silpo.ua");
        request.Headers.Add("referer", "https://silpo.ua/");
        request.Headers.Add("sec-ch-ua", "\"Not(A:Brand\";v=\"24\", \"Chromium\";v=\"122\"");
        request.Headers.Add("sec-ch-ua-mobile", "?0");
        request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
        request.Headers.Add("sec-fetch-dest", "empty");
        request.Headers.Add("sec-fetch-mode", "cors");
        request.Headers.Add("sec-fetch-site", "same-site");
        request.Headers.Add("user-agent",
            "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36");

        var response = await Client.SendAsync(request);
        response.EnsureSuccessStatusCode();

        return response;
    }
    
    public async Task<ICollection<Product>> LoadAllPromotedProducts(StoreGuid storeId)
    {
        var products = new List<Product>();

        try
        {
            // Get first page
            var page = await GetPageAsync(storeId);
            var content = await page.Content.ReadAsStringAsync();
            var root = GetProductsFromPage(content);
            products.AddRange(root.products);
            // Get remaining pages
            for (int offset = 100; offset < root.total; offset += 100)
            {
                await Task.Delay(120);
                try
                {
                    page = await GetPageAsync(storeId,100, offset);
                    content = await page.Content.ReadAsStringAsync();
                    var nextRoot = GetProductsFromPage(content);
                    
                    products.AddRange(nextRoot.products);
                }
                catch (HttpRequestException ex)
                {
                    throw new HttpRequestException($"Network error at offset {offset}", ex);
                }
                catch (JsonException ex)
                {
                    throw new InvalidDataException($"Invalid JSON at offset {offset}", ex);
                }
                
            }
        }
        catch (HttpRequestException ex)
        {
            throw new HttpRequestException("Failed to connect to API", ex);
        }
        catch (JsonException ex)
        {
            throw new InvalidDataException("Invalid JSON response", ex);
        }

        return products;
    }

    private (IEnumerable<Product> products, int total) GetProductsFromPage(string json)
    {
        var page = JsonSerializer.Deserialize<PageRoot>(json, JsonOptionsWeb);
        if (page == null) 
            throw new InvalidOperationException($"Failed to deserialize page");
        if (page.Total == 0) 
            throw new InvalidOperationException("API returned 0 products");
        
        return (page.Items.Select(p => ProductFactory.Create(p)), page.Total);
    }
}
    