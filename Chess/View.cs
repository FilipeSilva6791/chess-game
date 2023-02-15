﻿using System;
using GameBoard;

namespace Chess
{
    class View
    {
        public static void PrintBoard(Board board)
        {
            for(int i = 0; i < board.Lines; i++)
            {
                Console.Write(8 - i + " ");

                for(int j = 0; j < board.Columns; j++)
                {
                    if(board.GetPiece(i, j) == null)
                        Console.Write("- "); 
                    else
                        PrintPiece(board.GetPiece(i,j));                    
                }

                Console.WriteLine();
            }

            Console.WriteLine("  a b c d e f g h");
        }

        public static void PrintPiece (Piece piece)
        {
            if (piece.Color == Color.White)
                Console.Write(piece + " ");
            else
            {
                ConsoleColor aux = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(piece + " ");
                Console.ForegroundColor = aux;
            }

        }
    }
}
