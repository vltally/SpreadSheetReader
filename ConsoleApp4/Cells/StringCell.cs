namespace ConsoleApp4.Cells;

public class StringCell : BaseCell
{
    private string Value { get; set; }

    public StringCell(string address, string value) : base(address)
    {
        SetValue(value);
        IsProcessed = true;
    }

    public override object GetValue() => Value;

    public override void SetValue(string value)
    {
        RawValue = value;
        Value = value.Trim('"');
    }
}
