using System;
using GameBoard;

namespace Chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(color, board){}

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            if(Color == Color.White)
            {
                pos.DefineValues(Position.Line - 1, Position.Column);
                if(Board.IsValidPosition(pos) && Free(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line - 2, Position.Column);
                if (Board.IsValidPosition(pos) && Free(pos) && Moves == 0)
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line - 1, Position.Column -1);
                if (Board.IsValidPosition(pos) && ExistEnemie(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line - 1, Position.Column +1);
                if (Board.IsValidPosition(pos) && ExistEnemie(pos))
                    mat[pos.Line, pos.Column] = true;
            }
            else
            {
                pos.DefineValues(Position.Line + 1, Position.Column);
                if (Board.IsValidPosition(pos) && Free(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line + 2, Position.Column);
                if (Board.IsValidPosition(pos) && Free(pos) && Moves == 0)
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line + 1, Position.Column - 1);
                if (Board.IsValidPosition(pos) && ExistEnemie(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Board.IsValidPosition(pos) && ExistEnemie(pos))
                    mat[pos.Line, pos.Column] = true;

            }

            return mat;
        }

        private bool ExistEnemie(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p != null && p.Color != Color;
        }

        private bool Free (Position pos)
        {
            return Board.GetPiece(pos) == null;
        }

        public override string ToString() => "P";
    }
}