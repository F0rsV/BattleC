using BattleSea.Model;
using BattleSea.Strategy.Interfaces;

namespace BattleSea.FactoryMethod
{
    public abstract class StrategyCreator
    {
        public abstract IShootStrategy FactoryMethod();

    }
}