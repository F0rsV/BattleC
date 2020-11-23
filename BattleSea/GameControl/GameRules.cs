using BattleSea.Builder;

namespace BattleSea.GameControl
{
    public class GameRules
    {
        public int FieldHeight { get; set; }
        public int FieldWidth { get; set; }

        public ShipsStorage ShipsStorage { get; set; }
    }
}