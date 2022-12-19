using System.ComponentModel.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace MonoGameMastery.GameEngine.Input.Base
{
    public abstract class BaseInputMapper
    {
        public virtual IEnumerable<BaseInputCommand> GetKeyboardState(KeyboardState state) => new List<BaseInputCommand>();
        public virtual IEnumerable<BaseInputCommand> GetMouseState(MouseState state) => new List<BaseInputCommand>();
        public virtual IEnumerable<BaseInputCommand> GetGamePadState(GamePadState state) => new List<BaseInputCommand>();
    }
}