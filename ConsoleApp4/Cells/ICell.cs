namespace ConsoleApp4.Cells;

public interface ICell
{
    string Address { get; }
    string RawValue { get; }
    object GetValue();
    void SetValue(string value);
    bool IsProcessed { get; set; }
    bool HasError { get; }
    int? ErrorCode { get; }
}

