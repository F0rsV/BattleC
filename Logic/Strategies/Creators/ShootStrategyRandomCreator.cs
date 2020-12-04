using Logic.Strategies.Interfaces;

namespace Logic.Strategies.Creators
{
    public class ShootStrategyRandomCreator : StrategyCreator
    {
        public override IShootStrategy FactoryMethod()
        {
            return new ShootStrategyRandom();
        }
    }
}