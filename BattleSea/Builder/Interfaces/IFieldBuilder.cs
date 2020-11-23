using System.Collections.Generic;
using BattleSea.Builder.Enums;
using BattleSea.Model;

namespace BattleSea.Builder.Interfaces
{
    public interface IFieldBuilder
    {
        void Reset();
        void SetDimension(int height, int width);
        void SetShipsStorage(ShipsStorage shipsStorage);
        void AddShip(ShipPlacementInfo shipPlacementInfo);
        Field GetResult();
    }
}