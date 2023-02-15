using System.Collections.Generic;
using GameBoard;
using System.Linq;

namespace Chess
{
    class RunGame
    {
        public Board Board { get; private set; }
        public int Round { get; private set; }
        public Color PlayerColor { get; private set; }
        public bool Finished { get; private set; }
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _capturedPieces;


        public RunGame()
        {
            Board = new Board();
            Round = 1;
            PlayerColor = Color.White;
            Finished = false;
            _pieces = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
            InsertPieces();
        }

        private void MovePiece(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.AddMoves();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(p, destiny);

            if(capturedPiece != null)
                _capturedPieces.Add(capturedPiece);
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

        public HashSet<Piece> CapturedPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach(Piece x in _capturedPieces)
            {
                if (x.Color == color)
                    aux.Add(x);
            }

            return aux;
        }

        public HashSet<Piece> PiecesInGame(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece x in _capturedPieces)
            {
                if (x.Color == color)
                    aux.Add(x);
            }
            aux.ExceptWith(CapturedPieces(color));

            return aux;
        }

        public void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            _pieces.Add(piece);
        }

        private void InsertPieces()
        {
            InsertNewPiece('c', 1, new Tower(Board, Color.White));
            InsertNewPiece('e', 1, new Tower(Board, Color.White));
            InsertNewPiece('c', 2, new Tower(Board, Color.White));
            InsertNewPiece('d', 2, new Tower(Board, Color.White));
            InsertNewPiece('e', 2, new Tower(Board, Color.White));
            InsertNewPiece('d', 1, new King(Board, Color.White));

            InsertNewPiece('c', 8, new Tower(Board, Color.Black));
            InsertNewPiece('e', 8, new Tower(Board, Color.Black));
            InsertNewPiece('c', 7, new Tower(Board, Color.Black));
            InsertNewPiece('d', 7, new Tower(Board, Color.Black));
            InsertNewPiece('e', 7, new Tower(Board, Color.Black));
            InsertNewPiece('d', 8, new King(Board, Color.Black));
        }

    }
}
