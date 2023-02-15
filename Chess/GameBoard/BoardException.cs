using System;

namespace GameBoard
{
    class BoardException : ApplicationException
    {
        public BoardException(string message) : base(message) { }
    }
}
