using System.Collections.Concurrent;
using DataProvider.Models;

namespace DataProvider;

public class ProductCache
{
    private readonly ConcurrentDictionary<string, IReadOnlyList<Product>> _store = new();

    public IReadOnlyList<Product>? Get(StoreGuid storeId) =>
        _store.GetValueOrDefault(storeId.Value);

    public void Set(StoreGuid storeId, IEnumerable<Product> products) =>
        _store[storeId.Value] = products.ToList().AsReadOnly();
}