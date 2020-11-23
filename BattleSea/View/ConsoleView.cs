using System;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using BattleSea.Builder;
using BattleSea.Builder.Enums;
using BattleSea.Builder.Interfaces;
using BattleSea.FactoryMethod;
using BattleSea.Model;
using BattleSea.Observer;
using BattleSea.Observer.Interfaces;
using BattleSea.Strategy.Interfaces;

namespace BattleSea.View
{
    public class ConsoleView : IView
    {
        public void Clear()
        {
            Console.Clear();
        }

        public void ShowInfo(string stringInfo)
        {
            Console.WriteLine(stringInfo);
            Console.ReadKey(true);
        }

        public void ShowFields(IPlayerListener playerListener)
        {
            this.Clear();

            Console.WriteLine("Your Field: \n");
            ShowConcreteCellsMatrix(playerListener.GetMyField().CellsMatrix);
            Console.WriteLine();

            Console.WriteLine("Enemy Field: \n");
            ShowConcreteCellsMatrix(playerListener.GetEnemyField().CellsMatrix);
            Console.WriteLine("__________________________");

        }

        public void ShowConcreteCellsMatrix(Cell[,] cellsMatrix)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.WriteLine();

            for (int h = 0; h < cellsMatrix.GetLength(1); h++)
            {
                for (int w = 0; w < cellsMatrix.GetLength(0); w++)
                {
                    var cellRepresentation = cellsMatrix[h, w].CellState switch
                    {
                        CellState.NotChecked => "○",
                        CellState.Empty => "●",
                        CellState.Ship => "◘",
                        CellState.DamagedShip => "◙",
                        _ => ""
                    };

                    Console.Write(cellRepresentation + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }


        public string GetStrategyInput()
        {
            string userInput = "";

            bool isInputCorrect = false;
            while (!isInputCorrect)
            {
                Console.WriteLine("Chose strategy");
                Console.WriteLine("1. Random");
                Console.WriteLine("2. At Point");

                userInput = Console.ReadLine();
                if (userInput == "1" || userInput == "2")
                {
                    isInputCorrect = true;
                }
                else
                {
                    Console.WriteLine("Input Error. Try Again.");
                }

            }

            return userInput;
        }

        public Point GetPointInput()
        {
            int x = 0;
            int y = 0;

            bool isInputCorrect = false;
            while (!isInputCorrect)
            {
                Console.WriteLine("Choose point coordinates:");
                Console.Write("X:");
                var userInputX = Console.ReadLine();
                Console.Write("Y:");
                var userInputY = Console.ReadLine();

                try
                {
                    x = Int32.Parse(userInputX);
                    y = Int32.Parse(userInputY);
                }
                catch
                {
                    Console.WriteLine("Input Error. Try Again.");
                    continue;
                }

                isInputCorrect = true;
            }

            return new Point(x, y);
        }

        public ShipRotation GetRotationInput()
        {
            string userInput = "";
            ShipRotation shipRotation = ShipRotation.Horizontal;

            bool isInputCorrect = false;
            while (!isInputCorrect)
            {
                Console.WriteLine("Chose ship rotation");
                Console.WriteLine("V -> Vertical");
                Console.WriteLine("H -> Horizontal");

                userInput = Console.ReadLine();
                if (userInput == "V")
                {
                    shipRotation = ShipRotation.Vertical;
                    isInputCorrect = true;
                }
                else if (userInput == "H")
                {
                    shipRotation = ShipRotation.Horizontal;
                    isInputCorrect = true;
                }
                else
                {
                    Console.WriteLine("Input Error. Try Again.");
                }

            }

            return shipRotation;
        }
    }
}