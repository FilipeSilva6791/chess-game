namespace GameBoard
{
    class Board
    {
        public int Lines { get; } = 8;
        public int Columns { get; } = 8;

        private Piece[,] _pieces;

        public Board()
        {
            _pieces = new Piece[Lines, Columns];
        }

        public Piece GetPiece (int line, int column)
        {
            return _pieces[line, column];
        }

        public void InsertPiece(Piece piece, Position position)
        {
            _pieces[position.Line, position.Column] = piece;
            piece.Position = position;
        }
    }
}
