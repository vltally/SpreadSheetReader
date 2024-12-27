using ConsoleApp4.Lib.StringProcessor.Parser_Utilities;
using ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Nodes;
using ConsoleApp4.Lib.StringProcessor.Tokenizer;

namespace ConsoleApp4.Lib.StringProcessor.Evaluator;

/// <summary>
/// The <see cref="ExpressionEvaluator"/> class provides a high-level interface 
/// for evaluating mathematical expressions. It orchestrates the tokenization, parsing, 
/// and evaluation steps to produce a final numeric result.
/// </summary>
/// <remarks>
/// <para>
/// This class relies on three primary components:
/// <list type="bullet">
/// <item><see cref="ITokenizer"/> to convert the input expression into a sequence of tokens.</item>
/// <item><see cref="IParser"/> to build an abstract syntax tree (AST) from the tokens.</item>
/// <item><see cref="INodeEvaluator"/> to evaluate the AST and compute a numeric result.</item>
/// </list>
/// By injecting these dependencies, the <see cref="ExpressionEvaluator"/> can remain 
/// flexible and easily testable, as different implementations of these interfaces can 
/// be provided without changing the evaluation logic.
/// </para>
/// <para>
/// Typical usage involves providing an expression string such as "3+5*(10-2)", which is then 
/// tokenized, parsed, and finally evaluated to a <see cref="double"/> result.
/// </para>
/// </remarks>
public sealed class ExpressionEvaluator : IExpressionEvaluator
{
    private readonly ITokenizer _tokenizer;
    private readonly IParser _parser;
    private readonly INodeEvaluator _nodeEvaluator;

    /// <summary>
    /// Initializes a new instance of the <see cref="ExpressionEvaluator"/> class.
    /// </summary>
    /// <param name="tokenizer">
    /// An implementation of <see cref="ITokenizer"/> that will be used to convert the 
    /// input expression into tokens.
    /// </param>
    /// <param name="parser">
    /// An implementation of <see cref="IParser"/> used to translate the list of tokens 
    /// into an abstract syntax tree (AST).
    /// </param>
    /// <param name="nodeEvaluator">
    /// An implementation of <see cref="INodeEvaluator"/> that evaluates the AST nodes 
    /// to produce a final numeric result.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Thrown if any of the provided dependencies (<paramref name="tokenizer"/>, 
    /// <paramref name="parser"/>, or <paramref name="nodeEvaluator"/>) are <c>null</c>.
    /// </exception>
    public ExpressionEvaluator(ITokenizer tokenizer, IParser parser, INodeEvaluator nodeEvaluator)
    {
        _tokenizer = tokenizer;
        _parser = parser;
        _nodeEvaluator = nodeEvaluator;
    }

    /// <summary>
    /// Evaluates the provided mathematical <paramref name="expression"/> string and returns its numeric result.
    /// </summary>
    /// <param name="expression">
    /// A mathematical expression as a string. For example: "3+5*(10-2)". 
    /// This may include integers, operators, parentheses, and potentially other supported constructs.
    /// </param>
    /// <returns>
    /// A <see cref="double"/> representing the evaluated result of the mathematical expression.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="expression"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="FormatException">
    /// Thrown if the expression contains invalid number formats.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the expression is syntactically incorrect, 
    /// contains unrecognized tokens, or cannot be fully evaluated.
    /// </exception>
    public double Evaluate(string expression)
    {
        if (expression == null) 
            throw new ArgumentNullException(nameof(expression));

        List<Token> tokens = _tokenizer.Tokenize(expression);
        Node ast = _parser.Parse(tokens);
        return _nodeEvaluator.Evaluate(ast);
    }
}