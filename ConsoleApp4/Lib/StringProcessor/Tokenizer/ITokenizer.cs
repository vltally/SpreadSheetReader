namespace ConsoleApp4.Lib.StringProcessor.Tokenizer
{
    /// <summary>
    /// Defines a contract for classes that convert an input expression string into a list of <see cref="Token"/> objects.
    /// </summary>
    /// <remarks>
    /// Implementations of this interface are expected to handle parsing of operators, operands (numbers), 
    /// parentheses, and potentially other token types as needed. The resulting token list should be 
    /// suitable for further processing by a parser.
    /// </remarks>
    public interface ITokenizer
    {
        /// <summary>
        /// Converts the provided <paramref name="inputCharacters"/> string into a sequence of <see cref="Token"/> objects.
        /// </summary>
        /// <param name="inputCharacters">
        /// The raw expression string containing operators, operands, and other elements 
        /// that need to be tokenized. Whitespace or formatting nuances should be handled 
        /// by the tokenizer implementation.
        /// </param>
        /// <returns>
        /// A list of <see cref="Token"/> objects representing the parsed elements of the expression. 
        /// The order of tokens should match their appearance in the input string, after whitespace removal.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="inputCharacters"/> is <c>null</c>.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// Thrown if an unexpected character or format is encountered that cannot be tokenized.
        /// </exception>
        List<Token> Tokenize(string inputCharacters);
    }
}