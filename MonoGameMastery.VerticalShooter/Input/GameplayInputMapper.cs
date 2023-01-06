using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

namespace MonoGameMastery.GameEngine.Input
{
    public class GameplayInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<GamePlayInputCommand>();

            if (state.IsKeyDown(Keys.Escape))
                commands.Add(new GamePlayInputCommand.GameExit());
            if (state.IsKeyDown(Keys.Left))
                commands.Add(new GamePlayInputCommand.PlayerMoveLeft());
            if (state.IsKeyDown(Keys.Right))
                commands.Add(new GamePlayInputCommand.PlayerMoveRight());
            if (state.IsKeyDown(Keys.Space))
                commands.Add(new GamePlayInputCommand.PlayerShoots());
            if (state.IsKeyDown(Keys.LeftControl))
                commands.Add(new GamePlayInputCommand.PlayerShootsMissile());

            return commands;
        }
    }
}