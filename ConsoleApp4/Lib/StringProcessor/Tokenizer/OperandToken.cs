namespace ConsoleApp4.Lib.StringProcessor.Tokenizer;

/// <summary>
/// Represents a numeric operand token extracted from an expression string.
/// </summary>
/// <remarks>
/// <para>
/// An <see cref="OperandToken"/> holds a string value that corresponds to a sequence of digits 
/// (e.g., "42" or "100") encountered during the tokenization phase. Later stages (e.g., the parser) 
/// typically convert this string to a numeric type (like <see cref="int"/> or <see cref="double"/>)
/// and incorporate it into the abstract syntax tree as a <see cref="NumberNode"/> or similar structure.
/// </para>
/// <para>
/// By keeping operands as strings at the tokenization stage, the tokenizer remains simple and focused. 
/// Converting them to numeric values is deferred until the parsing stage, allowing each component to 
/// handle its own responsibilities (tokenization vs. parsing vs. evaluation).
/// </para>
/// </remarks>
/// <param name="value">The string representation of the numeric operand, consisting solely of digits.</param>
public sealed class OperandToken(string value) : Token
{
    /// <summary>
    /// Gets the string value of the numeric operand token.
    /// </summary>
    /// <value>
    /// A string containing only digit characters (0-9). This value is expected to be 
    /// validly formatted so it can be parsed as a numeric type in later processing steps.
    /// </value>
    public string Value { get; } = value;
}