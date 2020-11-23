using System;
using BattleSea.Builder;
using BattleSea.Builder.Enums;
using BattleSea.Builder.Interfaces;
using BattleSea.FactoryMethod;
using BattleSea.GameControl;
using BattleSea.GameControl.Controllers;
using BattleSea.Model;
using BattleSea.Observer;
using BattleSea.Observer.Interfaces;
using BattleSea.Strategy;
using BattleSea.Strategy.Interfaces;
using BattleSea.View;

namespace BattleSea
{
    class Program
    {
        static void Main(string[] args)
        {
            var mainControl = new MainControl();
            mainControl.SetMainControlFields(
                new PlayerController(new ConsoleView()),
                new BotController(new ConsoleView()));

            mainControl.Play();

        }


    }
}
