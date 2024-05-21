using Formation.Backend.Board;
using Formation.Backend.GameStates;

namespace Formation.Frontend
{
    internal class Program
    {
        static void Main(string[] args)
        {
            State state = new PlayerTurn(new GameBoard());
            ViewRouter router = new();

            router.Run(state);
        }
    }
}


