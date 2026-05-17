

namespace OOP_BoardGames_Framework
{
    public class Human : Player
    {
        public Human(int playerId, string gamePiece) : base(playerId, gamePiece)
        {

        }

        // This method prompts the human player to enter their next action, reads the input, and validates it.
        public override string RequestAction(Board board, GameRules rules, int turn)
        {
            Console.WriteLine($"Player #{this.PlayerId}: Type your next action.");
            string action = Logger.ReadLine() ?? "";
            if (action == "save") return action;

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
