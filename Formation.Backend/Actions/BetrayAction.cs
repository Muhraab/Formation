using Formation.Backend.Board;

namespace Formation.Backend.Actions
{
    public class BetrayAction : AbstractAction
    {
        public int Position { get; private set; }

        protected override ActionId ActionId => ActionId.Betray;

        public BetrayAction(int position)
        {
            Position = position;
        }

        protected override bool DoAction(GameBoard board)
        {
            return board
                .GetActiveCellRow()
                .SwitchWithOpposite(Position, board.GetPassiveCellRow());
        }
    }

}

