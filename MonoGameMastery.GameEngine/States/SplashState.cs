using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States.Base;

namespace MonoGameMastery.GameEngine.States;

public class SplashState : BaseGameState
{
    public override void LoadContent() => AddGameObject(new SplashImage(LoadTexture("splash")));
    public override void HandleInput()
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



}