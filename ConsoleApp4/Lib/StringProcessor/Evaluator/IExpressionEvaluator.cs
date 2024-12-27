namespace ConsoleApp4.Lib.StringProcessor.Evaluator;

/// <summary>
/// Defines a contract for classes that evaluate mathematical expressions and produce a numeric result.
/// </summary>
/// <remarks>
/// Implementations of this interface are expected to:
/// <list type="bullet">
/// <item>Parse and interpret the provided expression string into a meaningful computational model.</item>
/// <item>Perform the necessary operations (arithmetic, precedence handling, etc.) to produce a final result.</item>
/// <item>Handle errors in the input expression, such as invalid characters or unmatched parentheses.</item>
/// </list>
/// Common use cases include evaluating user input in calculators, scripting environments, or 
/// data-processing applications.
/// </remarks>
public interface IExpressionEvaluator
{
    /// <summary>
    /// Evaluates the specified mathematical <paramref name="expression"/> and returns its numeric result.
    /// </summary>
    /// <param name="expression">
    /// A string representing a mathematical expression. For example: "3+5*(10-2)". 
    /// The expression may contain various operators, parentheses, and numeric values.
    /// </param>
    /// <returns>
    /// A <see cref="double"/> representing the computed value of the given expression.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if <paramref name="expression"/> is <c>null</c>.
    /// </exception>
    /// <exception cref="FormatException">
    /// Thrown if the <paramref name="expression"/> contains invalid number formats or unrecognized tokens.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the <paramref name="expression"/> is syntactically incorrect or cannot be evaluated 
    /// due to a missing operator, mismatched parentheses, or other logical errors.
    /// </exception>
    double Evaluate(string expression);
}