using Microsoft.Extensions.Hosting;

namespace DataProvider;

//vibe coding xd
public class ProductCacheRefreshService : BackgroundService
{
    private readonly ApiClient _apiClient;
    private readonly ProductCache _cache;
    private readonly IEnumerable<StoreGuid> _stores;
    private readonly TimeSpan _interval = TimeSpan.FromHours(6);

    public ProductCacheRefreshService(ApiClient apiClient, ProductCache cache, IEnumerable<StoreGuid> stores)
    {
        _apiClient = apiClient;
        _cache = cache;
        _stores = stores;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        // Eager fill on startup
        await RefreshAllAsync(stoppingToken);

        using var timer = new PeriodicTimer(_interval);
        while (await timer.WaitForNextTickAsync(stoppingToken))
        {
            await RefreshAllAsync(stoppingToken);
        }
    }

    private async Task RefreshAllAsync(CancellationToken ct)
    {
        foreach (var store in _stores)
        {
            try
            {
                var products = await _apiClient.LoadAllPromotedProducts(store);
                _cache.Set(store, products);
            }
            catch (Exception ex)
            {
                // Log, but don't crash the refresh loop — stale cache beats no cache
                Console.WriteLine($"Cache refresh failed for {store}: {ex.Message}");
            }
        }
    }
}