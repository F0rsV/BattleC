using Logic.Exceptions;
using Logic.GameField.Builder.Interfaces;
using Logic.Utils;
using Logic.Utils.Enums;

namespace Logic.GameField.Builder
{
    public class FieldBuilder : IFieldBuilder
    {
        private Field _field = new Field();
        private ShipsStorage _shipsStorage;

        public FieldBuilder()
        {
            Reset();
        }


        public void Reset()
        {
            _field = new Field();
            _shipsStorage = new ShipsStorage();
        }

        public void SetDimension(int height, int width)
        {
            _field = new Field(height, width);
        }

        public void SetShipsStorage(ShipsStorage shipsStorage)
        {
            _shipsStorage = shipsStorage;
        }

        public void AddShip(ShipPlacementInfo shipPlacementInfo)
        {
            CheckShipPlacement(shipPlacementInfo);
            SurroundShipWithClosedCells(shipPlacementInfo);

            //CHECKS COMPLETE, ADDING SHIP
            int x = shipPlacementInfo.Point.X;
            int y = shipPlacementInfo.Point.Y;
            int iterations = (int)shipPlacementInfo.ShipType + 1;

            // MAIN ITERATION
            for (int i = 0; i < iterations; i++)
            {
                _field.CellsMatrix[y, x].CellState = CellState.Ship;
                _field.CellsMatrix[y, x].CanPlaceShip = false;

                //increment
                if (shipPlacementInfo.ShipRotation == ShipRotation.Horizontal)
                    x++;
                else if (shipPlacementInfo.ShipRotation == ShipRotation.Vertical)
                    y++;
            }

        }

        public Field GetResult()
        {
            Field field = _field;
            return field;
        }

        private void CheckShipPlacement(ShipPlacementInfo shipPlacementInfo)
        {
            int x = shipPlacementInfo.Point.X;
            int y = shipPlacementInfo.Point.Y;
            int iterations = (int)shipPlacementInfo.ShipType + 1;

            bool tempCanPlaceShipMainPoint;
            try
            {
                tempCanPlaceShipMainPoint = _field.CellsMatrix[shipPlacementInfo.Point.Y, shipPlacementInfo.Point.X].CanPlaceShip;
            }
            catch
            {
                throw new ShipPlacementOutOfBoundsException("Ship Out of bounds");
            }

            if (tempCanPlaceShipMainPoint == false)
            {
                throw new ShipPlacementCollisionException("Can't place ship here");
            }


            for (int i = 0; i < iterations; i++)
            {
                try
                {
                    var temp = _field.CellsMatrix[y, x].CellState;
                }
                catch
                {
                    throw new ShipPlacementOutOfBoundsException("Ship Out of bounds");
                }

                if (_field.CellsMatrix[y, x].CanPlaceShip == false)
                {
                    throw new ShipPlacementCollisionException("Can't place ship here");
                }


                //increment
                if (shipPlacementInfo.ShipRotation == ShipRotation.Horizontal)
                    x++;
                else if (shipPlacementInfo.ShipRotation == ShipRotation.Vertical)
                    y++;
            }

        }

        private void SurroundShipWithClosedCells(ShipPlacementInfo shipPlacementInfo)
        {
            int x = shipPlacementInfo.Point.X;
            int y = shipPlacementInfo.Point.Y;
            int iterations = (int)shipPlacementInfo.ShipType + 1;

            //before main iteration
            if (shipPlacementInfo.ShipRotation == ShipRotation.Vertical)
            {
                for (int xIndexBefore = x - 1; xIndexBefore < x + 2; xIndexBefore++)
                {
                    try
                    {
                        _field.CellsMatrix[y - 1, xIndexBefore].CanPlaceShip = false;
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            else
            {
                for (int yIndexBefore = y - 1; yIndexBefore < y + 2; yIndexBefore++)
                {
                    try
                    {
                        _field.CellsMatrix[yIndexBefore, x - 1].CanPlaceShip = false;
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }


            //main iteration
            for (int i = 0; i < iterations; i++)
            {
                if (shipPlacementInfo.ShipRotation == ShipRotation.Vertical)
                {
                    try
                    {
                        _field.CellsMatrix[y, x - 1].CanPlaceShip = false;
                    }
                    catch
                    {
                        // ignored
                    }
                    try
                    {
                        _field.CellsMatrix[y, x + 1].CanPlaceShip = false;
                    }
                    catch
                    {
                        // ignored
                    }
                }
                else
                {
                    try
                    {
                        _field.CellsMatrix[y - 1, x].CanPlaceShip = false;
                    }
                    catch
                    {
                        // ignored
                    }
                    try
                    {
                        _field.CellsMatrix[y + 1, x].CanPlaceShip = false;
                    }
                    catch
                    {
                        // ignored
                    }
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
                    try
                    {
                        _field.CellsMatrix[y, xIndexAfter].CanPlaceShip = false;
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }
            else
            {
                for (int yIndexAfter = y - 1; yIndexAfter < y + 2; yIndexAfter++)
                {
                    try
                    {
                        _field.CellsMatrix[yIndexAfter, x].CanPlaceShip = false;
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

        }
    }
}