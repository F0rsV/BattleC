using System;
using System.Runtime.CompilerServices;
using BattleSea.Model;
using BattleSea.Observer.Interfaces;

namespace BattleSea.Observer
{
    public class PlayerListener: IPlayerListener
    {
        public Field MyFieldView { get; set; }
        public Field EnemyFieldView { get; set; }
        public string MoveResultInfo { get; set; }

        public PlayerListener(Field myFieldView)
        {
            MyFieldView = myFieldView;

            var height = myFieldView.CellsMatrix.GetLength(0);
            var width = myFieldView.CellsMatrix.GetLength(1);
            EnemyFieldView = new Field(height, width);

            MoveResultInfo = "";
        }



        public void Update(bool isMyTurn, Cell changedCell)
        {
            if (isMyTurn)
            {
                EnemyFieldView.CellsMatrix[changedCell.Y, changedCell.X] = changedCell;

                MoveResultInfo = "\n You shot at point (" + changedCell.X + "; " + changedCell.Y +
                                 "). This cell now has state " + changedCell.CellState + "\n";
            }
            else
            {
                MyFieldView.CellsMatrix[changedCell.Y, changedCell.X] = changedCell;

                MoveResultInfo = "\n Enemy shot at point (" + changedCell.X + "; " + changedCell.Y +
                                 "). This cell now has state " + changedCell.CellState + "\n";
            }

        }

        public void Update(string message)
        {
            MoveResultInfo = "\n" + message + "\n";
        }

        public string GetMoveResultInfo() => MoveResultInfo;
        public Field GetMyField() => MyFieldView;
        public Field GetEnemyField() => EnemyFieldView;
    }
}