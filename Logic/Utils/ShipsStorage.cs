using System.Collections.Generic;
using Logic.Utils.Enums;

namespace Logic.Utils
{
    public class ShipsStorage
    {
        public Dictionary<ShipType, int> ShipsAvailable { get; set; } = new Dictionary<ShipType, int>();

        public void AddShip(ShipType shipType, int shipsAmount)
        {
            ShipsAvailable.Add(shipType, shipsAmount);
        }
    }
}