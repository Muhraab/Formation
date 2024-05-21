using Formation.Backend.Board;
using System.Collections.Specialized;

namespace Formation.Backend.Actions
{
    public class FourAction: AbstractAction
    {
        public int Position { get; }

        public Player Player { get; }

        protected override ActionId ActionId => ActionId.Four;

        public FourAction(int position, Player player)
        {
            Position = position;
            Player = player;
        }

        protected override bool DoAction(GameBoard board)
        {
            if( !board.GetCellRowFor(Player).HasDice(Position))
                return false;
            
            board.GetCellRowFor(Player).Cells[Position].Dice!.SetValue(4);

            return true;
        }
    }
}

