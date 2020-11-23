using BattleSea.Model;
using BattleSea.Observer.Interfaces;
using BattleSea.Strategy.Interfaces;
using BattleSea.View;

namespace BattleSea.GameControl.Controllers
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