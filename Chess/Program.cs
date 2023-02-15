using System;
using GameBoard;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Position p = new Position(3, 4);
            Board board = new Board();
            View.PrintBoard(board);

            Console.ReadLine();
        }
    }
}
