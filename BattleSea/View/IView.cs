﻿using BattleSea.Builder;
using BattleSea.Builder.Enums;
using BattleSea.GameControl.Enums;
using BattleSea.Model;
using BattleSea.Observer.Interfaces;

namespace BattleSea.View
{
    public interface IView
    {
        void ShowInfo(string stringInfo);
        void ShowFields(IPlayerListener playerListener);
        void ShowConcreteCellsMatrix(Cell[,] cellsMatrix);

        PlayerInputStrategy GetStrategyInput();
        Point GetPointInput();
        ShipRotation GetRotationInput();

        void Clear();
    }
}