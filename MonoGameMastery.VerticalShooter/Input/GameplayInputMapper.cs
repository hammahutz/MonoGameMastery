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
            else if (state.IsKeyDown(Keys.Right))
                commands.Add(new GamePlayInputCommand.PlayerMoveRight());
            else
                commands.Add(new GamePlayInputCommand.PlayerStopsMoving());

            if (state.IsKeyDown(Keys.Up))
                commands.Add(new GamePlayInputCommand.PlayerMoveUp());
            if (state.IsKeyDown(Keys.Down))
                commands.Add(new GamePlayInputCommand.PlayerMoveDown());

            if (state.IsKeyDown(Keys.Space))
                commands.Add(new GamePlayInputCommand.PlayerShoots());
            if (state.IsKeyDown(Keys.LeftControl))
                commands.Add(new GamePlayInputCommand.PlayerShootsMissile());


            return commands;
        }
    }
}