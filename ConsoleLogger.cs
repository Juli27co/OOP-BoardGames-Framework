
namespace OOP_BoardGames_Framework
{
    internal static class Logger
    {
        private const string HelpRequestText = "help";
        public static string ReadLine(string? request = null)// Custom method to read user input.
        {
            if (request != null)
            {
                Console.WriteLine(request);
            }
            string input = Console.ReadLine() ?? "";
            if (input == HelpRequestText)
            {
                ShowHelpMessage();
                return ReadLine(request);
            }
            return input;
        }
        public static void WriteLine(string? message = null)// Write a line to console
        {
            Console.WriteLine(message);
        }
        public static void WriteLine(string format, params object?[] args)// Write a formatted line to console
        {
            Console.WriteLine(format, args);
        }
        public static void Write(string? message)// Write to console without a newline
        {
            Console.Write(message);
        }
        public static void Write(string format, params object?[] args)// Write a formatted message without a newline
        {
            Console.Write(format, args);
        }
        public static int ReadInt(string? prompt = null, int min = int.MinValue, int max = int.MaxValue)// Read integer input with validation
        {
            while (true)
            {
                if (prompt != null)
                {
                    Write(prompt);
                }
                try
                {
                    int result = int.Parse(Console.ReadLine() ?? "");
                    if (result >= min && result <= max)
                    {
                        return result;
                    }
                    else
                    {
                        throw new FormatException();
                    }
                }
                catch
                {
                    WriteLine($"Invalid input. Please enter a number between {min} and {max}.");
                }
            }
        }
        public static void PrintHeader(string headerText)// Display a section header
        {
            WriteLine($"\n>>>> {headerText} <<<<");
        }
        public static void PrintOption(int number, string optionText)// Display an option item (typically for menus)
        {
            WriteLine($"{number}. {optionText}");
        }
        public static void Clear() // Clear the console
        {
            Console.Clear();
        }
        public static void ShowHelpMessage() // Method to display the help menu.
        {
            PrintHeader("HELP MENU");
        }
    }
}