namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Nodes;

/// <summary>
/// Represents a numeric literal within the abstract syntax tree (AST).
/// </summary>
/// <remarks>
/// A <see cref="NumberNode"/> holds a single numeric value and directly returns 
/// that value upon evaluation. It forms the terminal elements of an expression tree, 
/// serving as the base values on which operations can be performed.
/// </remarks>
/// <example>
/// Consider the expression "42". When parsed, it may simply produce:
/// <code>
/// var node = new NumberNode(42);
/// double result = node.Evaluate(); // result = 42
/// </code>
/// Or in a more complex expression like "3 + 5", a <see cref="NumberNode"/> might represent 
/// the literal '3' and another for '5', which are then used by a 
/// <see cref="BinaryOperationNode"/> to compute the result.
/// </example>
/// <param name="value">
/// The numeric value this node represents. It should be a valid double precision number.
/// </param>
public class NumberNode(double value) : Node
{
    /// <summary>
    /// Evaluates this node by returning the numeric value it represents.
    /// </summary>
    /// <returns>
    /// A <see cref="double"/> equal to the <paramref name="value"/> stored in the node.
    /// </returns>
    public override double Evaluate()
    {
        return value;
    }
}