using System;

namespace BattleSea.Exceptions
{
    [Serializable]
    public class ShootCellOutOfRangeException : Exception
    {
        public ShootCellOutOfRangeException() { }

        public ShootCellOutOfRangeException(string message) : base(message) { }

        public ShootCellOutOfRangeException(string message, Exception inner) : base(message, inner) { }
    }
}