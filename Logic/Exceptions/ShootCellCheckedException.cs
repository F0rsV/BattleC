using System;

namespace Logic.Exceptions
{
    [Serializable]
    public class ShootCellCheckedException : Exception
    {
        public ShootCellCheckedException() { }

        public ShootCellCheckedException(string message) : base(message) { }

        public ShootCellCheckedException(string message, Exception inner) : base(message, inner) { }
    }
}