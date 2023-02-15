using System;
using GameBoard;

namespace Chess
{
    class RunGame
    {
        public Board Board { get; private set; }
        public int Round { get; private set; }
        public Color PlayerColor { get; private set; }
        public bool Finished { get; private set; }

        public RunGame()
        {
            Board = new Board();
            Round = 1;
            PlayerColor = Color.White;
            Finished = false;
            InsertPieces();
        }

        private void MovePiece(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.AddMoves();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(p, destiny);
        }

        public void StartRound(Position origin, Position destiny)
        {
            MovePiece(origin, destiny);
            Round++;
            ChangePlayer();
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.GetPiece(pos) == null)
                throw new BoardException("There is no piece in this position!");

            if(PlayerColor != Board.GetPiece(pos).Color)
                throw new BoardException("This piece is not yours!");

            if (!Board.GetPiece(pos).ExistPossibleMoves())
                throw new BoardException("There are no moves possible for this piece!");
        }

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if(!Board.GetPiece(origin).CanMoveTo(destiny))
                throw new BoardException("Invalid destiny position!");
        }

        private void ChangePlayer()
        {
            if (PlayerColor == Color.White)
                PlayerColor = Color.Black;
            else
                PlayerColor = Color.White;
        }

        private void InsertPieces()
        {
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('c', 1).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('e', 1).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('c', 2).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('d', 2).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.White), new ChessPosition('e', 2).ToPosition());
            Board.InsertPiece(new King(Board, Color.White), new ChessPosition('d', 1).ToPosition());

            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('c', 8).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('e', 8).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('c', 7).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('d', 7).ToPosition());
            Board.InsertPiece(new Tower(Board, Color.Black), new ChessPosition('e', 7).ToPosition());
            Board.InsertPiece(new King(Board, Color.Black), new ChessPosition('d', 8).ToPosition());
        }

    }
}
