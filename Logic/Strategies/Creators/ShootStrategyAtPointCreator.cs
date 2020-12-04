using Logic.Strategies.Interfaces;
using Logic.Utils;

namespace Logic.Strategies.Creators
{
    public class ShootStrategyAtPointCreator : StrategyCreator
    {
        public Point ShootPoint { get; set; }

        public override IShootStrategy FactoryMethod()
        {
            return new ShootStrategyAtPoint() { ShootPoint = ShootPoint };
        }


        public ShootStrategyAtPointCreator(Point shootPoint)
        {
            ShootPoint = shootPoint;
        }
    }
}