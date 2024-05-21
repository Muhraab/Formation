using Formation.Backend.Actions;

namespace Formation.Backend.Board
{
    public class GameBoard
    {

        public CellRow CellRowWhite { get; set; } = new CellRow();
        public CellRow CellRowBlack { get; set; } = new CellRow();

        public ActionId[] AvailableActions { get; set; } = [ActionId.PlusTwo, ActionId.Betray, ActionId.Delete, ActionId.Four, ActionId.Rokade, ActionId.Chance, ActionId.Split, ActionId.Move];

        public ActionId[] SpentActions { get; set; } = [];

        public bool IsGameEnded => AvailableActions.Length == 0;

        //CellRow x2, 

        //Array available moves - moves minus brugte moves
        //Array med brugte moves- moves overføres fra Available når de bruges

        public void MoveActionToSpent(ActionId ActionId) //Flytter en Action fra tilgængelige til utilgængelige
        {
            int index = Array.IndexOf(AvailableActions, ActionId);
            if (index == -1)
                throw new InvalidOperationException("Action unavailable");

            SpentActions = [..SpentActions, ActionId];
            AvailableActions = [..AvailableActions[..index], ..AvailableActions[(index + 1)..]];
        }

        public Score GetScore()
        {
            return new Score(this);
        }

        public CellRow GetCellRowFor(Player player)
        {
            if (player == Player.White)
                return CellRowWhite; 
            
            if(player == Player.Black) 
                return CellRowBlack;

            throw new ArgumentOutOfRangeException(nameof(player));
        }

        public Player Opposite (Player player)
        {
            if(player == Player.Black)
                return Player.White;
            if(player== Player.White)
                return Player.Black; 
            
            throw new ArgumentOutOfRangeException(nameof(player)); 
        }

        public Player GetActivePlayer()
        {
            Player Leading = GetScore().Leading;

            if (Leading == Player.None)
                return Player.White;

            return Opposite(Leading);


        }

        public CellRow GetActiveCellRow()
        {
            return GetCellRowFor (GetActivePlayer());
           
        }

        public CellRow GetPassiveCellRow()
        {
            return GetCellRowFor(Opposite(GetActivePlayer())); 
            
        }
    }

    
}

