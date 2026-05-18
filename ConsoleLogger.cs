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
                Logger.Clear();
                return "help"; // Return "help" instead of recursing - let caller handle redisplay
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
        public static void PrintOption(int number, string optionText) // Display an option item (typically for menus)
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
            WriteLine("\n  SPECIAL COMMANDS:");
            WriteLine("");
            WriteLine("  • SAVE");
            WriteLine("    Saves your current game progress to a file.");
            WriteLine("    You can continue this game later by selecting 'Continue Saved Game'");
            WriteLine("    Type: save");
            WriteLine("");
            WriteLine("  • UNDO");
            WriteLine("    Reverses your last move.");
            WriteLine("    Useful if you made a mistake or want to try a different strategy.");
            WriteLine("    Type: undo");
            WriteLine("");
            WriteLine("  • REDO");
            WriteLine("    Restores the move you just undid.");
            WriteLine("    Use this if you changed your mind after undoing.");
            WriteLine("    Type: redo");
            WriteLine("");
            WriteLine("  • HELP");
            WriteLine("    Displays this help menu at any time during your turn.");
            WriteLine("    Type: help");
            WriteLine("");
            WriteLine(new string('─', 60));
        }

        public static void PrintTurnHeader(GameType gameType, GameMode gameMode, int turnNumber, Player currentPlayer)
        {
            WriteLine("\n" + new string('=', 60));
            WriteLine($"  GAME: {gameType} | MODE: {gameMode}");
            WriteLine($"  TURN: {turnNumber}");
            WriteLine(new string('=', 60));

            WriteLine($"\n  >>>  PLAYER {currentPlayer.PlayerId}'s TURN - {currentPlayer.GetType().Name}  <<<");
            WriteLine("");
        }

        public static void PrintBoardSectionStart()
        {
            WriteLine(new string('─', 60));
            WriteLine("  CURRENT BOARD");
            WriteLine(new string('─', 60));
        }

        public static void PrintBoardSectionEnd()
        {
            WriteLine(new string('─', 60));
        }

        public enum InstructionLevel
        {
            Main,
            Sub,
            None
        }

        public static void PrintInstructionsSection(params (string instruction, InstructionLevel level)[] instructions)
        {
            WriteLine(new string('─', 60));
            WriteLine("  INSTRUCTIONS");
            WriteLine(new string('─', 60));
            foreach (var (instruction, level) in instructions)
            {
                string prefix = "";

                switch (level)
                {
                    case InstructionLevel.Main:
                        prefix = "  ► ";
                        break;
                    case InstructionLevel.Sub:
                        prefix = "    └─ ";
                        break;
                }

                WriteLine(prefix + instruction);
            }
            WriteLine(new string('─', 60));
        }
    }
}