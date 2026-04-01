namespace Program.Models;

public abstract record Product(GeneralProductData data)
{
    public GeneralProductData Data { get; init; } = data;
}