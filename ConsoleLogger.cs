namespace OOP_BoardGames_Framework
{
    internal static class Logger
    {
        // Custom method to read user input.
        public static string ReadLine(string? request = null)
        {
            if (request != null)
            {
                Console.WriteLine(request);
            }
            string input = Console.ReadLine() ?? "";
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
        public static void ShowHelpMessage()
        {
            PrintHeader("HELP MENU");
            string[] lines =
            {
        "Commands:",
        "save - save the current game and continue playing",
        "undo - undo the previous move",
        "redo - redo an undone move",
        "help - show this menu",
        "",
        "Move examples:",
        "TicTacToe: 5 (Enter a number 1-9 // 1 is top left // 9 is bottom right)",
        "Numerical TicTacToe: 5,3 (Position, Number)",
        "Connect Four: 4 (Enter a column from 1 to 7)",
        "Gomoku: J8 (Column, Row)",
        "Notakto: 1,5 (Board, Position // 1 is top left // 9 is bottom right)"
    };
            foreach (string line in lines)
            {
                WriteLine(line);
            }
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
        { Main, Sub, None }
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