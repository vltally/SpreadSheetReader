namespace ConsoleApp4;

public static class ErrorCodes
{
    private static readonly Dictionary<int, string> ErrorMessages = new()
    {
        { 1, "Circular reference detected" },
        { 2, "Cannot use string value in formula" },
        { 3, "Invalid cell reference" },
        { 4, "Invalid formula format" },
        { 5, "Division by zero" }
    };

    public static string GetErrorMessage(int code) => ErrorMessages[code];
    
    public static string FormatError(int code) => $"Error#{code}";
    
    public static IEnumerable<(int Code, string Message)> GetActiveErrors(HashSet<int> errorCodes) =>
        errorCodes.Select(code => (code, ErrorMessages[code]));
}
