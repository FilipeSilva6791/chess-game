namespace GameBoard
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; protected set; }
        public int Moves { get; protected set; }
        public Board Board { get; protected set; }

        public Piece (Color color, Board board)
        {
            Position = null;
            Color = color;
            Board = board;
        }

        public abstract bool[,] PossibleMoves();

        public void AddMoves()
        {
            Moves++;
        }

        public bool ExistPossibleMoves()
        {
            bool[,] mat = PossibleMoves();

            for(int i = 0; i < Board.Lines; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i, j])
                        return true;
                }
            }
            return false;
        }

        protected bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p == null || p.Color != Color;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossibleMoves()[pos.Line, pos.Column];
        }

    }
}
