namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Operations;

/// <summary>
/// Defines the contract for an operation that can be performed on two numeric operands.
/// </summary>
/// <remarks>
/// Implementations of <see cref="IOperation"/> encapsulate both the functionality 
/// (in the form of a <see cref="Execute"/> function) and the relative precedence level 
/// (as given by <see cref="OperationPriority"/>) of a particular mathematical operator.
/// </remarks>
/// <example>
/// Common operations include:
/// <list type="bullet">
/// <item>Addition (+) with a low priority</item>
/// <item>Multiplication (*) with a medium priority</item>
/// <item>Exponentiation (^) with a high priority</item>
/// </list>
/// For instance, an addition operation may implement <see cref="OperationPriority"/> as 
/// low and <see cref="Execute"/> as (left, right) => left + right.
/// </example>
public interface IOperation
{
    /// <summary>
    /// Gets the priority level of this operation, which determines 
    /// how it is ordered relative to other operations in an expression.
    /// </summary>
    /// <value>
    /// One of the <see cref="OperationPriority"/> values indicating this operation’s precedence. 
    /// Higher priority operations are evaluated before lower priority operations 
    /// (excluding the effects of parentheses).
    /// </value>
    OperationPriority OperationPriority { get; }

    char Symbol { get; }
    
    /// <summary>
    /// Gets the function that executes this operation given two operands.
    /// </summary>
    /// <value>
    /// A <see cref="Func{T,TResult}"/> delegate that takes two <see cref="double"/> values 
    /// (the left and right operands) and returns a <see cref="double"/> result after applying 
    /// the operation’s logic.
    /// </value>
    /// <exception cref="DivideByZeroException">
    /// Implementations of division or modulus operations may throw this if the right operand is zero.
    /// </exception>
    Func<double, double, double> Execute { get; }
}