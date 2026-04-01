using Program.Models;

namespace Program.Interfaces;

public interface IProductFactory
{
    Product Create(JsonProductEntry entry);
}