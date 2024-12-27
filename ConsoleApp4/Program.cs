
using ConsoleApp4.Lib.StringProcessor;

namespace ConsoleApp4;

public class Program
{
    public static void Main()
    {
        Console.WriteLine("Enter number of columns:");
        int columns = int.Parse(Console.ReadLine());

        SpreadsheetReader spreadsheet = new SpreadsheetReader(columns);
        spreadsheet.AddStringProcessor(new StringProcessor());
        spreadsheet.ReadData();
        spreadsheet.ProcessAllFormulas();
        
        
        
        
    }
}