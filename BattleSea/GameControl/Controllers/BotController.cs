using System;
using BattleSea.Builder;
using BattleSea.Builder.Interfaces;
using BattleSea.FactoryMethod;
using BattleSea.Model;
using BattleSea.Strategy.Interfaces;
using BattleSea.View;

namespace BattleSea.GameControl.Controllers
{
    public class BotController : GeneralController
    {
        public override IShootStrategy GetShootStrategy()
        {
            StrategyCreator strategyCreator = new ShootStrategyRandomCreator();
            return strategyCreator.FactoryMethod();
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


        public BotController(IView view = null) : base(view) { }
    }
}