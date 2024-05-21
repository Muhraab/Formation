using Formation.Backend.Board;

namespace Formation.Backend.Actions
{
    public class ChanceAction : AbstractAction

    {
        public int Position { get; }

        public Player Player { get; }

        protected override ActionId ActionId => ActionId.Chance;

        public ChanceAction(int position, Player player)
        {
            Position = position;
            Player = player;
        }

        protected override bool DoAction(GameBoard board)
        {
            if (!board.GetCellRowFor(Player).HasDice(Position))
                return false;

            board.GetCellRowFor(Player).Cells[Position].Dice!.Roll();

            return true;
        }
    }

}

