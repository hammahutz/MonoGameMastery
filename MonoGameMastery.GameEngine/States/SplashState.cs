using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.Input;
using MonoGameMastery.GameEngine.Input.Base;
using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States.Base;

namespace MonoGameMastery.GameEngine.States;

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

    public override void Draw(SpriteBatch spriteBatch)
    {
        base.Draw(spriteBatch);
        spriteBatch.DrawString(MainGame.Font, "Splash State", new Vector2(10, 20), Color.AliceBlue);
    }

    protected override void SetInputManager() => InputManager = new InputManager(new GameplayInputMapper());


}