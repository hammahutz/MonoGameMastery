using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.VerticalShooter.States;

public class GamePlayEvents : BaseGameStateEvent
{
    public class PlayerShoot : GamePlayEvents
    { }

    public class PlayerShootMissile : GamePlayEvents
    { }

    public class ChopperHitBy : GamePlayEvents
    {
        public IGameObjectWithDamage HitBy { get; private set; }

        public ChopperHitBy(IGameObjectWithDamage gameObjectWithDamage) => HitBy = gameObjectWithDamage;
    }

    public class EnemyLostLife : GamePlayEvents
    {
        public int CurrentLife { get; private set; }

        public EnemyLostLife(int currentLife) => CurrentLife = currentLife;
    }
}