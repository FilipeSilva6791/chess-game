using System;
using GameBoard;

namespace Chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(color, board){}

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

            return mat;
        }

        public override string ToString() => "K";
    }
}
