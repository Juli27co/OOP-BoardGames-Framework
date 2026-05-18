namespace OOP_BoardGames_Framework
{
    public class Human : Player
    {
        public Human(int playerId, string gamePiece) : base(playerId, gamePiece)
        {
        }
        public override string RequestAction(Board board, GameRules rules, int turn)// This method prompts the human player to enter their next action, reads the input, and validates it.
        {
            while (true)
            {
                DisplayGameInstructions(rules);
                string action = Logger.ReadLine() ?? "";

                if (action == "help")
                {
                    Logger.Clear();
                    continue; // Loop back to redisplay board and instructions
                }

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
                    Logger.WriteLine(e.Message);
                    continue; // Loop back to redisplay instructions
                }
            }
        }

        private void DisplayGameInstructions(GameRules rules)
        {
            List<(string instruction, Logger.InstructionLevel level)> instructions = new();
            instructions.Add(($"Player #{this.PlayerId}: Type your next action.", Logger.InstructionLevel.Main));

            if (rules is NumericalTicTacToeRules)
            {
                string numberType = this.PlayerId == 1 ? "odd" : "even";
                instructions.Add(($"Enter position,number. Example: 5,3", Logger.InstructionLevel.Sub));
                instructions.Add(($"Must use an unused {numberType} number.", Logger.InstructionLevel.Sub));
            }
            else if (rules is GomokuRules)
            {
                instructions.Add(("Enter a move like J8.", Logger.InstructionLevel.Sub));
            }
            else if (rules is TicTacToeRules)
            {
                instructions.Add(("Enter a number from 1 to 9.", Logger.InstructionLevel.Sub));
            }
            else if (rules is ConnectFourRules)
            {
                instructions.Add(("Enter a column from 1 to 7.", Logger.InstructionLevel.Sub));
            }
            else if (rules is NotaktoRules)
            {
                instructions.Add(("Enter board, position. Example: 1, 5", Logger.InstructionLevel.Sub));
                instructions.Add(("", Logger.InstructionLevel.None));
                instructions.Add(("   Board 1 | Board 2  | Board 3", Logger.InstructionLevel.None));
                instructions.Add(("   1 2 3   |  1 2 3   |  1 2 3", Logger.InstructionLevel.None));
                instructions.Add(("   4 5 6   |  4 5 6   |  4 5 6", Logger.InstructionLevel.None));
                instructions.Add(("   7 8 9   |  7 8 9   |  7 8 9", Logger.InstructionLevel.None));
            }

            Logger.PrintInstructionsSection(instructions.ToArray());
        }
    }
}
