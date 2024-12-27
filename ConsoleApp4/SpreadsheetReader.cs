using ConsoleApp4.Cells;

namespace ConsoleApp4;



public class SpreadsheetReader
{
    private readonly Dictionary<string, ICell> _cellsByAddress;
    private readonly int columns;
    private List<string[]> rawData;
    private readonly CellTypeFactory _cellTypeFactory = new CellTypeFactory();
    
    public SpreadsheetReader(int columns)
    {
        this.columns = columns;
        _cellsByAddress = new Dictionary<string, ICell>();
        rawData = new List<string[]>();
    }
    
    public void ReadData()
    {
        string input;
        while (true)
        {
            Console.WriteLine($"Enter row {rawData.Count + 1} values separated by space:");
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
            {
                if (rawData.Count > 0) break;
                continue;
            }

            string[] values = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (values.Length != columns)
            {
                Console.WriteLine($"Error: Expected {columns} values, got {values.Length}");
                continue;
            }

            rawData.Add(values);
            Console.Clear();
            DisplayRawData();
        }

        InitializeCells();
    }
    
 // public void DisplayTable1()
 //    {
 //        int rows = rawData.Count;
 //        for (int i = 0; i < rows; i++)
 //        {
 //            for (int j = 0; j < columns; j++)
 //            {
 //                string address = GetCellAddress(i, j);
 //                var cell = _cellsByAddress[address];
 //                string displayValue = cell.GetType() == _cellTypeFactory.GetCellType(CellType.String) 
 //                    ? $"\"{cell.GetValue()}\""
 //                    : cell.GetValue()?.ToString() ?? "";
 //                Console.Write($"{displayValue}\t");
 //            }
 //            Console.WriteLine();
 //        }
 //    }
 public void DisplayProcessedData()
 {
     int rows = rawData.Count;

     if (rows == 0)
     {
         Console.WriteLine("No data to display.");
         return;
     }

     // Формування заголовків таблиці
     for (int i = 0; i < columns; i++)
     {
         char columnName = (char)('A' + i); // A, B, C, ...
         Console.Write($"{columnName,-15}"); // Виводимо заголовок стовпця з вирівнюванням
     }
     Console.WriteLine();

     // Вивід даних таблиці
     for (int i = 0; i < rows; i++)
     {
         for (int j = 0; j < columns; j++)
         {
             string address = GetCellAddress(i, j);
             var cell = _cellsByAddress[address];
             string displayValue = cell.GetType() == _cellTypeFactory.GetCellType(CellType.String) 
                 ? $"\"{cell.GetValue()}\""
                 : cell.GetValue()?.ToString() ?? "";
             
             Console.Write($"{displayValue,-15}");
         }
         Console.WriteLine();
     }
 }


    
    public void DisplayRawData()
    {
        if (rawData.Count == 0)
        {
            Console.WriteLine("No data to display.");
            return;
        }

        // Формування заголовків таблиці
        for (int i = 0; i < columns; i++)
        {
            char columnName = (char)('A' + i); // A, B, C, ...
            Console.Write($"{columnName,-15}");
        }
        Console.WriteLine();

        // Вивід даних таблиці
        foreach (var row in rawData)
        {
            foreach (var value in row)
            {
                Console.Write($"{value,-15}");
            }
            Console.WriteLine();
        }
    }
    
    private string GetCellAddress(int row, int col)
    {
        char colLetter = (char)('A' + col);
        return $"{colLetter}{row + 1}";
    }
    
    private void InitializeCells()
    {
        for (int i = 0; i < rawData.Count; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                string address = GetCellAddress(i, j);
                string value = rawData[i][j].Trim();

                ICell cell = CreateCell(address, value);
                _cellsByAddress[address] = cell;
            }
        }
    }
    
    private ICell CreateCell(string address, string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return new EmptyCell(address);

        if (value.StartsWith("\"") && value.EndsWith("\""))
            return new StringCell(address, value);

        if (value.StartsWith("="))
            return new FormulaCell(address, value, _cellsByAddress);

        if (double.TryParse(value, out _))
            return new NumberCell(address, value);

        throw new ArgumentException($"Invalid value format in cell {address}: {value}");
    }
    
    public void ProcessAllFormulas()
    {
        var formulaCells = _cellsByAddress.Values
            .Where(c => c.GetType() == _cellTypeFactory.GetCellType(CellType.Formula) )
            .Cast<FormulaCell>();

        foreach (var cell in formulaCells)
        {
            if (!cell.IsProcessed)
            {
                try
                {
                    cell.ProcessFormula();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {cell.Address}: {ex.Message}");
                }
            }
        }
        Console.WriteLine("\nProcessed spreadsheet:");
        DisplayProcessedData();
    }
}