using Logic.Exceptions;
using Logic.GameField;
using Logic.GameField.Builder.Interfaces;
using Logic.Strategies;
using Logic.Utils;
using Logic.Utils.Enums;
using Moq;
using NUnit.Framework;


namespace Logic.Tests.Strategies
{
    [TestFixture]
    public class ShootStrategyAtPointTest
    {
        [TestCase(2, 2)]
        [TestCase(3, 2)]
        public void Shoot_HitShipCell(int shootPointX, int shootPointY)
        {
            var field = new Field(5, 5);
            field.CellsMatrix[shootPointY, shootPointX].CellState = CellState.Ship;


            var shootStrategyAtPoint = new ShootStrategyAtPoint
            {
                ShootPoint = new Point(shootPointX, shootPointY)
            };


            var shootCell = shootStrategyAtPoint.Shoot(field);


            Assert.True(shootCell.CellState == CellState.DamagedShip);

        }


        [TestCase(2, 2)]
        [TestCase(3, 2)]
        public void Shoot_MissShipCell(int shootPointX, int shootPointY)
        {
            var shootStrategyAtPoint = new ShootStrategyAtPoint
            {
                ShootPoint = new Point(shootPointX, shootPointY)
            };
            var field = new Field(5, 5);
            field.CellsMatrix[shootPointY + 1, shootPointX].CellState = CellState.Ship;

            var shootCell = shootStrategyAtPoint.Shoot(field);


            Assert.True(shootCell.CellState == CellState.Empty);

        }

        [TestCase(6, 0)]
        [TestCase(0, 6)]
        [TestCase(-2, 6)]
        [TestCase(42, -2)]
        public void Shoot_ThrowsShootCellOutOfRangeException(int shootPointX, int shootPointY)
        {
            var shootStrategyAtPoint = new ShootStrategyAtPoint
            {
                ShootPoint = new Point(shootPointX, shootPointY)
            };
            var field = new Field(5, 5);

            void Action() => shootStrategyAtPoint.Shoot(field);

            Assert.Throws<ShootCellOutOfRangeException>(Action);
        }


        [TestCase(3, 0)]
        [TestCase(2, 1)]
        public void Shoot_OnDamagedShip_ThrowsShootCellCheckedException(int shootPointX, int shootPointY)
        {
            var shootStrategyAtPoint = new ShootStrategyAtPoint
            {
                ShootPoint = new Point(shootPointX, shootPointY)
            };
            var field = new Field(5, 5);
            field.CellsMatrix[shootPointY, shootPointX].CellState = CellState.DamagedShip;

            void Action() => shootStrategyAtPoint.Shoot(field);

            Assert.Throws<ShootCellCheckedException>(Action);
        }


        [TestCase(2, 1)]
        [TestCase(4, 0)]
        public void Shoot_OnEmptyCell_ThrowsShootCellCheckedException(int shootPointX, int shootPointY)
        {
            var shootStrategyAtPoint = new ShootStrategyAtPoint
            {
                ShootPoint = new Point(shootPointX, shootPointY)
            };
            var field = new Field(5, 5);
            field.CellsMatrix[shootPointY, shootPointX].CellState = CellState.Empty;

            void Action() => shootStrategyAtPoint.Shoot(field);

            Assert.Throws<ShootCellCheckedException>(Action);
        }

    }
}