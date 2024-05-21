using Formation.Backend.Board;

namespace Formation.Backend.Actions
{
    public class RokadeAction : AbstractAction
    {
        public Direction Direction { get; }
        protected override ActionId ActionId => ActionId.Rokade;

        public RokadeAction(Direction direction)
        {
            Direction = direction;
        }

        protected override bool DoAction(GameBoard board)
        {
            CellRow row = board.GetActiveCellRow();
            int top = GetTopIndex(row);
            int bottom = GetBottomIndex(row);

            if (Direction == Direction.Down)
            {
                if (bottom == 7)
                    return false;

                row.Move(top, bottom + 1);
            }
            else
            {
                if (top == 0)
                    return false;

                row.Move(bottom, top - 1);
            }

            return true;
        }

        private int GetTopIndex(CellRow row)
        {
            for (int i = 0; i < 8; i++)
            {
                if (row.HasDice(i))
                    return i;
            }

            throw new ArgumentOutOfRangeException();
        }

        private int GetBottomIndex(CellRow row)
        {
            for (int i = 7; i >= 0; i--)
            {
                if (row.HasDice(i))
                    return i;
            }

            throw new ArgumentOutOfRangeException();
        }
    }
}

