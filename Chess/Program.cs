using System;
using Chess;
using GameBoard;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            ChessPosition pos = new ChessPosition('c', 7);
            Console.WriteLine(pos.ToPosition());

            Board board = new Board();

            board.InsertPiece(new King(board, Color.Black), new Position(0, 0));
            board.InsertPiece(new King(board, Color.White), new Position(7, 0));

            View.PrintBoard(board);           

            Console.ReadLine();

        }
    }
}
