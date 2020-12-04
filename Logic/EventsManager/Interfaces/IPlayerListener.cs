using Logic.GameField;
using Logic.Utils;

namespace Logic.EventsManager.Interfaces
{
    public interface IPlayerListener
    {
        void Update(bool isMyTurn, Cell changedCell);
        void Update(string message);

        string GetMoveResultInfo();
        Field GetMyField();
        Field GetEnemyField();
    }
}