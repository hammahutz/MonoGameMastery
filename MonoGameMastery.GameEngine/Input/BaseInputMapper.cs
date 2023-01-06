using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;

namespace MonoGameMastery.GameEngine.Input
{
    public abstract class BaseInputMapper
    {
        public virtual IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state) => new List<BaseInputCommand>();

        public virtual IEnumerable<BaseInputCommand> GetMouseState(MouseState state) => new List<BaseInputCommand>();

        public virtual IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state) => new List<BaseInputCommand>();
    }
}