using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MonoGameMastery.GameEngine.Input;
using Microsoft.Xna.Framework.Input;

namespace MonoGameMastery.GameEngine.Input
{
    public class GameplayInputMapper : BaseInputMapper
    {
        public override IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state)
        {
            var commands = new List<GamePlayInputCommand>();



            if(state.IsKeyDown(Keys.Escape))
                commands.Add(new GamePlayInputCommand.GameExit());
            if(state.IsKeyDown(Keys.Left))
                commands.Add(new GamePlayInputCommand.PlayerMoveLeft());
            if(state.IsKeyDown(Keys.Right))
                commands.Add(new GamePlayInputCommand.PlayerMoveRight());
            if(state.IsKeyDown(Keys.Space))
                commands.Add(new GamePlayInputCommand.PlayerShoots());


            return commands;
        }
    }
}