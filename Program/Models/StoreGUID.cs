namespace Program;

public class StoreGuid
{
    public string Value { get; private set; }

    private StoreGuid(string value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value;
    }
    
    public static StoreGuid KyivskiMaidan => new("1edb733a-a496-6a36-b584-29c9aae16dcc");
}