using ConsolePresentation.View.Interfaces;
using Logic.EventsManager.Interfaces;
using Logic.GameField;
using Logic.Strategies.Interfaces;
using Logic.Utils;

namespace ConsolePresentation.Controllers
{
    public abstract class GeneralController
    {
        public IPlayerListener PlayerListener { get; set; }
        public IView View { get; set; }

        public abstract IShootStrategy GetShootStrategy();
        public abstract Field GetPlayerField(GameRules gameRules);

        protected GeneralController(IView view = null)
        {
            View = view;
        }
    }
}