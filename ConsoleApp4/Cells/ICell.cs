namespace ConsoleApp4.Cells;

public interface ICell
{
    string Address { get; }
    string RawValue { get; }
    object GetValue();
    void SetValue(string value);
   // CellType GetType();
    bool IsProcessed { get; set; }
}

