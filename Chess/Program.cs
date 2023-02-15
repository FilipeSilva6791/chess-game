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
                    try
                    {
                        Console.Clear();
                        View.PrintGame(game);

                        Console.Write("\nOrigin: ");
                        Position origin = View.ReadPosition().ToPosition();
                        game.ValidateOriginPosition(origin);

                        bool[,] possibleMoves = game.Board.GetPiece(origin).PossibleMoves();

                        Console.Clear();
                        View.PrintBoard(game.Board, possibleMoves);

                        Console.Write("\nDestiny: ");
                        Position destiny = View.ReadPosition().ToPosition();
                        game.ValidateDestinyPosition(origin, destiny);

                        game.StartRound(origin, destiny);
                    }
                    catch (BoardException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Type the column letter first and then the line number ! Example: 'a1'");
                        Console.ReadLine();
                    }
                    catch(IndexOutOfRangeException)
                    {
                        Console.WriteLine("Enter a possible value !");
                        Console.ReadLine();
                    }

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
