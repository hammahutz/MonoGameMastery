using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine;
using MonoGameMastery.GameEngine.Input;
using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.VerticalShooter.States;

public class SplashState : BaseGameState
{
    public override void LoadContent() => AddGameObject(new SplashImage(LoadTexture("gfx/splash")));
    public override void HandleInput(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Enter))
        {
            SwitchState(new GameplayState());
        }

        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
        {
            NotifyEvent(Events.QUIT_GAME);
        }
    }

    protected override void SetInputManager() => InputManager = new InputManager(new GameplayInputMapper());


}