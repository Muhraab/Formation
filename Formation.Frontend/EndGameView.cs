using Formation.Backend.Board;
using Formation.Backend.GameStates;

namespace Formation.Frontend
{
    public class EndGameView : View
    {
        public EndGame EndGame { get; set; }

        public EndGameView(EndGame endGame)
        {
            EndGame = endGame;
        }

        public override State? Render()
        {
            Score score = EndGame.Board.GetScore();

            Console.WriteLine(score.Leading + " has won with " + score.TotalScore + " points.");
            Console.WriteLine("Press any key to start new game.");

            Console.ReadKey();

            return new PlayerTurn(new GameBoard());
        }
    }
}


