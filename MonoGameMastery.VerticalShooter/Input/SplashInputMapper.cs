using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

namespace MonoGameMastery.GameEngine.Input
{
    public class SplashInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<SplashInputCommand>();

            if (state.IsKeyDown(Keys.Enter))
                commands.Add(new SplashInputCommand.GameSelect());

            return commands;
        }
    }
}