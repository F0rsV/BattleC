using System;
using Logic.Context;
using Logic.EventsManager;
using Logic.GameField;
using Logic.Strategies;
using Logic.Utils;
using Logic.Utils.Enums;
using NUnit.Framework;

namespace Logic.Tests.Context
{
    [TestFixture]
    public class GameContextTest
    {
        [TestCase(true, 2, 2)]
        [TestCase(true, 3, 3)]
        [TestCase(false, 3, 3)]
        [TestCase(false, 3, 3)]
        public void MakeMove_CellIsChanged(bool isPlayerOneTurn, int shootPointX, int shootPointY)
        {

            var gameContext = new GameContext
            {
                FieldPlayerOne = new Field(5, 5), 
                FieldPlayerTwo = new Field(5, 5)
            };

            gameContext.FieldPlayerOne.CellsMatrix[0, 0].CellState = CellState.Ship;
            gameContext.FieldPlayerTwo.CellsMatrix[0, 0].CellState = CellState.Ship;

            gameContext.EventManager = new EventManager(
                new PlayerListener(gameContext.FieldPlayerOne),
                new PlayerListener(gameContext.FieldPlayerTwo)
                ) {IsPlayerOneTurn = isPlayerOneTurn };

            var shootStrategy = new ShootStrategyAtPoint
            {
                ShootPoint = new Point(shootPointX, shootPointX)
            };

            
            gameContext.MakeMove(shootStrategy);

            CellState changedCellState;
            if (isPlayerOneTurn)
            {
                changedCellState = gameContext.FieldPlayerTwo
                    .CellsMatrix[shootPointY, shootPointX].CellState;
            }
            else
            {
                changedCellState = gameContext.FieldPlayerOne
                    .CellsMatrix[shootPointY, shootPointX].CellState;
            }

            ;
            Assert.True(changedCellState == CellState.Empty);
        }


        [TestCase(true)]
        [TestCase(false)]
        public void MakeMove_PlayerTurnIsChanged(bool isPlayerOneTurn)
        {
            
            var gameContext = new GameContext
            {
                FieldPlayerOne = new Field(5, 5),
                FieldPlayerTwo = new Field(5, 5)
            };

            gameContext.FieldPlayerOne.CellsMatrix[0, 0].CellState = CellState.Ship;
            gameContext.FieldPlayerTwo.CellsMatrix[0, 0].CellState = CellState.Ship;

            gameContext.EventManager = new EventManager(
                    new PlayerListener(gameContext.FieldPlayerOne),
                    new PlayerListener(gameContext.FieldPlayerTwo)
                )
                { IsPlayerOneTurn = isPlayerOneTurn };

            var shootStrategy = new ShootStrategyAtPoint
            {
                ShootPoint = new Point(3, 3)
            };


            gameContext.MakeMove(shootStrategy);

            bool isChangedMove;
            if (isPlayerOneTurn)
            {
                isChangedMove = !gameContext.IsPlayerOneTurn();
            }
            else
            {
                isChangedMove = gameContext.IsPlayerOneTurn();
            }


            Assert.True(isChangedMove);
        }

        [Test]
        public void IsGameEndedCheck_Correct()
        {
            var gameContext = new GameContext
            {
                FieldPlayerOne = new Field(5, 5),
                FieldPlayerTwo = new Field(5, 5)
            };
            
            gameContext.EventManager = new EventManager(
                    new PlayerListener(gameContext.FieldPlayerOne),
                    new PlayerListener(gameContext.FieldPlayerTwo)
                )
                { IsPlayerOneTurn = true };


            var isGameEnded = gameContext.IsGameEndedCheck();


            Assert.True(isGameEnded);
        }

    }
}