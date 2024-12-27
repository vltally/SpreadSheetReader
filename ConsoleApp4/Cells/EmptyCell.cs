namespace ConsoleApp4.Cells;

public class EmptyCell : ICell
{
    public string Address { get; }
    public string RawValue => string.Empty;
    public bool IsProcessed { get; set; }

    public EmptyCell(string address)
    {
        Address = address;
        IsProcessed = true;
    }

    public object GetValue() => null;
    public void SetValue(string value) { }
    public new CellType GetType() => CellType.Empty;
}