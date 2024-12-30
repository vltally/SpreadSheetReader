namespace ConsoleApp4.Cells;

public class NumberCell : BaseCell
{
    private double Value { get; set; }

    public NumberCell(string address, string value) : base(address)
    {
        SetValue(value);
        IsProcessed = true;
    }

    public override object GetValue() => Value;

    public override void SetValue(string value)
    {
        RawValue = value;
        if (!double.TryParse(value, out double result))
        {
            ErrorCode = 4;
            return;
        }
        Value = result;
    }
}