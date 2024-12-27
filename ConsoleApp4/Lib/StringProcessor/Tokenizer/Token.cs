namespace ConsoleApp4.Lib.StringProcessor.Tokenizer;

/// <summary>
/// Serves as the abstract base class for all tokens produced by the tokenization process.
/// </summary>
/// <remarks>
/// <para>
/// A token is the smallest meaningful unit extracted from an input expression string. Tokens are 
/// used by parsers to construct an abstract syntax tree (AST) and, ultimately, evaluate an expression.
/// By representing tokens as distinct classes derived from <see cref="Token"/>, the system can 
/// distinguish between different kinds of syntax elements, such as:
/// <list type="bullet">
/// <item><see cref="OperandToken"/> for numbers</item>
/// <item><see cref="OperationToken"/> for operators (+, -, *, /, etc.)</item>
/// <item><see cref="Parenthesis"/> for grouping sub-expressions</item>
/// </list>
/// </para>
/// <para>
/// The <see cref="Token"/> class itself is abstract, meaning it cannot be instantiated directly. 
/// Concrete subclasses define their own properties and behaviors relevant to their token type, 
/// enabling the tokenizer and parser to handle complex expressions in a structured manner.
/// </para>
/// </remarks>
public abstract class Token;