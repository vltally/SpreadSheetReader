namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Operations;

/// <summary>
/// Represents the relative precedence of different operations within mathematical expressions.
/// </summary>
/// <remarks>
/// Operator precedence determines the order in which operations are performed 
/// when evaluating expressions. Operators with higher priority are evaluated 
/// before those with lower priority, unless parentheses dictate otherwise.
/// </remarks>
/// <example>
/// For a typical arithmetic expression:
/// <list type="bullet">
/// <item><see cref="High"/> priority might be assigned to exponentiation operators (^).</item>
/// <item><see cref="Medium"/> priority might be for multiplication, division, and modulus (*, /, %).</item>
/// <item><see cref="Low"/> priority might be for addition and subtraction (+, -).</item>
/// </list>
/// This ensures that "3 + 5 * 2" is interpreted as "3 + (5 * 2)" rather than "(3 + 5) * 2".
/// </example>
public enum OperationPriority
{
    /// <summary>
    /// Indicates the lowest level of operator precedence, 
    /// typically used for addition and subtraction.
    /// </summary>
    Low,

    /// <summary>
    /// Indicates a middle level of operator precedence, 
    /// often used for multiplication, division, and modulus.
    /// </summary>
    Medium,

    /// <summary>
    /// Indicates the highest level of operator precedence,
    /// often used for operations like exponentiation.
    /// </summary>
    High,
}