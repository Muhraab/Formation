using Formation.Backend.GameStates;

namespace Formation.Frontend
{
    public class ViewRouter
    {
        public void Run(State? state)
        {
            while (state is not null)
            {
                Console.Clear();
                var view = SwitchView(state);
                state = view.Render();
            }
        }

        public View SwitchView(State state) 
        { 
            switch (state)
            {
                case PlayerTurn playerTurn:
                    return new PlayerTurnView(playerTurn);

                case EndGame endGame:
                    return new EndGameView(endGame);

                default:
                    throw new Exception("No view for state!");
            }
        }
	}
}


