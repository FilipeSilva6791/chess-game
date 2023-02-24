using System.Collections.Generic;
using GameBoard;

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
        public bool Check { get; private set; }
        public Piece VulnerableEnPassant { get; private set; }


        public RunGame()
        {
            Board = new Board();
            Round = 1;
            PlayerColor = Color.White;
            Finished = false;
            Check = false;
            _pieces = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
            InsertPieces();
        }

        private Piece MovePiece(Position origin, Position destiny)
        {
            Piece p = Board.RemovePiece(origin);
            p.AddMoves();
            Piece capturedPiece = Board.RemovePiece(destiny);
            Board.InsertPiece(p, destiny);

            if(capturedPiece != null)
                _capturedPieces.Add(capturedPiece);

            //SPECIAL MOVE : Castling short
            if(p is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestiny = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.AddMoves();
                Board.InsertPiece(rook, rookDestiny);
            }

            //SPECIAL MOVE : Castling long
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestiny = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookOrigin);
                rook.AddMoves();
                Board.InsertPiece(rook, rookDestiny);
            }

            //SPECIAL MOVE : En Passant
            if(p is Pawn)
            {
                if(origin.Column != destiny.Column && capturedPiece == null)
                {
                    Position posPawn;

                    if (p.Color == Color.White)
                        posPawn = new Position(destiny.Line + 1, destiny.Column);
                    else
                        posPawn = new Position(destiny.Line - 1, destiny.Column);

                    capturedPiece = Board.RemovePiece(posPawn);
                    _capturedPieces.Add(capturedPiece);
                }
            }

            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destiny, Piece capturedPiece)
        {
            Piece p = Board.RemovePiece(destiny);
            p.RemoveMoves();
            if (capturedPiece != null)
            {
                Board.InsertPiece(capturedPiece, destiny);
                _capturedPieces.Remove(capturedPiece);
            }
            Board.InsertPiece(p, origin);


            //SPECIAL MOVE : Castling short
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column + 3);
                Position rookDestiny = new Position(origin.Line, origin.Column + 1);
                Piece rook = Board.RemovePiece(rookDestiny);
                rook.RemoveMoves();
                Board.InsertPiece(rook, rookOrigin);
            }

            //SPECIAL MOVE : Castling long
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position rookOrigin = new Position(origin.Line, origin.Column - 4);
                Position rookDestiny = new Position(origin.Line, origin.Column - 1);
                Piece rook = Board.RemovePiece(rookDestiny);
                rook.AddMoves();
                Board.InsertPiece(rook, rookOrigin);
            }

            //SPECIAL MOVE : En Passant
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == VulnerableEnPassant)
                {
                    Piece pawn = Board.RemovePiece(destiny);

                    Position posPawn;

                    if (p.Color == Color.White)
                        posPawn = new Position(3, destiny.Column);
                    else
                        posPawn = new Position(4, destiny.Column);

                    Board.InsertPiece(pawn, posPawn);
                }
            }
        }

        public void StartRound(Position origin, Position destiny)
        {
            Piece capturedPiece = MovePiece(origin, destiny);
            if (IsInCheck(PlayerColor))
            {
                UndoMove(origin, destiny, capturedPiece);
                throw new BoardException("You can't put yourself in check!");
            }

            if (IsInCheck(AdversaryColor(PlayerColor)))
                Check = true;
            else
                Check = false;

            if (IsInCheckmate(AdversaryColor(PlayerColor)))
                Finished = true;
            else
            {
                Round++;
                ChangePlayer();
            }

            Piece p = Board.GetPiece(destiny);

            //SPECIAL MOVE: En Passant
            if (p is Pawn && destiny.Line == origin.Line - 2 || destiny.Line == origin.Line + 2)
                VulnerableEnPassant = p;
            else
                VulnerableEnPassant = null;
            
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
            if(!Board.GetPiece(origin).PossibleMove(destiny))
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

            foreach (Piece x in _pieces)
            {
                if (x.Color == color)
                    aux.Add(x);
            }
            aux.ExceptWith(CapturedPieces(color));

            return aux;
        }

        private Piece King (Color color)
        {
            foreach(Piece x in PiecesInGame(color))
            {
                if (x is King)
                    return x;
            }

            return null;
        }

        private Color AdversaryColor (Color color)
        {
            return color == Color.White ? Color.Black : Color.White;
        }

        public bool IsInCheck(Color color)
        {
            Piece king = King(color);
            
            foreach (Piece x in PiecesInGame(AdversaryColor(color)))
            {
                bool[,] mat = x.PossibleMoves();

                if (mat[king.Position.Line, king.Position.Column])
                    return true;
            }

            return false;
        }

        public bool IsInCheckmate(Color color)
        {
            if(!IsInCheck(color)) 
                return false;

            foreach(Piece x in PiecesInGame(color))
            {
                bool[,] mat = x.PossibleMoves();

                for(int i = 0; i < Board.Lines; i++)
                {
                    for(int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = x.Position;
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = MovePiece(origin, destiny);
                            bool testCheck = IsInCheck(color);
                            UndoMove(origin, destiny, capturedPiece);

                            if (!testCheck) 
                                return false;
                        }
                    }
                }
            }

            return true;
        }

        public void InsertNewPiece(char column, int line, Piece piece)
        {
            Board.InsertPiece(piece, new ChessPosition(column, line).ToPosition());
            _pieces.Add(piece);
        }

        private void InsertPieces()
        {
            InsertNewPiece('a', 1, new Rook(Board, Color.White));
            InsertNewPiece('b', 1, new Knight(Board, Color.White));
            InsertNewPiece('c', 1, new Bishop(Board, Color.White));
            InsertNewPiece('d', 1, new Queen(Board, Color.White));
            InsertNewPiece('e', 1, new King(Board, Color.White, this));
            InsertNewPiece('f', 1, new Bishop(Board, Color.White));
            InsertNewPiece('g', 1, new Knight(Board, Color.White));
            InsertNewPiece('h', 1, new Rook(Board, Color.White));
            InsertNewPiece('a', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('b', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('c', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('d', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('e', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('f', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('g', 2, new Pawn(Board, Color.White, this));
            InsertNewPiece('h', 2, new Pawn(Board, Color.White, this));

            InsertNewPiece('a', 8, new Rook(Board, Color.Black));
            InsertNewPiece('b', 8, new Knight(Board, Color.Black));
            InsertNewPiece('c', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('d', 8, new Queen(Board, Color.Black));
            InsertNewPiece('e', 8, new King(Board, Color.Black, this));
            InsertNewPiece('f', 8, new Bishop(Board, Color.Black));
            InsertNewPiece('g', 8, new Knight(Board, Color.Black));
            InsertNewPiece('h', 8, new Rook(Board, Color.Black));
            InsertNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            InsertNewPiece('h', 7, new Pawn(Board, Color.Black, this));
        }

    }
}
