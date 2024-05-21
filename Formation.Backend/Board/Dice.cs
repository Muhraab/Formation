
using System.Runtime.CompilerServices;

namespace Formation.Backend.Board
{
    public class Dice 
    { 
        public int Value { get; private set; }

        public Dice()
        {
            Roll();
        }

        public void Roll()
        {
            Value = Random.Shared.Next(1,7); 
        }

        public void SetValue(int value)
        {
            if (value > 6) value = 6;
            if (value < 1) value = 1;
            Value = value;
        }
    }
}
