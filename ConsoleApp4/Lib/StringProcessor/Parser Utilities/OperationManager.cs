using ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Operations;
using ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Operations.Supported;

namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities;

/// <summary>
/// Provides a factory for creating <see cref="IOperation"/> instances based on a given operator symbol.
/// </summary>
/// <remarks>
/// The <see cref="OperationManager"/> maps single-character symbols (e.g., '+', '-', '*') 
/// to functions that construct corresponding <see cref="IOperation"/> instances. 
/// This allows the parser and tokenizer to remain generic and operation-agnostic, 
/// while enabling new operations to be added by simply extending the dictionary.
/// </remarks>
public sealed class OperationManager
{
 private readonly List<IOperation> _operations = new()
    {
        new AdditionOperation(),
        new SubtractionOperation(),
        new MultiplicationOperation(),
        new DivisionOperation(),
        new ModulusOperation(),
        new PowerOperation(),
    };
 
    /// <summary>
    /// Retrieves an <see cref="IOperation"/> instance for the given operator symbol.
    /// </summary>
    /// <param name="symbol">A character representing a mathematical operator (e.g., '+', '*').</param>
    /// <returns>
    /// An <see cref="IOperation"/> instance corresponding to the specified symbol, 
    /// or <c>null</c> if the symbol is not recognized.
    /// </returns>
    /// <remarks>
    /// The method returns <c>null</c> if the provided symbol is not found in the dictionary, 
    /// enabling callers to handle unknown operators gracefully.
    /// </remarks>
   
    
    public IOperation? GetOperation(char symbol)
    {
       
        IOperation? operation = _operations
            .FirstOrDefault(op => op.Symbol == symbol); 

        if (operation == null)
        {
           
            return null;
        }

        return operation;
    }
    
}
