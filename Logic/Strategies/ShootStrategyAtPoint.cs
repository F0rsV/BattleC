using Logic.Exceptions;
using Logic.GameField;
using Logic.Strategies.Interfaces;
using Logic.Utils;
using Logic.Utils.Enums;

namespace Logic.Strategies
{
    public class ShootStrategyAtPoint : IShootStrategy
    {
        public Point ShootPoint { get; set; }

        public Cell Shoot(Field field)
        {
            CellState cellState;
            try
            {
                cellState = field.CellsMatrix[ShootPoint.Y, ShootPoint.X].CellState;
            }
            catch
            {
                throw new ShootCellOutOfRangeException("Cell is out of range.");
            }

            if (cellState == CellState.NotChecked)
            {
                field.CellsMatrix[ShootPoint.Y, ShootPoint.X].CellState = CellState.Empty;

            }
            else if (cellState == CellState.Ship)
            {
                field.CellsMatrix[ShootPoint.Y, ShootPoint.X].CellState = CellState.DamagedShip;
            }
            else
            {
                throw new ShootCellCheckedException("This cell is already checked.");
            }


            return field.CellsMatrix[ShootPoint.Y, ShootPoint.X];
        }
    }
}