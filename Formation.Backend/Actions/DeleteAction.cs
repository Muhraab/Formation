using Formation.Backend.Board;

namespace Formation.Backend.Actions
{
    public class DeleteAction : AbstractAction
    {
        public int Position { get; set; }

        protected override ActionId ActionId => ActionId.Delete;

        public Player Player { get; }

        public DeleteAction(Player player, int position)
        {
            Player = player;
            Position = position;
        }

        protected override bool DoAction(GameBoard board)
        {
            if (!board.GetCellRowFor(Player).HasDice(Position))
                return false;

            board
                .GetCellRowFor(Player)
                .ClearCell(Position);

            return true;
        }


        //Metode der skal sætte værdien af position i en celle til Null. Der skal både vælges række og position.

    }

}

