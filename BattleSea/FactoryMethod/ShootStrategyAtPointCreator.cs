using BattleSea.Builder;
using BattleSea.Strategy;
using BattleSea.Strategy.Interfaces;

namespace BattleSea.FactoryMethod
{
    public class ShootStrategyAtPointCreator : StrategyCreator
    {
        public Point ShootPoint { get; set; }

        public override IShootStrategy FactoryMethod()
        {
            return new ShootStrategyAtPoint(){ShootPoint = ShootPoint};
        }


        public ShootStrategyAtPointCreator(Point shootPoint)
        {
            ShootPoint = shootPoint;
        }


    }
}