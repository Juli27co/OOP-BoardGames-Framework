

namespace OOP_BoardGames_Framework
{
    public class PlayerMove
    {
        public string Move { get; set; }
        public Player Player { get; set; }

        public int Turn { get; set; }

        public PlayerMove(string move, Player player, int turn)
        {
            Move = move;
            Player = player;
            Turn = turn;
        }
    }
}
