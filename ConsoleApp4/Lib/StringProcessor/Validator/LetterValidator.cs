namespace ConsoleApp4.Lib.StringProcessor.Validator;

public class LetterValidator : IValidator
{
    private const string InvalidDataEntry = "The string contains letters. ";

    public bool ValidateExpression(string input)
    {
        foreach (char c in input)
        {
            if (char.IsLetter(c)) 
            {
                throw new ArgumentException(InvalidDataEntry);
            }
        }
        return false; 
    }

}