using Logic.GameField;
using Logic.Strategies;
using Logic.Utils;
using Logic.Utils.Enums;
using NUnit.Framework;

namespace Logic.Tests.Strategies
{
    [TestFixture]
    public class ShootStrategyRandomTest
    {
        [Test]
        public void Shoot_HitCell()
        {
            var field = new Field(5, 5);
            var shootStrategyRandom = new ShootStrategyRandom();
            
            var shootCell = shootStrategyRandom.Shoot(field);


            Assert.True(shootCell.CellState == CellState.Empty 
                        || shootCell.CellState == CellState.DamagedShip);

        }

    }
}