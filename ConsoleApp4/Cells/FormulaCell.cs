using System.Text.RegularExpressions;
using ConsoleApp4.Lib.StringProcessor;

namespace ConsoleApp4.Cells;

public class FormulaCell : ICell
{
    public string Address { get; }
    public string RawValue { get; private set; }
    
    private string processedFormula;
    public bool IsProcessed { get; set; }

    private readonly Dictionary<string, ICell> cellsByAddress;
    
    private readonly CellTypeFactory _cellTypeFactory = new CellTypeFactory();

    public FormulaCell(string address, string value, Dictionary<string, ICell> cellsByAddress)
    {
        Address = address;
        this.cellsByAddress = cellsByAddress;
        SetValue(value);
        IsProcessed = false;
    }

    public object GetValue() => processedFormula;

    public void SetValue(string value)
    {
        if (!value.StartsWith("="))
            throw new ArgumentException($"Invalid formula format in cell {Address}");
        RawValue = value;
    }

    public new CellType GetType() => CellType.Formula;

    public void ProcessFormula(HashSet<string> visitedCells = null)
    {
        if (IsProcessed) return;

        visitedCells ??= new HashSet<string>();
        if (visitedCells.Contains(Address))
            throw new ArgumentException($"Circular reference detected at {Address}");

        visitedCells.Add(Address);
        string formula = RawValue.TrimStart('=');
        string cellAddressPattern = @"[A-Z]\d+";
        MatchCollection matches = Regex.Matches(formula, cellAddressPattern);

        foreach (Match match in matches)
        {
            string cellAddress = match.Value;
            if (!cellsByAddress.TryGetValue(cellAddress, out ICell referencedCell))
                throw new ArgumentException($"Invalid cell reference: {cellAddress}");

            if (referencedCell.GetType() == _cellTypeFactory.GetCellType(CellType.String))
                throw new ArgumentException($"Cannot use string value in formula: {cellAddress}");

            if (referencedCell is FormulaCell formulaCell && !formulaCell.IsProcessed)
                formulaCell.ProcessFormula(visitedCells);

            string cellValue = referencedCell.GetValue()?.ToString() ?? "0";
            formula = formula.Replace(cellAddress, cellValue);
        }

        visitedCells.Remove(Address);
        processedFormula = formula;
        IsProcessed = true;
    }

    public void EvaluateFormula(StringProcessor stringProcessor)
    {
        processedFormula = stringProcessor.ProcessString(processedFormula).ToString();
    }
}