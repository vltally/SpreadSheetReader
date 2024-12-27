using ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Operations;

namespace ConsoleApp4.Lib.StringProcessor.Tokenizer;

/// <summary>
/// Represents a token corresponding to a mathematical operation, such as addition, subtraction, 
/// multiplication, division, or exponentiation.
/// </summary>
/// <remarks>
/// <para>
/// When an input expression (e.g., "3+5*2") is tokenized, each operator character 
/// (like '+', '-', '*') is transformed into an <see cref="OperationToken"/> that encapsulates 
/// both the identity of the operation and its associated precedence. This approach decouples 
/// the parsing logic from hard-coded operator rules, making the system more extensible.
/// </para>
/// <para>
/// During the parsing stage, <see cref="OperationToken"/> instances guide the construction 
/// of an abstract syntax tree (AST) by helping determine where and how operators apply to 
/// operand nodes. The parser uses the <see cref="IOperation"/> stored in this token to build 
/// <see cref="BinaryOperationNode"/> instances or similar AST constructs.
/// </para>
/// </remarks>
/// <param name="operation">The operation associated with this token, including its priority and execute function.</param>
public class OperationToken(IOperation operation) : Token
{
    /// <summary>
    /// Gets the <see cref="IOperation"/> associated with this token.
    /// </summary>
    /// <value>
    /// An <see cref="IOperation"/> that defines both the operation’s precedence (via 
    /// <see cref="IOperation.OperationPriority"/>) and its logic (via 
    /// <see cref="IOperation.Execute"/>).
    /// </value>
    public IOperation Operation { get; } = operation;
}