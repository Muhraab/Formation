
namespace Formation.Backend.Board
{
    public class Cell
    {
        public Dice? Dice { get; set; }

        public bool IsEmpty => Dice is null;
    }
}
