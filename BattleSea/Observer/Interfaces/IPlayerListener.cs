using System;
using BattleSea.Model;

namespace BattleSea.Observer.Interfaces
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