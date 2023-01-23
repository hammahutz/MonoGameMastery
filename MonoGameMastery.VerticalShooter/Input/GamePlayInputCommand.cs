namespace MonoGameMastery.GameEngine.Input
{
    public class GamePlayInputCommand : BaseInputCommand
    {
        public class GameExit : GamePlayInputCommand { }

        public class PlayerMoveLeft : GamePlayInputCommand { }

        public class PlayerMoveRight : GamePlayInputCommand { }
        public class PlayerMoveUp : GamePlayInputCommand { }
        public class PlayerMoveDown : GamePlayInputCommand { }

        public class PlayerShoots : GamePlayInputCommand { }

        public class PlayerShootsMissile : GamePlayInputCommand { }

        public class PlayerStopsMoving : GamePlayInputCommand { }
    }
}