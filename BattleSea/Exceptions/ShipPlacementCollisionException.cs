using System;

namespace BattleSea.Exceptions
{
    [Serializable]
    public class ShipPlacementCollisionException : Exception
    {
        public ShipPlacementCollisionException() { }

        public ShipPlacementCollisionException(string message) : base(message) { }

        public ShipPlacementCollisionException(string message, Exception inner) : base(message, inner) { }
    }
}