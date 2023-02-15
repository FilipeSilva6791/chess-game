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

        public void AddMoves()
        {
            Moves++;
        }
    }
}
