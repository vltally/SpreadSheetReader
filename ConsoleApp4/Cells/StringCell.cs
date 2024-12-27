namespace ConsoleApp4.Cells;

public class StringCell : ICell
{
    public string Address { get; }
    public string RawValue { get; private set; }
    public string Value { get; private set; }
    public bool IsProcessed { get; set; }

    public StringCell(string address, string value)
    {
        Address = address;
        SetValue(value);
        IsProcessed = true;
    }

    public object GetValue() => Value;

    public void SetValue(string value)
    {
        RawValue = value;
        if (!value.StartsWith("\"") || !value.EndsWith("\""))
            throw new ArgumentException($"Invalid string format in cell {Address}");
        Value = value.Trim('"');
    }

    public new CellType GetType() => CellType.String;
}