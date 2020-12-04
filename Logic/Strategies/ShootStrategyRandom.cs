using System;
using Logic.GameField;
using Logic.Strategies.Interfaces;
using Logic.Utils;
using Logic.Utils.Enums;

namespace Logic.Strategies
{
    public class ShootStrategyRandom : IShootStrategy
    {
        public Cell Shoot(Field field)
        {
            int maxY = field.CellsMatrix.GetUpperBound(0) - field.CellsMatrix.GetLowerBound(0);
            int maxX = field.CellsMatrix.GetUpperBound(1) - field.CellsMatrix.GetLowerBound(1);

            Random random = new Random();
            Point randomPoint = new Point(random.Next(0, maxX), random.Next(0, maxY));

            while (field.CellsMatrix[randomPoint.Y, randomPoint.X].CellState == CellState.DamagedShip ||
                   field.CellsMatrix[randomPoint.Y, randomPoint.X].CellState == CellState.Empty)
            {
                randomPoint.X = random.Next(0, maxX + 1);
                randomPoint.Y = random.Next(0, maxY + 1);
            }


            var cellState = field.CellsMatrix[randomPoint.Y, randomPoint.X].CellState;

            if (cellState == CellState.NotChecked)
            {
                field.CellsMatrix[randomPoint.Y, randomPoint.X].CellState = CellState.Empty;
            }
            else if (cellState == CellState.Ship)
            {
                field.CellsMatrix[randomPoint.Y, randomPoint.X].CellState = CellState.DamagedShip;
            }


            return field.CellsMatrix[randomPoint.Y, randomPoint.X];
        }
    }
}