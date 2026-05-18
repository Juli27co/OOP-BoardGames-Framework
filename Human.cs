namespace OOP_BoardGames_Framework
{
    public class Human : Player
    {
        public Human(int playerId, string gamePiece) : base(playerId, gamePiece)
        {
        }
        public override string RequestAction(Board board, GameRules rules, int turn)// This method prompts the human player to enter their next action, reads the input, and validates it.
        {
            Console.WriteLine($"Player #{this.PlayerId}: Type your next action.");

            if (rules is NumericalTicTacToeRules)
            {
                string numberType = this.PlayerId == 1 ? "odd" : "even";
                Logger.WriteLine($"Player #{this.PlayerId}: Enter position,number. Example: 5,3");
                Logger.WriteLine($"Player #{this.PlayerId} must use an unused {numberType} number.");
            }
            else if (rules is GomokuRules)
            {
                Logger.WriteLine($"Player #{this.PlayerId}: Enter a move like J8.");
            }
            else if (rules is TicTacToeRules)
            {
                Logger.WriteLine($"Player #{this.PlayerId}: Enter a number from 1 to 9.");
            }
            else if (rules is ConnectFourRules)
            {
                Logger.WriteLine($"Player #{this.PlayerId}: Enter a column from 1 to 7.");
            }
            else if (rules is NotaktoRules)
            {
                Logger.WriteLine($"Player #{this.PlayerId}: Enter board, position. Example: 1, 5");
                Logger.WriteLine("  Board 1 | Board 2 | Board 3");
                Logger.WriteLine("  1 2 3   |  1 2 3   |  1 2 3");
                Logger.WriteLine("  4 5 6   |  4 5 6   |  4 5 6");
                Logger.WriteLine("  7 8 9   |  7 8 9   |  7 8 9");
            }

            else
            {
                Logger.WriteLine($"Player #{this.PlayerId}: Type your next action.");
            }
            string action = Logger.ReadLine() ?? "";
            if (action == "save" || action == "redo" || action == "undo")
            {
                return action;
            }
            PlayerMove move = new PlayerMove(action, this, turn);
            try
            {
                bool valid = rules.ValidatePlayerMove(board, move);
                if (valid)
                {
                    return action;
                }
                else
                {
                    throw new FormatException("Invalid move. Please try again.");
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);
                return this.RequestAction(board, rules, turn);
            }

        }
    }
}
