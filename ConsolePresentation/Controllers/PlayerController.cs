using System;
using ConsolePresentation.Utils.Enums;
using ConsolePresentation.View.Interfaces;
using Logic.GameField;
using Logic.GameField.Builder;
using Logic.GameField.Builder.Interfaces;
using Logic.Strategies.Creators;
using Logic.Strategies.Interfaces;
using Logic.Utils;

namespace ConsolePresentation.Controllers
{
    public class PlayerController : GeneralController
    {
        public override IShootStrategy GetShootStrategy()
        {
            StrategyCreator strategyCreator = null;

            var playerInputStrategy = View.GetStrategyInput(); //TODO
            if (playerInputStrategy == PlayerInputStrategy.Random)
            {
                strategyCreator = new ShootStrategyRandomCreator();
            }
            else if (playerInputStrategy == PlayerInputStrategy.AtPoint)
            {
                strategyCreator = new ShootStrategyAtPointCreator(View.GetPointInput());
            }

            return strategyCreator?.FactoryMethod();
        }

        public override Field GetPlayerField(GameRules gameRules)
        {
            IFieldBuilder fieldBuilder = new FieldBuilder();

            fieldBuilder.SetDimension(gameRules.FieldHeight, gameRules.FieldWidth);
            fieldBuilder.SetShipsStorage(gameRules.ShipsStorage);

            View.ShowConcreteCellsMatrix(fieldBuilder.GetResult().CellsMatrix);

            foreach (var shipAvailable in gameRules.ShipsStorage.ShipsAvailable)
            {
                int i = 0;
                while (i < shipAvailable.Value)
                {
                    View.ShowInfo("Place " + shipAvailable.Key + " ship:");

                    var shipPlacementInfo = new ShipPlacementInfo
                    {
                        ShipType = shipAvailable.Key,
                        ShipRotation = View.GetRotationInput(),
                        Point = View.GetPointInput()
                    };

                    try
                    {
                        fieldBuilder.AddShip(shipPlacementInfo);
                    }
                    catch (Exception e)
                    {
                        View.ShowInfo(e.Message);
                        continue;
                    }

                    View.Clear();

                    View.ShowConcreteCellsMatrix(fieldBuilder.GetResult().CellsMatrix);

                    i++;
                }

            }

            return fieldBuilder.GetResult();
        }


        public PlayerController(IView view = null) : base(view) { }
    }
}