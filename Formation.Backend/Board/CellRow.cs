using System.CodeDom.Compiler;

namespace Formation.Backend.Board
{
    public class CellRow
    {
        public Cell[] Cells { get; set; } =
            [
                new Cell(),
                new Cell(),
                new Cell(),
                new Cell(),
                new Cell(),
                new Cell(),
                new Cell(),
                new Cell(),
            ];

        public CellRow()
        {
            SetupInitialPosition(); //constuctor
        }

        public void SetCell(int position, Dice dice)
        {
            Cells[position].Dice = dice;
        }


        public void ClearCell(int position)
        {
            Cells[position].Dice = null;
        }

        public Dice[] SortedDice(int NumberOfDice)
        {
            Dice[] result = new Dice[NumberOfDice];
            for (int i = 0; i < NumberOfDice; i++)
            {
                result[i] = new Dice();
            }
            Array.Sort(result, (a, b) => a.Value - b.Value);
            Array.Reverse(result);
            return result;
        }

        public bool Move(int from, int to)
        {
            if (from >= Cells.Length || from < 0)
                return false;

            if (to >= Cells.Length || to < 0)
                return false;

            if (from == to)
                return false;
            
            if (Cells[from].IsEmpty) 
                return false;

            if (!Cells[to].IsEmpty) 
                return false;

            SetCell(to, Cells[from].Dice!);
            ClearCell(from);

            return true;
        }

        public bool SwitchWithOpposite(int position, CellRow oppositeRow)
        {
            if (oppositeRow is null)
                return false;

            if (Cells[position].IsEmpty || oppositeRow.Cells[position].IsEmpty)
                return false;

            var temp = oppositeRow.Cells[position].Dice!;

            oppositeRow.SetCell(position, Cells[position].Dice!);
            SetCell(position, temp);

            return true;
        }

        public void SetupInitialPosition()
        {
            Dice[] StartDice = SortedDice(5);
            for (int i = 0; i < StartDice.Length; i++)
            {
                SetCell(i + 1, StartDice[i]);
            }
        }

        public int CellValue(int position)
        {
            Dice? Dice = Cells[position].Dice;
            if (Dice is null) 
            {
                return 0;  
            }
            return Dice.Value;
        }

        public bool HasDice(int position)
        {
            return !Cells[position].IsEmpty;
        }
    }
}
