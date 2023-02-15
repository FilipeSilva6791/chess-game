﻿using System;
using Chess;
using GameBoard;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {

            try
            {
                Position p = new Position(3, 4);
                Board board = new Board();

                board.InsertPiece(new Tower(board, Color.Black), new Position(0, 0));
                board.InsertPiece(new Tower(board, Color.Black), new Position(1, 3));
                board.InsertPiece(new King(board, Color.Black), new Position(0, 5));

                View.PrintBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();

        }
    }
}
