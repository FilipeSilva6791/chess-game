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

        public Piece GetPiece (Position pos)
        {
            return _pieces[pos.Line, pos.Column];
        }

        public bool ExistPieceInPosition(Position pos)
        {
            ValidatePosition(pos);
            return GetPiece(pos) != null;
        }

        public void InsertPiece(Piece piece, Position pos)
        {
            if (ExistPieceInPosition(pos))
                throw new BoardException("There is already a piece in this position!");

            _pieces[pos.Line, pos.Column] = piece;
            piece.Position = pos;
        }

        public Piece RemovePiece(Position pos)
        {
            if (GetPiece(pos) == null)
                return null;

            Piece aux = GetPiece(pos);
            aux.Position = null;
            _pieces[pos.Line, pos.Column] = null;

            return aux;
        }

        public bool IsValidPosition(Position pos)
        {
            if (pos.Line < 0 || pos.Line >= Lines || pos.Column < 0 || pos.Column >= Columns)
                return false;

            return true;
        }

        public void ValidatePosition (Position pos)
        {
            if (!IsValidPosition(pos))
                throw new BoardException("Invalid position !");
        }
    }
}
