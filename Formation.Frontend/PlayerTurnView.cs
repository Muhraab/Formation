using Formation.Backend.Actions;
using Formation.Backend.Board;
using Formation.Backend.GameStates;
using System;

namespace Formation.Frontend
{
    public class PlayerTurnView : View
    {
        public PlayerTurn PlayerTurn { get; set; }

        public PlayerTurnView(PlayerTurn playerTurn)
        {
            PlayerTurn = playerTurn;
        }

        public override State? Render()
        {

            Console.Write("Position index:  ");

            for (int i = 1; i <= 8; i++) 
                Console.Write(i.ToString() + " ");    

            Console.WriteLine();
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("White player:    " + ShowDiceRow(PlayerTurn.Board.CellRowWhite));

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Black player:    " + ShowDiceRow(PlayerTurn.Board.CellRowBlack));

            Console.ResetColor();

            Console.WriteLine();
            Console.Write("Score:           ");

            var score = PlayerTurn.Board.GetScore();

            for (int i = 0; i < 8; i++)
            {
                var positionScore = score.GetPositionScore(i);
                
                if (positionScore.Leading == Player.White)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                }
                else if (positionScore.Leading == Player.Black) 
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.ResetColor();
                }

                Console.Write(positionScore.Score + " ");
                Console.ResetColor();
            }

            Console.Write(" = " + score.Leading + " is leading with " + score.TotalScore + " points.");

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Player turn: " + PlayerTurn.Board.GetActivePlayer());

            Console.WriteLine();
            Console.WriteLine("Available moves:");

            var nextState = AwaitUserInputs();

            return nextState;
        }

        private string ShowDiceRow(CellRow row)
        {
            string result = string.Empty;

            for (int i = 0; i < 8; i++)
            {
                Cell cell = row.Cells[i];

                if (cell.IsEmpty)
                    result += "- ";
                else
                    result += cell.Dice!.Value.ToString() + " ";
            }

            return result;
        }

        private State AwaitUserInputs()// i user inputs skal den nuværende liste udvides til at unkludere alle moves'ne. Derefter skal casene udvides til at inkludere alle readkeys
        {
           
            Console.WriteLine();


            PresentIfAvailable(ActionId.Betray, "(B)etray: Switch the die in your row with the opposite die(ind the opponents row)");
            PresentIfAvailable(ActionId.Four, "(F)our: Turn any die to show the value of 4");
            PresentIfAvailable(ActionId.Chance, "(C)hance: Reroll any single die");
            PresentIfAvailable(ActionId.Delete, "(D)elete: Remove a single die");
            PresentIfAvailable(ActionId.Move, "(M)ove: Move entire row one step down.");
            PresentIfAvailable(ActionId.PlusTwo, "(P)lus Two: Turn a single die to show a value of +2 higher than the current value");
            PresentIfAvailable(ActionId.Rokade, "(R)okade: Move yor top-most die down below your bottom-most die, or vice versa.");
            PresentIfAvailable(ActionId.Split, "(S)plit: Choose a die, and move either the top up, or the bottom down. The chosen die moves with them.");

            ConsoleKeyInfo key = Console.ReadKey();

            switch (key.Key)
            {
                case ConsoleKey.M:
                    return PerformMoveAction();

                case ConsoleKey.F:
                    return PerformFourAction();

                case ConsoleKey.P:
                    return PerformPlusTwoAction();
                
                case ConsoleKey.B:
                    return PerformBetrayAction();

                case ConsoleKey.D:
                    return PerformDeleteAction();

                case ConsoleKey.C:
                    return PerformChanceAction();

                case ConsoleKey.R:
                    return PerformRokadeAction();

                case ConsoleKey.S:
                    return PerformSplitAction();


                default:
                    return PlayerTurn;
            }
        }

        private void PresentIfAvailable(ActionId action, string presentation)
        {
            if (PlayerTurn.Board.AvailableActions.Contains(action))
            {
                Console.WriteLine(presentation);
            }
        }

        private State PerformBetrayAction()
        {
            
            int position = AskPosition();
            var action = new BetrayAction(position);
            return PlayerTurn.TakeAction(action);
                
        }

        private State PerformChanceAction()
        {
            Player player = AskPlayer();
            int position = AskPosition();
            var action = new ChanceAction(position,player);
            return PlayerTurn.TakeAction(action);
        }


        private State PerformDeleteAction() 
        {
            Player player = AskPlayer();
            int position = AskPosition();
            var action = new DeleteAction(player,position);
            return PlayerTurn.TakeAction(action); 
        }

        private State PerformFourAction()
        {
            Player player = AskPlayer();
            int position = AskPosition();
            var action = new FourAction(position, player);
            return PlayerTurn.TakeAction(action);
        }


        private State PerformMoveAction()
        {
            var action = new MoveAction();
            return PlayerTurn.TakeAction(action);
        }

        private State PerformPlusTwoAction() //Virker kun nogle gange???
        {
            Player player = AskPlayer();
            int position = AskPosition();
            var action = new PlusTwoAction(player, position);
            return PlayerTurn.TakeAction(action);

        }

       private State PerformRokadeAction ()
        {
            var direction = AskDirection();
            var action = new RokadeAction(direction);
            
            return PlayerTurn.TakeAction(action);
            
        }

        private State PerformSplitAction()
        {
            int position = AskPosition();
            Direction direction = AskDirection(); 
            var action = new SplitAction(position, direction);
            return PlayerTurn.TakeAction(action); 
        }

        private Player AskPlayer()
        {
            while (true)
            {
                Console.WriteLine("(B)lack or (W)hite?");
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.W)
                {
                    return Player.White;
                }
                else if (key == ConsoleKey.B) 
                {
                    return Player.Black; 
                }
            }
        }

      
        private int AskPosition()
        {
            while (true)
            {
                Console.WriteLine("Position (1-8)?");
                string? answer = Console.ReadLine();

                if (int.TryParse(answer, out int result)) 
                { 
                    if (result >= 1 && result <= 8)
                    {
                        return result - 1;
                    }
                } 

            }
        }

        private Direction AskDirection() //direction - ask up/down skal returnere en direction. Jeg kan ikke finde ud af at indstille direction som parameter til RokadeAction
        {
            while(true)
            {
                Console.WriteLine("(L)eft or (R)ight?");
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.L)
                {
                    return Direction.Up;
                }
                else if (key == ConsoleKey.R)
                {
                    return Direction.Down;
                }
            }
        }

    }
}


