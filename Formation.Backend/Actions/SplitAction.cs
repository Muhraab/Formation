using Formation.Backend.Board;

namespace Formation.Backend.Actions
{
    public class SplitAction : AbstractAction
    {
        public int Position { get; set; }
        public Direction Direction { get; set; }
        protected override ActionId ActionId => ActionId.Split;

        public SplitAction(int position, Direction direction)
        {
            Position = position;
            Direction = direction;
        }

        protected override bool DoAction(GameBoard board)
        {
            var row = board.GetActiveCellRow();

            if (Direction == Direction.Down)
                return MoveDown(row, Position);
            else
                return MoveUp(row, Position);
        }

        private bool MoveUp(CellRow row, int position)
        {
            bool wasMoved = false;

            for (int i = 0; i <= position; i++)
            {
                if (row.Move(i, i - 1))
                    wasMoved = true;
            }

            return wasMoved;
        }

        private bool MoveDown(CellRow row, int position)
        {
            bool wasMoved = false;

            for (int i = 7; i >= position; i--)
            {
                if (row.Move(i, i + 1))
                    wasMoved = true;
            }

            return wasMoved;
        }
    }
}

