using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;

using MonoGameMastery.GameEngine.Input;
using MonoGameMastery.GameEngine.States;
using MonoGameMastery.VerticalShooter.Input;
using MonoGameMastery.VerticalShooter.Particles;

namespace MonoGameMastery.VerticalShooter.States;

public class DevState : BaseGameState
{
    private const string GfxExhaust = "gfx/Cloud001";
    private ExhaustEmitter _exhaustEmitter;

    public override void LoadContent()
    {
        var exhaustPosition = new Vector2(300, 300);
        _exhaustEmitter = new ExhaustEmitter(LoadTexture(GfxExhaust), exhaustPosition);
        AddGameObject(_exhaustEmitter);
    }
    protected override void SetInputManager() => InputManager = new InputManager(new DevInputMapper());
    public override void HandleInput(GameTime gameTime)
    {
        InputManager.GetCommands(cmd =>
        {
            if (cmd is DevInputCommand.DevQuit)
            {
                NotifyEvent(new BaseGameStateEvent.GameQuit());
            }
        });
    }

    public override void UpdateGameState(GameTime gameTime) => _exhaustEmitter.Update(gameTime);


}