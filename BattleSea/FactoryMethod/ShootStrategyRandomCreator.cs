using BattleSea.Strategy;
using BattleSea.Strategy.Interfaces;

namespace BattleSea.FactoryMethod
{
    public class ShootStrategyRandomCreator : StrategyCreator
    {
        public override IShootStrategy FactoryMethod()
        {
            return new ShootStrategyRandom();
        }

    }
}