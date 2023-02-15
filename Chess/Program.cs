using System;
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
                RunGame game = new RunGame();

                while (!game.Finished)
                {
                    Console.Clear();
                    View.PrintBoard(game.Board);

                    Console.Write("\nOrigin: ");
                    Position origin = View.ReadPosition().ToPosition();

                    bool[,] possibleMoves = game.Board.GetPiece(origin).PossibleMoves();

                    Console.Clear();
                    View.PrintBoard(game.Board, possibleMoves);

                    Console.Write("\nDestiny: ");
                    Position destiny = View.ReadPosition().ToPosition();

                    game.MovePiece(origin, destiny);
                }

                View.PrintBoard(game.Board);
            }
            catch(BoardException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();

        }
    }
}
