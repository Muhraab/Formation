using Formation.Backend.Actions;
using Formation.Backend.Board;

namespace Formation.Backend.GameStates
{
    public class PlayerTurn: State
    {
        public GameBoard Board { get; private set; }
        
        public PlayerTurn(GameBoard board)
        {
            Board = board;
        }

        public State TakeAction(AbstractAction Action) 
        {
            bool Success = Action.Execute(Board);

            if (Board.IsGameEnded)
                return new EndGame(Board);

            return this;
        }

    }
}
