namespace OOP_BoardGames_Framework
{
    public class Human : Player
    {
        public Human(int playerId, string gamePiece) : base(playerId, gamePiece)
        {
        }
        public override string RequestAction(Board board, GameRules rules, int turn)// This method prompts the human player to enter their next action, reads the input, and validates it.
        {
            DisplayGameInstructions(rules);
            string action = Logger.ReadLine() ?? "";
            if (action == "save" || action == "redo" || action == "undo" || action == "help")
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
        private void DisplayGameInstructions(GameRules rules)
        {
            Logger.PrintInstructionsSection(
                ($"Player #{PlayerId}: enter a move, or type save, undo, redo, help.", Logger.InstructionLevel.Main)
            );
        }
    }
}