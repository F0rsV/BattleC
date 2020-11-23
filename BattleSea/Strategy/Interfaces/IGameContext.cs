using BattleSea.Model;
using BattleSea.Observer;

namespace BattleSea.Strategy.Interfaces
{
    public interface IGameContext
    {
        void MakeMove(IShootStrategy shootStrategy);
        bool IsPlayerOneTurn();
        void SetFieldsUsingListeners();
        void SetEventManager(EventManager eventManager);
        bool IsGameEndedCheck();
    }
}