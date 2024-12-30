namespace ConsoleApp4.Cells;

public abstract class BaseCell : ICell
{
    public string Address { get; }
    public string RawValue { get; protected set; }
    public bool IsProcessed { get; set; }
    public bool HasError => ErrorCode.HasValue;
    public int? ErrorCode { get; protected set; }
    
    protected BaseCell(string address)
    {
        Address = address;
        IsProcessed = false;
    }
    
    public abstract object GetValue();
    public abstract void SetValue(string value);
}