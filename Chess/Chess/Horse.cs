using System;
using GameBoard;

namespace Chess
{
    class Horse : Piece
    {
        public Horse(Board board, Color color) : base(color, board){}

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            pos.DefineValues(Position.Line - 1, Position.Column - 2);
            if(Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line - 2, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line - 2, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line - 1, Position.Column + 2);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line + 1, Position.Column + 2);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line + 2, Position.Column + 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line + 2, Position.Column - 1);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            pos.DefineValues(Position.Line + 1, Position.Column - 2);
            if (Board.IsValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Line, pos.Column] = true;
            }

            return mat;
        }

        public override string ToString() => "H";
    }
}