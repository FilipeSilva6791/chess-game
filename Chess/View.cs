using System;
using System.Collections.Generic;
using GameBoard;

namespace Chess
{
    class View
    {
        public static void PrintGame(RunGame game)
        {
            PrintBoard(game.Board);
            PrintCapturedPieces(game);
            Console.WriteLine("\n\nRound: " + game.Round);

            if (!game.Finished)
            {
                Console.WriteLine("Current player: " + game.PlayerColor);

                if (game.Check)
                    Console.WriteLine("UUhh, CHECK!");
            }
            else
            {
                Console.WriteLine("CHECKMATE !! ");
                Console.WriteLine("Winner: " + game.PlayerColor);
            }          
        }
        public static void PrintBoard(Board board)
        {
            for(int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");

                for(int j = 0; j < board.Columns; j++)
                {
                    PrintPiece(board.GetPiece(i, j));
                }
                                       
                Console.WriteLine();
            }

            Console.WriteLine("   a  b  c  d  e  f  g  h");
        }

        public static void PrintCapturedPieces(RunGame game)
        {
            Console.WriteLine("\nCaptured pieces:");
            Console.Write("Whites: ");
            PrintSet(game.CapturedPieces(Color.White));

            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\nBlacks: ");
            PrintSet(game.CapturedPieces(Color.Black));
            Console.ForegroundColor = aux;
        }

        public static void PrintSet(HashSet<Piece> set)
        {
            Console.Write("[");

            foreach(Piece x in set)
            {
                Console.Write(x + " ");
            }

            Console.Write("]");
        }

        public static void PrintBoard(Board board, bool[,] possibleMoves)
        {
            ConsoleColor originalBackground = Console.BackgroundColor;
            ConsoleColor alteredBackground = ConsoleColor.DarkGray;

            for (int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");

                for (int j = 0; j < board.Columns; j++)
                {
                    if (possibleMoves[i, j])
                        Console.BackgroundColor = alteredBackground;
                    else
                        Console.BackgroundColor = originalBackground;

                    PrintPiece(board.GetPiece(i, j));
                    Console.BackgroundColor = originalBackground;
                }

                Console.WriteLine();
            }

            Console.WriteLine("   a  b  c  d  e  f  g  h");
            Console.BackgroundColor = originalBackground;
        }

        public static ChessPosition ReadPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int line = int.Parse(s[1] + "");

            return new ChessPosition(column, line);
        }

        private static void PrintPiece (Piece piece)
        {
            if (piece == null)
                Console.Write("[ ]");
            else
            {
                if (piece.Color == Color.White)
                    Console.Write("[" + piece + "]");
                else
                {
                    Console.Write("[");
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write(piece);
                    Console.ForegroundColor = aux;
                    Console.Write("]");
                }
            }
        }
    }
}
