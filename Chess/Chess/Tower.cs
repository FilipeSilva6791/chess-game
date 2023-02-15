﻿using System;
using GameBoard;

namespace Chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(color, board) { }

        public override string ToString() => "T";
    }
}