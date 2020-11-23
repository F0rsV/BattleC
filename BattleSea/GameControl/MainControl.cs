using System;
using BattleSea.Builder;
using BattleSea.Builder.Enums;
using BattleSea.Builder.Interfaces;
using BattleSea.GameControl.Controllers;
using BattleSea.Observer;
using BattleSea.Observer.Interfaces;
using BattleSea.Strategy;
using BattleSea.Strategy.Interfaces;

namespace BattleSea.GameControl
{
    public class MainControl
    {
        public IGameContext GameContext { get; set; } = new GameContext();
        public GeneralController PlayerOneController { get; set; }
        public GeneralController PlayerTwoController { get; set; }
        private GameRules _gameRules;


        public void SetMainControlFields(GeneralController playerOneController, GeneralController playerTwoController)
        {
            SetGameRules();

            SetPlayerOneControllerWithoutListener(playerOneController);
            SetPlayerTwoControllerWithoutListener(playerTwoController);

            SetListenersForControllers();
            SetGameContextEventManagerAndFields();
        }


        private void SetGameRules() //TODO
        {
            _gameRules = new GameRules();

            ShipsStorage shipsStorage = new ShipsStorage();

            /*
            shipsStorage.AddShip(ShipType.FourDecks, 1);
            shipsStorage.AddShip(ShipType.ThreeDecks, 2);
            shipsStorage.AddShip(ShipType.TwoDecks, 3);
            shipsStorage.AddShip(ShipType.OneDeck, 4);

            _gameRules.FieldHeight = 10;
            _gameRules.FieldWidth = 10;
            */
            shipsStorage.AddShip(ShipType.TwoDecks, 1);
            shipsStorage.AddShip(ShipType.OneDeck, 2);

            _gameRules.FieldHeight = 5;
            _gameRules.FieldWidth = 5;

            _gameRules.ShipsStorage = shipsStorage;

        }

        private void SetPlayerOneControllerWithoutListener(GeneralController controller)
        {
            PlayerOneController = controller;
        }

        private void SetPlayerTwoControllerWithoutListener(GeneralController controller)
        {
            PlayerTwoController = controller;
        }

        private void SetListenersForControllers()
        {
            var fieldPlayerOne = PlayerOneController.GetPlayerField(_gameRules);
            var fieldPlayerTwo = PlayerTwoController.GetPlayerField(_gameRules);
            
            PlayerOneController.PlayerListener = new PlayerListener(fieldPlayerOne);
            PlayerTwoController.PlayerListener = new PlayerListener(fieldPlayerTwo);
        }

        private void SetGameContextEventManagerAndFields()
        {
            EventManager eventManager = new EventManager(PlayerOneController.PlayerListener, PlayerTwoController.PlayerListener);
            GameContext.SetEventManager(eventManager);
            
            GameContext.SetFieldsUsingListeners();
        }


        public void Play()
        {
            while (!GameContext.IsGameEndedCheck())
            {
                PlayerOneController.View?.ShowFields(PlayerOneController.PlayerListener);
                PlayerOneController.View?.ShowInfo(PlayerOneController.PlayerListener.GetMoveResultInfo());

                GameContext.MakeMove(GameContext.IsPlayerOneTurn()
                    ? PlayerOneController.GetShootStrategy()
                    : PlayerTwoController.GetShootStrategy());
            }

            PlayerOneController.View?.ShowFields(PlayerOneController.PlayerListener);
            PlayerOneController.View?.ShowInfo(PlayerOneController.PlayerListener.GetMoveResultInfo());
        }
    }
}