using Logic.Utils.Enums;

namespace Logic.Utils
{
    public class Cell
    {
        public bool CanPlaceShip { get; set; } = true;
        public CellState CellState { get; set; } = CellState.NotChecked;
        public int X { get; set; }
        public int Y { get; set; }

        public Cell(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}