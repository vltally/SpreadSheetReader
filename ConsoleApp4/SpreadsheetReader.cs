using ConsoleApp4.Cells;
using ConsoleApp4.Lib.StringProcessor;

namespace ConsoleApp4;



public class SpreadsheetReader
{
    private StringProcessor _stringProcessor;
    private readonly Dictionary<string, ICell> _cellsByAddress;
    private readonly int columns;
    private List<string[]> rawData;
    private readonly HashSet<int> encounteredErrors;
    
    public SpreadsheetReader(int columns)
    {
        this.columns = columns;
        _cellsByAddress = new Dictionary<string, ICell>();
        rawData = new List<string[]>();
        encounteredErrors = new HashSet<int>();
    }

    public void AddStringProcessor(StringProcessor stringProcessor)
    {
        _stringProcessor = stringProcessor;
    }
    
    public void ReadData()
    {
        string input;
        while (true)
        {
            Console.WriteLine($"Enter row {rawData.Count + 1} values separated by space:");
            input = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(input))
                break;

            string[] values = input.Split('\t', StringSplitOptions.RemoveEmptyEntries);
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
    
    public void DisplayProcessedData()
 {
     Console.WriteLine("\nProcessed spreadsheet:");
     Console.WriteLine("---------------------------------");
     int rows = rawData.Count;

     if (rows == 0)
     {
         Console.WriteLine("No data to display.");
         return;
     }

     
     for (int i = 0; i < columns; i++)
     {
         char columnName = (char)('A' + i); // A, B, C, ...
         Console.Write($"{columnName,-15}"); }
     Console.WriteLine();

      
     for (int i = 0; i < rows; i++)
     {
         for (int j = 0; j < columns; j++)
         {
             string address = GetCellAddress(i, j);
             ICell cell = _cellsByAddress[address];
             string displayValue;

             if (cell.HasError)
             {
                 displayValue = ErrorCodes.FormatError(cell.ErrorCode.Value);
             }
             else if (cell is StringCell)
             {
                 displayValue = $"\"{cell.GetValue()}\"";
             }
             else
             {
                 displayValue = cell.GetValue()?.ToString() ?? "";
             }
        
             Console.Write($"{displayValue,-15}");
         }
         Console.WriteLine();
     }
     
     if (encounteredErrors.Any())
     {
         Console.WriteLine("\nEncountered errors:");
         foreach ((int code, string message) in ErrorCodes.GetActiveErrors(encounteredErrors))
         {
             Console.WriteLine($"Error#{code}: {message}");
         }
     }
     
 }
    
     public void DisplayIntermediateData()
 {
     Console.WriteLine("\nIntermediate data processing:");
     Console.WriteLine("---------------------------------");
   
     int rows = rawData.Count;
   
     // Заголовки
     for (int i = 0; i < columns; i++)
     {
         char columnName = (char)('A' + i);
         Console.Write($"{columnName,-15}");
     }
     Console.WriteLine();

     // Дані
     for (int i = 0; i < rows; i++)
     {
         for (int j = 0; j < columns; j++)
         {
             string address = GetCellAddress(i, j);
             ICell cell = _cellsByAddress[address];
             string displayValue;

             if (cell is FormulaCell)
             {
                 displayValue = ((FormulaCell)cell).GetProcessedFormula();
             }
             else if (cell is StringCell)
             {
                 displayValue = $"\"{cell.GetValue()}\"";
             }
             else
             {
                 displayValue = cell.GetValue()?.ToString() ?? "";
             }
           
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

         
        for (int i = 0; i < columns; i++)
        {
            char columnName = (char)('A' + i); // A, B, C, ...
            Console.Write($"{columnName,-15}");
        }
        Console.WriteLine();

         
        foreach (string[] row in rawData)
        {
            foreach (string value in row)
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
            return new StringCell(address, string.Empty);

        if (value.StartsWith("="))
            return new FormulaCell(address, value, _cellsByAddress);

         
        if (double.TryParse(value, out _))
            return new NumberCell(address, value);

         
        return new StringCell(address, value);
    }
    
    
    
    public void ProcessAllFormulas()
    {
        List<FormulaCell> formulaCells = _cellsByAddress.Values
            .OfType<FormulaCell>()
            .ToList();

        foreach (FormulaCell cell in formulaCells)
        {
            if (!cell.IsProcessed)
            {
                cell.ProcessFormula(encounteredErrors: encounteredErrors);
            }
        }
        
        DisplayIntermediateData();
        
        foreach (FormulaCell cell in formulaCells)
        {
            cell.EvaluateFormula(_stringProcessor, encounteredErrors);
        }
        
        DisplayProcessedData();
    }

  
}