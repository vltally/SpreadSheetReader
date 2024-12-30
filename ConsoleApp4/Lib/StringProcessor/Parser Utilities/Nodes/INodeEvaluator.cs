namespace ConsoleApp4.Lib.StringProcessor.Parser_Utilities.Nodes
{
    
    public interface INodeEvaluator
    {
        /// <summary>
        /// Evaluates the given <paramref name="node"/> and returns its numeric result.
        /// </summary>
        /// <param name="node">A node in the AST, representing a portion of an expression.</param>
        /// <returns>
        /// A <see cref="double"/> that represents the computed value of the AST 
        /// defined by <paramref name="node"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="node"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the node cannot be evaluated due to invalid or unsupported node types.
        /// </exception>
        double Evaluate(Node node);
    }
}