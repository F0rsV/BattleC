using System;

namespace Logic.Exceptions
{
    [Serializable]
    public class ShipPlacementCollisionException : Exception
    {
        public ShipPlacementCollisionException() { }

        public ShipPlacementCollisionException(string message) : base(message) { }

        public ShipPlacementCollisionException(string message, Exception inner) : base(message, inner) { }
    }
}