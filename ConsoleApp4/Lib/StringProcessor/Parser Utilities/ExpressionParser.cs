using ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Operations;
using ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Nodes;
using ConsoleApp4.Lib.StringProcessor.Tokenizer;

namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities;

/// <summary>
/// The <see cref="ExpressionParser"/> class is responsible for transforming a sequence of tokens 
/// into an Abstract Syntax Tree (AST) representation of a mathematical expression. 
/// It handles operator precedence, parentheses, and numeric operands.
/// </summary>
/// <remarks>
/// <para>
/// The parser works by progressively consuming tokens and building an AST consisting of:
/// <list type="bullet">
/// <item><see cref="NumberNode"/> for numeric values.</item>
/// <item><see cref="BinaryOperationNode"/> for operations (e.g., +, -, *, /, ^).</item>
/// <item>Nested nodes to handle parentheses and operator precedence.</item>
/// </list>
/// </para>
/// <para>
/// The parser implements a recursive-descent strategy, breaking down expression parsing into 
/// multiple stages of precedence handling. It expects a well-formed token list produced by 
/// an <see cref="ITokenizer"/> implementation.
/// </para>
/// </remarks>
public sealed class ExpressionParser : IParser
{
    private const string UnexpectedEndErrorMessage = "Unexpected end of tokens while parsing primary.";
    private const string ExtraTokensErrorMessage = "Extra tokens found after parsing completed.";
    private const string MissingClosingParenthesisErrorMessage = "Missing closing parenthesis.";
    private const string InvalidOperationTokenErrorMessage = "Expected an operation token at this position.";
    private const string InvalidNumberFormatErrorMessage = "Invalid number format: {0}";
    private const string UnexpectedTokenErrorMessage = "Unexpected token {0} at position {1}.";

    private List<Token> _tokens = null!;
    private int _currentTokenIndex;

    /// <summary>
    /// Parses a list of <see cref="Token"/> objects into an AST representation of the expression.
    /// </summary>
    /// <param name="tokens">The list of tokens to parse, typically produced by an <see cref="ITokenizer"/>.</param>
    /// <returns>
    /// A <see cref="Node"/> object representing the root of the AST for the given expression.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the input contains extra tokens after a complete expression is parsed, 
    /// if a closing parenthesis is missing, or if an unexpected token is encountered.
    /// </exception>
    /// <exception cref="FormatException">
    /// Thrown if a numeric token cannot be parsed into a valid number.
    /// </exception>
    public Node Parse(List<Token> tokens)
    {
        _tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
        _currentTokenIndex = 0;

        Node root = ParseLowestPriority();

        if (_currentTokenIndex < _tokens.Count)
        {
            throw new InvalidOperationException(ExtraTokensErrorMessage);
        }

        return root;
    }

    /// <summary>
    /// Parses the most basic elements (primaries) of the expression: numbers and parenthesized expressions.
    /// </summary>
    /// <remarks>
    /// This method handles:
    /// <list type="bullet">
    /// <item><see cref="OperandToken"/> representing numbers.</item>
    /// <item><see cref="Parenthesis"/> representing sub-expressions enclosed in '()'.</item>
    /// </list>
    /// </remarks>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the token stream ends unexpectedly or if a closing parenthesis is missing.
    /// </exception>
    /// <exception cref="FormatException">
    /// Thrown if a number token cannot be parsed as an integer.
    /// </exception>
    private Node ParsePrimary()
    {
        if (_currentTokenIndex >= _tokens.Count)
        {
            throw new InvalidOperationException(UnexpectedEndErrorMessage);
        }

        Token currentToken = _tokens[_currentTokenIndex];

        if (currentToken is OperandToken operandToken)
        {
            if (!int.TryParse(operandToken.Value, out int numericValue))
                throw new FormatException(string.Format(InvalidNumberFormatErrorMessage, operandToken.Value));

            _currentTokenIndex++;
            return new NumberNode(numericValue);
        }

        if (currentToken is Parenthesis parenthesis && parenthesis.Direction == ParenthesisDirection.Left)
        {
            _currentTokenIndex++; // consume '('
            Node expression = ParseLowestPriority();

            if (_currentTokenIndex >= _tokens.Count ||
                !(_tokens[_currentTokenIndex] is Parenthesis closeParen) ||
                closeParen.Direction != ParenthesisDirection.Right)
            {
                throw new InvalidOperationException(MissingClosingParenthesisErrorMessage);
            }

            _currentTokenIndex++; // consume ')'
            return expression;
        }

        throw new InvalidOperationException(
            string.Format(UnexpectedTokenErrorMessage, currentToken.GetType().Name, _currentTokenIndex));
    }

    /// <summary>
    /// Parses a binary operation node at a specific operator precedence level.
    /// </summary>
    /// <param name="parseNextLevel">
    /// A function to parse the next higher-precedence level. For example, when parsing 
    /// addition and subtraction (low priority), <paramref name="parseNextLevel"/> might 
    /// parse multiplication and division (medium priority).
    /// </param>
    /// <param name="operationPriority">The priority level of the operations to parse.</param>
    /// <returns>A <see cref="Node"/> representing a binary operation or a simpler expression at that level.</returns>
    private Node ParseBinaryOperation(Func<Node> parseNextLevel, OperationPriority operationPriority)
    {
        Node leftNode = parseNextLevel();
        while (IsOperationWithPriority(operationPriority))
        {
            OperationToken operationToken = GetOperationToken();
            Func<double, double, double> operationFunc = operationToken.Operation.Execute;

            _currentTokenIndex++;
            Node rightNode = parseNextLevel();
            leftNode = new BinaryOperationNode(leftNode, rightNode, operationFunc);
        }

        return leftNode;
    }

    /// <summary>
    /// Retrieves the current <see cref="OperationToken"/> and ensures that the current token is indeed an operator.
    /// </summary>
    /// <exception cref="InvalidOperationException">Thrown if the current token is not an operation token.</exception>
    private OperationToken GetOperationToken()
    {
        if (_currentTokenIndex >= _tokens.Count || _tokens[_currentTokenIndex] is not OperationToken opToken)
        {
            throw new InvalidOperationException(InvalidOperationTokenErrorMessage);
        }

        return opToken;
    }

    /// <summary>
    /// Checks if the current token represents an operator with the given <paramref name="operationPriority"/>.
    /// </summary>
    /// <param name="operationPriority">The priority level to check against the current token.</param>
    /// <returns>
    /// <c>true</c> if the current token is an operation token with the specified priority; otherwise, <c>false</c>.
    /// </returns>
    private bool IsOperationWithPriority(OperationPriority operationPriority) =>
        _currentTokenIndex < _tokens.Count &&
        _tokens[_currentTokenIndex] is OperationToken opToken &&
        opToken.Operation.OperationPriority == operationPriority;

    /// <summary>
    /// Parses an expression with the lowest operator precedence level (e.g., addition and subtraction).
    /// </summary>
    private Node ParseLowestPriority() =>
        ParseBinaryOperation(ParseMediumPriority, OperationPriority.Low);

    /// <summary>
    /// Parses expressions at the medium operator precedence level (e.g., multiplication, division, modulus).
    /// </summary>
    private Node ParseMediumPriority() =>
        ParseBinaryOperation(ParseHigherPriority, OperationPriority.Medium);

    /// <summary>
    /// Parses expressions at the highest operator precedence level (e.g., exponentiation).
    /// </summary>
    private Node ParseHigherPriority() =>
        ParseBinaryOperation(ParsePrimary, OperationPriority.High);
}