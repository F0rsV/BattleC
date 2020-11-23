using System;
using System.Diagnostics.Contracts;
using BattleSea.Model;
using BattleSea.Observer;
using BattleSea.Observer.Interfaces;
using BattleSea.Strategy.Interfaces;

namespace BattleSea.Strategy
{
    public class GameContext : IGameContext
    {
        public Field FieldPlayerOne { get; set; }
        public Field FieldPlayerTwo { get; set; }
        public EventManager EventManager { get; set; }

        public void MakeMove(IShootStrategy shootStrategy)
        {
            Cell changedCell;
            try
            {
                changedCell = shootStrategy.Shoot(EventManager.IsPlayerOneTurn ? FieldPlayerTwo : FieldPlayerOne);
            }
            catch (Exception exception)
            {
                EventManager.Notify(exception);
                return;
            }

            if (IsGameEndedCheck())
            {
                EventManager.Notify();
            }
            else
            {
                EventManager.Notify(changedCell);
            }
        }


        public bool IsGameEndedCheck()
        {
            if (EventManager.IsPlayerOneTurn)
            {
                for (int i = 0; i < FieldPlayerTwo.CellsMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < FieldPlayerTwo.CellsMatrix.GetLength(1); j++)
                    {
                        if (FieldPlayerTwo.CellsMatrix[i, j].CellState == CellState.Ship)
                        {
                            return false;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < FieldPlayerOne.CellsMatrix.GetLength(0); i++)
                {
                    for (int j = 0; j < FieldPlayerOne.CellsMatrix.GetLength(1); j++)
                    {
                        if (FieldPlayerOne.CellsMatrix[i, j].CellState == CellState.Ship)
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        public bool IsPlayerOneTurn() => EventManager.IsPlayerOneTurn;

        public void SetFieldsUsingListeners()
        {
            FieldPlayerOne = EventManager.PlayerOneListener.GetMyField();
            FieldPlayerTwo = EventManager.PlayerTwoListener.GetMyField();
        }

        public void SetEventManager(EventManager eventManager)
        {
            this.EventManager = eventManager;
        }

    }
}