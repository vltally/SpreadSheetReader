namespace ConsoleApp4.Lib.StringProcessor.Tokenizer;

/// <summary>
/// Represents a parenthesis token encountered within a mathematical expression.
/// </summary>
/// <remarks>
/// Parentheses are crucial in expressions for altering the natural order of operations. 
/// When the tokenizer encounters a '(' or ')' character, it produces a <see cref="Parenthesis"/> token 
/// that indicates whether it is an opening (left) or closing (right) parenthesis.
/// </remarks>
public sealed class Parenthesis : Token
{
    private static readonly Dictionary<char, ParenthesisDirection> ParenthesisMap = new()
    {
        { '(', ParenthesisDirection.Left },
        { ')', ParenthesisDirection.Right }
    };

    /// <summary>
    /// Gets the direction of the parenthesis, indicating whether it is left-opening '(' or right-closing ')'.
    /// </summary>
    /// <value>
    /// A <see cref="ParenthesisDirection"/> value indicating the role of the parenthesis in the expression.
    /// </value>
    public ParenthesisDirection Direction { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Parenthesis"/> class for the specified parenthesis character.
    /// </summary>
    /// <param name="parenthesis">A character that should be either '(' or ')'.</param>
    /// <exception cref="ArgumentException">
    /// Thrown if the provided <paramref name="parenthesis"/> character is not a recognized parenthesis.
    /// </exception>
    public Parenthesis(char parenthesis)
    {
        if (!ParenthesisMap.TryGetValue(parenthesis, out ParenthesisDirection direction))
        {
            throw new ArgumentException($"Invalid parenthesis character: {parenthesis}", nameof(parenthesis));
        }

        Direction = direction;
    }

    /// <summary>
    /// Returns a string representation of the parenthesis token.
    /// </summary>
    /// <returns>
    /// A string containing either "(" or ")" corresponding to the <see cref="Direction"/>.
    /// </returns>
    public override string ToString() =>
        ParenthesisMap.FirstOrDefault(kvp => kvp.Value == Direction).Key.ToString();
}