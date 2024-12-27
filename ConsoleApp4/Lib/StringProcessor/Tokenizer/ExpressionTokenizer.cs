using ConsoleApp4.Lib.StringProcessor.Parser_Utilities;
using ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Operations;

namespace ConsoleApp4.Lib.StringProcessor.Tokenizer;

/// <summary>
/// The <see cref="ExpressionTokenizer"/> class is responsible for converting a mathematical expression string 
/// into a sequence of tokens that can be parsed and evaluated. It removes whitespace, identifies operators, 
/// operands (numbers), and parentheses, and produces a list of well-defined <see cref="Token"/> objects.
/// </summary>
public sealed class ExpressionTokenizer : ITokenizer
{
    private const string Whitespace = " ";
    private const string UnexpectedCharacterMessage = "Unexpected character '{0}' at position {1}.";

    private readonly OperationManager _operationManager = new();

    /// <summary>
    /// Converts the given <paramref name="input"/> expression string into a list of <see cref="Token"/> objects.
    /// </summary>
    /// <param name="input">A mathematical expression string (e.g., "3+5*(10-2)").</param>
    /// <returns>A list of tokens representing operators, operands, and parentheses found in the expression.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="input"/> is null.</exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if an unknown character is encountered that cannot be interpreted as an operator, operand, or parenthesis.
    /// </exception>
    public List<Token> Tokenize(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            throw new ArgumentNullException(nameof(input));
        }

        input = input.Replace(Whitespace, string.Empty);

        List<Token> tokens = [];
        int currentIndex = 0;

        // Iterate over each character in the input string
        while (currentIndex < input.Length)
        {
            char currentChar = input[currentIndex];

            // Attempt to interpret the character as an operator
            Token? operationToken = CreateOperationToken(currentChar);
            if (operationToken != null)
            {
                tokens.Add(operationToken);
                currentIndex++;
                continue;
            }

            // Attempt to interpret the character sequence starting at currentChar as a number (operand)
            Token? operandToken = CreateOperandToken(ref input, ref currentIndex);
            if (operandToken != null)
            {
                tokens.Add(operandToken);
                continue;
            }

            // Attempt to interpret the character as a parenthesis
            Parenthesis parenthesis = new(currentChar);
            if (parenthesis.Direction != ParenthesisDirection.None)
            {
                tokens.Add(parenthesis);
                currentIndex++;
                continue;
            }

            // If we reach this point, the character does not match any known token type
            throw new InvalidOperationException(
                string.Format(UnexpectedCharacterMessage, currentChar, currentIndex));
        }

        return tokens;
    }

    /// <summary>
    /// Attempts to create an <see cref="OperationToken"/> from the given character.
    /// </summary>
    /// <param name="currentChar">The current character in the input string.</param>
    /// <returns>
    /// An <see cref="OperationToken"/> if the character represents a known operator; 
    /// otherwise, <c>null</c>.
    /// </returns>
    private OperationToken? CreateOperationToken(char currentChar)
    {
        IOperation? operation = _operationManager.GetOperation(currentChar);
        return operation != null ? new OperationToken(operation) : null;
    }

    /// <summary>
    /// Attempts to create an <see cref="OperandToken"/> by reading consecutive digits starting at the current index.
    /// </summary>
    /// <param name="input">
    /// The input expression string (passed by reference to avoid copying large strings repeatedly).
    /// </param>
    /// <param name="currentIndex">
    /// The current parsing position (passed by reference so it can be advanced as digits are consumed).
    /// </param>
    /// <returns>
    /// An <see cref="OperandToken"/> if a sequence of digits is found; otherwise, <c>null</c>.
    /// </returns>
    private static OperandToken? CreateOperandToken(ref string input, ref int currentIndex)
    {
        // If the current character is not a digit or we're at the end of the string, no operand can be formed.
        if (currentIndex >= input.Length || !char.IsDigit(input[currentIndex]))
            return null;

        string number = string.Empty;

        // Consume consecutive digits to form a complete number
        while (currentIndex < input.Length && char.IsDigit(input[currentIndex]))
        {
            number += input[currentIndex];
            currentIndex++;
        }

        return new OperandToken(number);
    }
}