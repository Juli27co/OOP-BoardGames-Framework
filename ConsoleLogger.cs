
namespace OOP_BoardGames_Framework
{
    internal static class Logger
    {
        private const string HelpRequestText = "help";

        // Custom method to read user input. It displays a prompt if provided, and checks for special commands like "help" and "save".
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
            else if (input == "save")
            {
                throw new SaveAndExit();
            }

            return input;
        }

        // Method to display the help menu.
        public static void ShowHelpMessage()
        {
            Console.WriteLine("\n>>>> HELP MENU <<<<");
        }

    }
}
