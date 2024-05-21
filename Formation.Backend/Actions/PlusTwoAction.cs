using Formation.Backend.Board;

namespace Formation.Backend.Actions
{
    public class PlusTwoAction : AbstractAction
    {
        public Player Player { get; }
        public int Position { get; }

        protected override ActionId ActionId => ActionId.PlusTwo; //derived get


        public PlusTwoAction(Player player, int position)
        {
            Player = player;
            Position = position;
        }

        protected override bool DoAction(GameBoard board)
        {
            CellRow row = board.GetCellRowFor(Player);

            if (!row.HasDice(Position))
                return false;

            Dice dice = row.Cells[Position].Dice!;
            dice.SetValue(dice.Value + 2);

            return true;
        }
    }

}

