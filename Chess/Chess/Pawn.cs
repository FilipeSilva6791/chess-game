using System;
using GameBoard;

namespace Chess
{
    class Pawn : Piece
    {
        private RunGame _game;

        public Pawn(Board board, Color color, RunGame game) : base(color, board)
        {
            _game = game;
        }

        public override bool[,] PossibleMoves()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            if(Color == Color.White)
            {
                pos.DefineValues(Position.Line - 1, Position.Column);
                if(Board.IsValidPosition(pos) && Free(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line - 2, Position.Column);
                if (Board.IsValidPosition(pos) && Free(pos) && Moves == 0)
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line - 1, Position.Column -1);
                if (Board.IsValidPosition(pos) && ExistEnemie(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line - 1, Position.Column +1);
                if (Board.IsValidPosition(pos) && ExistEnemie(pos))
                    mat[pos.Line, pos.Column] = true;

                //ESPECIAL MOVE: En Passant
                if(Position.Line == 3)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.IsValidPosition(left) && ExistEnemie(left) && Board.GetPiece(left) == _game.VulnerableEnPassant)
                        mat[left.Line - 1, left.Column] = true;

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.IsValidPosition(right) && ExistEnemie(right) && Board.GetPiece(right) == _game.VulnerableEnPassant)
                        mat[right.Line - 1, right.Column] = true;

                }
            }
            else
            {
                pos.DefineValues(Position.Line + 1, Position.Column);
                if (Board.IsValidPosition(pos) && Free(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line + 2, Position.Column);
                if (Board.IsValidPosition(pos) && Free(pos) && Moves == 0)
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line + 1, Position.Column - 1);
                if (Board.IsValidPosition(pos) && ExistEnemie(pos))
                    mat[pos.Line, pos.Column] = true;

                pos.DefineValues(Position.Line + 1, Position.Column + 1);
                if (Board.IsValidPosition(pos) && ExistEnemie(pos))
                    mat[pos.Line, pos.Column] = true;

                //ESPECIAL MOVE: En Passant
                if (Position.Line == 4)
                {
                    Position left = new Position(Position.Line, Position.Column - 1);
                    if (Board.IsValidPosition(left) && ExistEnemie(left) && Board.GetPiece(left) == _game.VulnerableEnPassant)
                        mat[left.Line + 1, left.Column] = true;

                    Position right = new Position(Position.Line, Position.Column + 1);
                    if (Board.IsValidPosition(right) && ExistEnemie(right) && Board.GetPiece(right) == _game.VulnerableEnPassant)
                        mat[right.Line + 1, right.Column] = true;

                }

            }

            return mat;
        }

        private bool ExistEnemie(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p != null && p.Color != Color;
        }

        private bool Free (Position pos)
        {
            return Board.GetPiece(pos) == null;
        }

        public override string ToString() => "P";
    }
}