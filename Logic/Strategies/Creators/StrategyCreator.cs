using Logic.Strategies.Interfaces;

namespace Logic.Strategies.Creators
{
    public abstract class StrategyCreator
    {
        public abstract IShootStrategy FactoryMethod();
    }
}