namespace Formation.Backend.Board
{
    public class Score
    {
        public Player Leading { get; private set; }
        public int TotalScore { get; private set; }
        public PositionScore[] PositionScores { get; set; } = new PositionScore[8];

        public Score(GameBoard board)
        {
            Calculate(board);
        }

        private void Calculate(GameBoard board)
        {
            int totalWhite = 0;
            int totalBlack = 0;

            for (int i = 0; i < board.CellRowWhite.Cells.Length; i++)
            {
                var position = new PositionScore();

                var white = board.CellRowWhite.CellValue(i);
                var black = board.CellRowBlack.CellValue(i);

                if (white == 0 || black == 0 || white == black)
                {
                    position.Score = 0;
                    position.Leading = Player.None;
                }
                else
                {
                    var diff = white - black;
                    position.Score = Math.Abs(diff);
                    
                    if (white > black)
                    {
                        position.Leading = Player.White;
                        totalWhite = totalWhite + position.Score;
                    }
                    else
                    {
                        position.Leading = Player.Black;
                        totalBlack = totalBlack + position.Score;
                    }
                }

                PositionScores[i] = position;
            }

            if (totalWhite > totalBlack)
            {
                Leading = Player.White;
            }
            else if (totalBlack > totalWhite)
            {
                Leading = Player.Black;
            }
            else
            {
                Leading = Player.None;
            }

            TotalScore = Math.Abs(totalWhite - totalBlack);
        }

        public PositionScore GetPositionScore(int position)
        {
            return PositionScores[position];
        }


    }
}

