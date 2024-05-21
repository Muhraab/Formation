using Formation.Backend.Board;

namespace Formation.Backend.GameStates
{
    public class EndGame: State
    {
        public GameBoard Board { get; set; }

        public EndGame(GameBoard board)
        {
            Board = board;
        }
    }
}
