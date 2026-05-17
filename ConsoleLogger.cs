
namespace OOP_BoardGames_Framework
{
    internal static class Logger
    {
        private const string HelpRequestText = "help";

        // Custom method to read user input.
        public static string ReadLine(string? request = null)
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
            // else if (input == "save")
            // {
            //     throw new SaveAndExit();
            // }

            return input;
        }

        // Write a line to console
        public static void WriteLine(string? message = null)
        {
            Console.WriteLine(message);
        }

        // Write a formatted line to console
        public static void WriteLine(string format, params object?[] args)
        {
            Console.WriteLine(format, args);
        }

        // Write to console without a newline
        public static void Write(string? message)
        {
            Console.Write(message);
        }

        // Write a formatted message without a newline
        public static void Write(string format, params object?[] args)
        {
            Console.Write(format, args);
        }

        // Read integer input with validation
        public static int ReadInt(string? prompt = null, int min = int.MinValue, int max = int.MaxValue)
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

        // Display a section header
        public static void PrintHeader(string headerText)
        {
            WriteLine($"\n>>>> {headerText} <<<<");
        }

        // Display an option item (typically for menus)
        public static void PrintOption(int number, string optionText)
        {
            WriteLine($"{number}. {optionText}");
        }

        // Clear the console
        public static void Clear()
        {
            Console.Clear();
        }

        // Method to display the help menu.
        public static void ShowHelpMessage()
        {
            PrintHeader("HELP MENU");
        }

    }
}
