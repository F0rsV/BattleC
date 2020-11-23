using System;
using System.Collections.Generic;
using System.Linq;
using BattleSea.Builder.Enums;

namespace BattleSea.Builder
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