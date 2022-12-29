using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.VerticalShooter.States;

public class GamePlayEvents : BaseGameStateEvent
{
    public class PlayerShoot : GamePlayEvents{}
    public class PlayerShootMissile : GamePlayEvents{}
}