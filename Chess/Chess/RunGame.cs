using System;
using GameBoard;

namespace Chess
{
    class RunGame
    {
        public Board Board { get; }
        private int Round;
        private Color PlayerColor;
        public bool Finished { get; }

        public RunGame()
        {
            Board = new Board();
            Round = 1;
            PlayerColor = Color.White;
            Finished = false;
            InsertPieces();
        }

        public void MovePiece(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.AddMoves();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(p, destiny);
        }

        private void InsertPieces()
        {
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('a', 1).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('h', 1).ToPosition());
        }

    }
}
