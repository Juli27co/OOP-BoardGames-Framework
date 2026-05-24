namespace OOP_BoardGames_Framework
{
    // Inheritance from Player class
    public class Computer : Player
    {
        public Computer(int playerNumber, string gamePiece) : base(playerNumber, gamePiece)
        {
        }
        // Polymorphic behaviour
        public override string RequestAction(Board board, GameRules rules, int turn)
        {
            // try to win first
            PlayerMove move = CheckForWinningMove(board, rules);
            if (move == null)
            {
                // if no winning move, pick random valid move
                move = ChooseRandomValidMove(board, rules);
            }
            return move?.Move ?? "";
        }
        private PlayerMove CheckForWinningMove(Board board, GameRules rules)
        {
            // get all valid moves for AI testing
            List<PlayerMove> moves = rules.GetValidMoves(board, this);
            foreach (PlayerMove move in moves)
            {
                if (rules.ApplyMove(board, move))
                {
                    bool gameEnd = rules.CheckForWinning(board);
                    rules.UndoMove(board, move);
                    // Notakto uses opposite win logic
                    if (rules is NotaktoRules)
                    {
                        // choose a move that avoids ending the game
                        if (!gameEnd)
                        {
                            return move;
                        }
                    }
                    else if (gameEnd)
                    {
                        return move;
                    }
                }
            }
            return null!;
        }
        private PlayerMove ChooseRandomValidMove(Board board, GameRules rules)
        {
            List<PlayerMove> moves = rules.GetValidMoves(board, this);
            if (moves.Count == 0)
            {
                return null!;
            }
            Random random = new Random();
            return moves[random.Next(moves.Count)];
        }
    }
}