
namespace OOP_BoardGames_Framework
{
    // The Player class represents a player in the Connect Four game.
    public class Player
    {
        public int PlayerId { get; }

        // Store the symbol/game piece used by this player.
        public string GamePiece { get; }


        public Player(int playerId)
        {
            PlayerId = playerId;
        }

        public virtual string RequestAction(Board board, GameRules game, int turn)
        {
            throw new NotImplementedException("RequestAction must be implemented in derived classes.");
        }
    }
}

