using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.Input.Base;

namespace MonoGameMastery.GameEngine.Input
{
    public class SplashInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<SplashInputCommand>();

            if(state.IsKeyDown(Keys.Enter))
                commands.Add(new SplashInputCommand.GameSelect());


            return commands;
        }
    }
}