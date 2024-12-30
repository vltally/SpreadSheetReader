using System.Text.RegularExpressions;
using ConsoleApp4.Lib.StringProcessor;

namespace ConsoleApp4.Cells;

public class FormulaCell : BaseCell
{
    private string processedFormula;
    private readonly Dictionary<string, ICell> cellsByAddress;

    public string GetProcessedFormula()
    {
        return processedFormula;
    }
    
    public FormulaCell(string address, string value, Dictionary<string, ICell> cellsByAddress) 
        : base(address)
    {
        this.cellsByAddress = cellsByAddress;
        SetValue(value);
    }

    public override object GetValue() => HasError ? ErrorCodes.FormatError(ErrorCode.Value) : processedFormula;

    public override void SetValue(string value)
    {
        if (!value.StartsWith("="))
        {
            ErrorCode = 4;
            return;
        }
        RawValue = value;
        IsProcessed = false;
    }

    public void ProcessFormula(HashSet<string> visitedCells = null, HashSet<int> encounteredErrors = null)
    {
        if (IsProcessed) return;

        visitedCells ??= new HashSet<string>();
        encounteredErrors ??= new HashSet<int>();

        if (visitedCells.Contains(Address))
        {
            ErrorCode = 1;
            encounteredErrors.Add(1);
            IsProcessed = true;
            return;
        }

        visitedCells.Add(Address);
        string formula = RawValue.TrimStart('=');
        string cellAddressPattern = @"[A-Z]\d+";
        MatchCollection matches = Regex.Matches(formula, cellAddressPattern);

        foreach (Match match in matches)
        {
            string cellAddress = match.Value;
            if (!cellsByAddress.TryGetValue(cellAddress, out ICell referencedCell))
            {
                ErrorCode = 3;
                encounteredErrors.Add(3);
                IsProcessed = true;
                return;
            }

            if (referencedCell is StringCell)
            {
                ErrorCode = 2;
                encounteredErrors.Add(2);
                IsProcessed = true;
                return;
            }

            if (referencedCell is FormulaCell formulaCell && !formulaCell.IsProcessed)
                formulaCell.ProcessFormula(visitedCells, encounteredErrors);

            string cellValue = referencedCell.HasError 
                ? "0" 
                : referencedCell.GetValue()?.ToString() ?? "0";
            
            formula = formula.Replace(cellAddress, cellValue);
        }

        visitedCells.Remove(Address);
        processedFormula = formula;
        IsProcessed = true;
        
    }

    public void EvaluateFormula(StringProcessor stringProcessor, HashSet<int> encouunteredErrors = null)
    {
        if (HasError) return;
        
        try
        {
            processedFormula = stringProcessor.ProcessString(processedFormula).ToString();
        }
        catch (DivideByZeroException)
        {
            ErrorCode = 5;
            encouunteredErrors.Add(5);
        }
        catch
        {
            ErrorCode = 4;
            encouunteredErrors.Add(4);
        }
    }
}