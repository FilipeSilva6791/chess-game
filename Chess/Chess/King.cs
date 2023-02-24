using System;
using GameBoard;

namespace Chess
{
    class King : Piece
    {
        private RunGame _game;

        public King(Board board, Color color, RunGame game) : base(color, board){
            _game = game;
        }

        private bool TestRookForCastling(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p != null && p is Rook && p.Color == Color && p.Moves == 0;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            //up
            pos.DefineValues(Position.Line - 1, Position.Column);
            if (Board.IsValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //up-right
            pos.DefineValues(Position.Line - 1, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //right
            pos.DefineValues(Position.Line, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //down-right
            pos.DefineValues(Position.Line + 1, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //down
            pos.DefineValues(Position.Line + 1, Position.Column);
            if (Board.IsValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //down-left
            pos.DefineValues(Position.Line + 1, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //left
            pos.DefineValues(Position.Line, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //up-left
            pos.DefineValues(Position.Line - 1, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
                mat[pos.Line, pos.Column] = true;

            //SPECIAL MOVE: Castling 
            if(Moves == 0 && !_game.Check)
            {
                //Castling short
                Position posRook = new Position(Position.Line, Position.Column + 3);

                if (TestRookForCastling(posRook))
                {
                    Position p1 = new Position(Position.Line, Position.Column + 1);
                    Position p2 = new Position(Position.Line, Position.Column + 2);

                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null)
                        mat[Position.Line, Position.Column + 2] = true;
                }

                //Castling long
                Position posRook2 = new Position(Position.Line, Position.Column - 4);

                if (TestRookForCastling(posRook2))
                {
                    Position p1 = new Position(Position.Line, Position.Column - 1);
                    Position p2 = new Position(Position.Line, Position.Column - 2);
                    Position p3 = new Position(Position.Line, Position.Column - 3);

                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null && Board.GetPiece(p3) == null)
                        mat[Position.Line, Position.Column - 2] = true;
                }
            }

            return mat;
        }

        public override string ToString() => "K";
    }
}
