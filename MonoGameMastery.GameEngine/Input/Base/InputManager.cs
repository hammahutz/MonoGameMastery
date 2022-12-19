using System.Linq;
using System;

using Microsoft.Xna.Framework.Input;

namespace MonoGameMastery.GameEngine.Input.Base;

public class InputManager
{
    private BaseInputMapper _inputMapper;

    public InputManager(BaseInputMapper inputMapper) => _inputMapper = inputMapper;

    public void GetCommands(Action<BaseInputCommand> actOnState)
    {
        var keyboardState = Keyboard.GetState();
        var mouseState = Mouse.GetState();
        var gamePadState = GamePad.GetState(0);

        _inputMapper.GetKeyboardState(keyboardState).ToList().ForEach(state => actOnState(state));
        _inputMapper.GetMouseState(mouseState).ToList().ForEach(state => actOnState(state));
        _inputMapper.GetGamePadState(gamePadState).ToList().ForEach(state => actOnState(state));
    }
}