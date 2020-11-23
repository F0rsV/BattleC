using BattleSea.Model;

namespace BattleSea.Strategy.Interfaces
{
    public interface IShootStrategy
    {
        Cell Shoot(Field field);
    }
}