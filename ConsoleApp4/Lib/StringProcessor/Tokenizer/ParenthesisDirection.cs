namespace ConsoleApp4.Lib.StringProcessor.Tokenizer;

/// <summary>
/// Specifies the direction or type of a parenthesis encountered in a mathematical expression.
/// </summary>
/// <remarks>
/// Parentheses are used to alter the natural order of operations, ensuring certain parts 
/// of an expression are evaluated before others. The direction of a parenthesis indicates 
/// whether it opens a new expression context or closes one.
/// </remarks>
public enum ParenthesisDirection : byte
{
    /// <summary>
    /// Indicates no parenthesis direction, typically used as a default or error state.
    /// This value should rarely appear in a correctly tokenized expression.
    /// </summary>
    None,

    /// <summary>
    /// Indicates an opening parenthesis '(' which starts a new sub-expression.
    /// </summary>
    Left,

    /// <summary>
    /// Indicates a closing parenthesis ')' which ends a previously opened sub-expression.
    /// </summary>
    Right
}