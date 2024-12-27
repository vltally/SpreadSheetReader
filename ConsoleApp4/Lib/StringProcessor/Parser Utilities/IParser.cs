using ConsoleApp4.Lib.StringProcessor.Tokenizer;
using ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Nodes;

namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities;

/// <summary>
/// Defines a contract for transforming a list of <see cref="Token"/> objects into an 
/// abstract syntax tree (AST) representation of a mathematical expression.
/// </summary>
/// <remarks>
/// Implementations of <see cref="IParser"/> are expected to:
/// <list type="bullet">
/// <item>Take a tokenized representation of an expression (as produced by an <see cref="ITokenizer"/>)</item>
/// <item>Respect operator precedence, handle parentheses, and construct an AST composed of <see cref="Node"/> objects.</item>
/// <item>Return a root <see cref="Node"/> that can be evaluated to produce a numeric result.</item>
/// </list>
/// This separation of concerns allows tokenization and parsing to be tested and maintained independently.
/// </remarks>
public interface IParser
{
    /// <summary>
    /// Parses the given list of <see cref="Token"/> objects and returns an AST <see cref="Node"/> 
    /// that represents the entire mathematical expression.
    /// </summary>
    /// <param name="tokens">
    /// A list of tokens representing a mathematical expression. This list is typically generated 
    /// by an <see cref="ITokenizer"/> implementation that converts an input string into tokens.
    /// </param>
    /// <returns>
    /// A <see cref="Node"/> serving as the root of the AST. This node can be evaluated 
    /// to produce a numeric result of the parsed expression.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="tokens"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the token list cannot be parsed into a valid expression, 
    /// for example, due to extra tokens after a complete expression is parsed or 
    /// missing closing parentheses.
    /// </exception>
    /// <exception cref="FormatException">
    /// Thrown if one of the tokens representing a number is invalid and cannot 
    /// be converted into a numeric value.
    /// </exception>
    Node Parse(List<Token> tokens);
}