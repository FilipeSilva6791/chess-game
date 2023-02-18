using System;
using GameBoard;

namespace Chess
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(color, board){}

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            pos.DefineValues(Position.Line - 1, Position.Column - 1);
            while(Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
                
                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                    break;

                pos.DefineValues(pos.Line - 1, pos.Column - 1);
            }

            pos.DefineValues(Position.Line - 1, Position.Column + 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                    break;

                pos.DefineValues(pos.Line - 1, pos.Column + 1);
            }

            pos.DefineValues(Position.Line + 1, Position.Column + 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                    break;

                pos.DefineValues(pos.Line + 1, pos.Column + 1);
            }

            pos.DefineValues(Position.Line + 1, Position.Column - 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                    break;

                pos.DefineValues(pos.Line + 1, pos.Column - 1);
            }

            return mat;
        }

        public override string ToString() => "B";
    }
}