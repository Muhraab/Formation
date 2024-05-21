using Formation.Backend.Board;

namespace Formation.Backend.Actions
{
    public abstract class AbstractAction
    {
        protected abstract ActionId ActionId { get; }
        protected abstract bool DoAction(GameBoard board);

        public bool Execute(GameBoard board)
        {
            if (board.SpentActions.Contains(ActionId))
            {
                return false;
            }

            bool success = DoAction(board);

            if (success)
            {
                board.MoveActionToSpent(ActionId);
            } 
            return success; 
        }
    }



}

