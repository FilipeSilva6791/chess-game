namespace Board
{
    class Board
    {
        private const int Lines = 8;
        private const int Columns = 8;

        private Piece[,] Pieces;

        public Board()
        {
            Pieces = new Piece[Lines, Columns];
        }
    }
}
