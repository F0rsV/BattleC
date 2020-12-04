using Logic.EventsManager;
using Logic.Strategies.Interfaces;

namespace Logic.Context.Interfaces
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