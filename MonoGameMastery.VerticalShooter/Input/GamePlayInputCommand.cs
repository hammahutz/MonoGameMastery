using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MonoGameMastery.GameEngine.Input;

namespace MonoGameMastery.GameEngine.Input
{
    public class GamePlayInputCommand : BaseInputCommand
    {
        public class GameExit : GamePlayInputCommand { }
        public class PlayerMoveLeft : GamePlayInputCommand { }
        public class PlayerMoveRight : GamePlayInputCommand { }
        public class PlayerShoots : GamePlayInputCommand { }

    }
}