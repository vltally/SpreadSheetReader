namespace ConsoleApp4.Cells;

public class CellTypeFactory
{
    private readonly Dictionary<CellType, Type> cellTypes = new Dictionary<CellType, Type>
    {
        { CellType.Formula, typeof(FormulaCell) },
        { CellType.String, typeof(StringCell) },
        { CellType.Number, typeof(NumberCell) },
        { CellType.Empty, typeof(EmptyCell) }
    };
    
    public Type GetCellType(CellType type)
    {
        if (cellTypes.TryGetValue(type, out Type cellType))
        {
            return cellType;
        }
        throw new ArgumentException("Unsupported CellType", nameof(type));
    }
}
