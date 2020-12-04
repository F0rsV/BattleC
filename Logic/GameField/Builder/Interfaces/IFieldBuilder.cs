using Logic.Utils;

namespace Logic.GameField.Builder.Interfaces
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