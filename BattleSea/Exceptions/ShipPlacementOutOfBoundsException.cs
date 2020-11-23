using System;

namespace BattleSea.Exceptions
{
    [Serializable]
    public class ShipPlacementOutOfBoundsException : Exception
    {
        public ShipPlacementOutOfBoundsException() { }

        public ShipPlacementOutOfBoundsException(string message) : base(message) { }

        public ShipPlacementOutOfBoundsException(string message, Exception inner) : base(message, inner) { }
    }
}