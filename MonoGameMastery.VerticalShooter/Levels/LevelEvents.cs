using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.VerticalShooter.Levels;

public class LevelEvents : BaseGameStateEvent
{
    public class GenerateEnemies : LevelEvents
    {
        public int NbEnemies { get; private set; }
        public GenerateEnemies(int nbEnemies) => NbEnemies = nbEnemies;
    }

    public class GenderateTurret : LevelEvents
    {
        public int XPosition { get; private set; }
        public GenderateTurret(int xPosition) => XPosition = xPosition;
    }

    public class StartLevel : LevelEvents { }
    public class EndLevel : LevelEvents { }
    public class NoRow : LevelEvents { }

}