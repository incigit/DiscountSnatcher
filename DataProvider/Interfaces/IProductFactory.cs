using DataProvider.Models;

namespace DataProvider.Interfaces;

public interface IProductFactory
{
    Product Create(JsonProductEntry entry);
}