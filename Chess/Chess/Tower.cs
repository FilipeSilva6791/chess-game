using System;
using GameBoard;

namespace Chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(color, board) { }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            //up
            pos.DefineValues(Position.Line - 1, Position.Column);
            while(Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                    break;

                pos.Line--;
            }

            //down
            pos.DefineValues(Position.Line + 1, Position.Column);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                    break;

                pos.Line++;
            }

            //right
            pos.DefineValues(Position.Line, Position.Column + 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                    break;

                pos.Column++;
            }

            //left
            pos.DefineValues(Position.Line, Position.Column - 1);
            while (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                    break;

                pos.Column--;
            }

            return mat;
        }

        public override string ToString() => "T";
    }
}
