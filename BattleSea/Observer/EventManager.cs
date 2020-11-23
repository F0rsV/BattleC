using System;
using BattleSea.Model;
using BattleSea.Observer.Interfaces;

namespace BattleSea.Observer
{
    public class EventManager
    {
        public IPlayerListener PlayerOneListener { get; set; }
        public IPlayerListener PlayerTwoListener { get; set; }
        public bool IsPlayerOneTurn { get; set; } = true;

        public EventManager(IPlayerListener playerOneListener, IPlayerListener playerTwoListener)
        {
            PlayerOneListener = playerOneListener;
            PlayerTwoListener = playerTwoListener;
        }
        
        public void Notify(Cell changedCell)
        {
            PlayerOneListener.Update(IsPlayerOneTurn, changedCell); 
            PlayerTwoListener.Update(!IsPlayerOneTurn, changedCell);

            //don`t change turn if ship got hit
            if (changedCell.CellState != CellState.DamagedShip)
            {
                IsPlayerOneTurn = !IsPlayerOneTurn;
            }
        }

        public void Notify(Exception exception)
        {
            if (IsPlayerOneTurn)
            {
                PlayerOneListener.Update("ERROR: " + exception.Message + " Try again.");
                PlayerTwoListener.Update("");
            }
            else
            {
                PlayerTwoListener.Update("ERROR: " + exception.Message + " Try again.");
                PlayerOneListener.Update("");
            }
        }

        public void Notify()
        {
            if (IsPlayerOneTurn)
            {
                PlayerOneListener.Update("You have won!");
                PlayerTwoListener.Update("You are loser, чел.");
            }
            else
            {
                PlayerTwoListener.Update("You have won!");
                PlayerOneListener.Update("You are loser, чел.");
            }
        }


    }
}