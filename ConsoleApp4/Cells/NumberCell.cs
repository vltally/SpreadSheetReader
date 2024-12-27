namespace ConsoleApp4.Cells;

public class NumberCell : ICell
{
    public string Address { get; }
    public string RawValue { get; private set; }
    public double Value { get; private set; }
    public bool IsProcessed { get; set; }

    public NumberCell(string address, string value)
    {
        Address = address;
        SetValue(value);
        IsProcessed = true;
    }

    public object GetValue() => Value;

    public void SetValue(string value)
    {
        RawValue = value;
        if (!double.TryParse(value, out double result))
            throw new ArgumentException($"Invalid number format in cell {Address}");
        Value = result;
    }

    public new CellType GetType() => CellType.Number;
}