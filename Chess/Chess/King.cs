using System;
using GameBoard;

namespace Chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(color, board){}

        public override string ToString() => "K";
    }
}
