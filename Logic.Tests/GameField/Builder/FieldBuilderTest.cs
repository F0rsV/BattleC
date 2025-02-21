﻿using System;
using System.Collections.Generic;
using Logic.Exceptions;
using Logic.GameField.Builder;
using Logic.Utils;
using Logic.Utils.Enums;
using NUnit.Framework;

namespace Logic.Tests.GameField.Builder
{
    [TestFixture]
    public class FieldBuilderTest
    {
        private FieldBuilder _builder;

        [SetUp]
        public void Init()
        {
            _builder = new FieldBuilder();
        }


        [Test]
        public void Reset_FieldIsNewInstance()
        {
            var oldField = _builder.GetResult();
            _builder.Reset();
            var newField = _builder.GetResult();

            Assert.AreNotSame(oldField, newField);
        }


        [TestCase(-1, -5)]
        [TestCase(0, 0)]
        [TestCase(0, 5)]
        [TestCase(-7, 5)]
        [TestCase(4, 0)]
        public void SetSetDimension_NegativeOrZero_ThrowsFieldBuilderNegativeOrZeroException(int height, int width)
        {
            void Action() => _builder.SetDimension(height, width);
            
            Assert.Throws<FieldBuilderNegativeOrZeroException>(Action);
        }


        [TestCase(2, 2, ShipRotation.Horizontal, ShipType.TwoDecks)]
        [TestCase(2, 1, ShipRotation.Vertical, ShipType.ThreeDecks)]
        public void AddShip_RightShipPlacementInfo(int pointX, int pointY, ShipRotation shipRotation, ShipType shipType)
        {
            var shipStorage = new ShipsStorage();
            shipStorage.AddShip(shipType, 1);

            _builder.SetDimension(5, 5);
            _builder.SetShipsStorage(shipStorage);

            var shipPlacementInfo = new ShipPlacementInfo
            {
                Point = new Point(pointX, pointY),
                ShipRotation = shipRotation,
                ShipType = shipType
            };


            _builder.AddShip(shipPlacementInfo);

            var listOfCells = GetListOfCellsWhereShipShouldBe(shipPlacementInfo);
            foreach (var cell in listOfCells)
            {
                Assert.True(cell.CellState == CellState.Ship);
            }

        }



        [TestCase(2, 2, ShipRotation.Horizontal, ShipType.TwoDecks)]
        [TestCase(2, 1, ShipRotation.Vertical, ShipType.ThreeDecks)]
        public void AddShip_SurroundShipWithClosedCells(int pointX, int pointY, ShipRotation shipRotation, ShipType shipType)
        {
            var shipStorage = new ShipsStorage();
            shipStorage.AddShip(shipType, 1);

            _builder.SetDimension(5, 5);
            _builder.SetShipsStorage(shipStorage);

            var shipPlacementInfo = new ShipPlacementInfo
            {
                Point = new Point(pointX, pointY),
                ShipRotation = shipRotation,
                ShipType = shipType
            };


            _builder.AddShip(shipPlacementInfo);

            var listOfCells = GetListOfCellsAroundShip(shipPlacementInfo);

            foreach (var cell in listOfCells)
            {
                Assert.False(cell.CanPlaceShip);
            }

        }


        [TestCase(4, 0, ShipRotation.Horizontal)]
        [TestCase(0, 3, ShipRotation.Vertical)]
        public void AddShip_ThrowsShipPlacementOutOfBoundsException(int pointX, int pointY, ShipRotation shipRotation)
        {
            var shipStorage = new ShipsStorage();
            shipStorage.AddShip(ShipType.FourDecks, 1);

            
            _builder.SetDimension(5, 5);
            _builder.SetShipsStorage(shipStorage);

            var shipPlacementInfo = new ShipPlacementInfo
            {
                Point = new Point(pointX, pointY), 
                ShipRotation = shipRotation, 
                ShipType = ShipType.FourDecks
            };


            void Action() => _builder.AddShip(shipPlacementInfo);


            Assert.Throws<ShipPlacementOutOfBoundsException>(Action);
        }


        [TestCase(0, 3, ShipRotation.Horizontal)]
        [TestCase(4, 0, ShipRotation.Vertical)]
        public void AddShip_ThrowsShipPlacementCollisionException(int pointX, int pointY, ShipRotation shipRotation)
        {
            var shipStorage = new ShipsStorage();
            shipStorage.AddShip(ShipType.FourDecks, 1);
            shipStorage.AddShip(ShipType.TwoDecks, 1);

            _builder.SetDimension(5, 5);
            _builder.SetShipsStorage(shipStorage);

            var shipPlacementInfoFirst = new ShipPlacementInfo
            {
                Point = new Point(3, 3),
                ShipRotation = ShipRotation.Horizontal,
                ShipType = ShipType.TwoDecks
            };
            _builder.AddShip(shipPlacementInfoFirst);

            var shipPlacementInfoSecond = new ShipPlacementInfo
            {
                Point = new Point(pointX, pointY),
                ShipRotation = shipRotation,
                ShipType = ShipType.FourDecks
            };


            void Action() => _builder.AddShip(shipPlacementInfoSecond);


            Assert.Throws<ShipPlacementCollisionException>(Action);
        }


        private List<Cell> GetListOfCellsWhereShipShouldBe(ShipPlacementInfo shipPlacementInfo)
        {
            var listOfCells = new List<Cell>();
            var filed = _builder.GetResult();

            int x = shipPlacementInfo.Point.X;
            int y = shipPlacementInfo.Point.Y;
            int iterations = (int)shipPlacementInfo.ShipType + 1;

            //main iteration
            for (int i = 0; i < iterations; i++)
            {
                if (shipPlacementInfo.ShipRotation == ShipRotation.Vertical)
                {
                    listOfCells.Add(filed.CellsMatrix[y, x]);
                }
                else
                {
                    listOfCells.Add(filed.CellsMatrix[y, x]);
                }

                //increment
                if (shipPlacementInfo.ShipRotation == ShipRotation.Horizontal)
                    x++;
                else if (shipPlacementInfo.ShipRotation == ShipRotation.Vertical)
                    y++;
            }


            return listOfCells;
        }

        private List<Cell> GetListOfCellsAroundShip(ShipPlacementInfo shipPlacementInfo)
        {
            var listOfCells = new List<Cell>();
            var filed = _builder.GetResult();

            int x = shipPlacementInfo.Point.X;
            int y = shipPlacementInfo.Point.Y;
            int iterations = (int)shipPlacementInfo.ShipType + 1;

            //before main iteration
            if (shipPlacementInfo.ShipRotation == ShipRotation.Vertical)
            {
                for (int xIndexBefore = x - 1; xIndexBefore < x + 2; xIndexBefore++)
                {
                    listOfCells.Add(filed.CellsMatrix[y - 1, xIndexBefore]);
                }
            }
            else
            {
                for (int yIndexBefore = y - 1; yIndexBefore < y + 2; yIndexBefore++)
                {
                    listOfCells.Add(filed.CellsMatrix[yIndexBefore, x - 1]);
                }
            }


            //main iteration
            for (int i = 0; i < iterations; i++)
            {
                if (shipPlacementInfo.ShipRotation == ShipRotation.Vertical)
                {
                    listOfCells.Add(filed.CellsMatrix[y, x - 1]);
                    listOfCells.Add(filed.CellsMatrix[y, x + 1]);
                }
                else
                {
                    listOfCells.Add(filed.CellsMatrix[y - 1, x]);
                    listOfCells.Add(filed.CellsMatrix[y + 1, x]);
                }

                //increment
                if (shipPlacementInfo.ShipRotation == ShipRotation.Horizontal)
                    x++;
                else if (shipPlacementInfo.ShipRotation == ShipRotation.Vertical)
                    y++;
            }

            //after main iteration
            if (shipPlacementInfo.ShipRotation == ShipRotation.Vertical)
            {
                for (int xIndexAfter = x - 1; xIndexAfter < x + 2; xIndexAfter++)
                {
                    listOfCells.Add(filed.CellsMatrix[y, xIndexAfter]);
                }
            }
            else
            {
                for (int yIndexAfter = y - 1; yIndexAfter < y + 2; yIndexAfter++)
                {
                    listOfCells.Add(filed.CellsMatrix[yIndexAfter, x]);
                }
            }

            return listOfCells;
        }


    }
}