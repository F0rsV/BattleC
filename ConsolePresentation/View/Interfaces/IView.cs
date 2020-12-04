using ConsolePresentation.Utils.Enums;
using Logic.EventsManager.Interfaces;
using Logic.Utils;
using Logic.Utils.Enums;

namespace ConsolePresentation.View.Interfaces
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