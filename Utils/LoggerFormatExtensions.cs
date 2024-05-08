namespace Utils
{
    public static class LoggerFormatExtensions
    {
        public static string FormatMessage(string projectName, string message)
        {
            const string redColor = "\u001b[33m";
            const string resetColor = "\u001b[0m";

            return $"{redColor}[{projectName}]{resetColor} {message}";
        }
    }
}