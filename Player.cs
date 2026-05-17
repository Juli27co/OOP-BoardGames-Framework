
namespace OOP_BoardGames_Framework
{
    public class Player // The Player class represents a player in the Connect Four game.
    {
        public int PlayerId { get; }
        public string GamePiece { get; }
        public Player(int playerId, string gamePiece)
        {
            PlayerId = playerId;
            GamePiece = gamePiece;
        }
        public virtual string RequestAction(Board board, GameRules game, int turn)
        {
            throw new NotImplementedException("RequestAction must be implemented in derived classes.");
        }
    }
}