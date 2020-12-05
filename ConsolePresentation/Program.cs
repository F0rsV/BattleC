using System;
using ConsolePresentation.Controllers;
using ConsolePresentation.View;


namespace ConsolePresentation
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
