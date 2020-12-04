using Logic.GameField;
using Logic.Utils;

namespace Logic.Strategies.Interfaces
{
    public interface IShootStrategy
    {
        Cell Shoot(Field field);
    }
}