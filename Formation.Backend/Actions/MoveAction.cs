using Formation.Backend.Board;

namespace Formation.Backend.Actions
{
    public class MoveAction : AbstractAction
    {
        protected override ActionId ActionId => ActionId.Move;

        public MoveAction()
        {

        }

        protected override bool DoAction(GameBoard board)
        {
            CellRow active = board.GetActiveCellRow();

            for (int i = 6; i >= 0; i--)
            { 
                var result = active.Move(i, i+1);
            }

            return true;
        }
    }


}

