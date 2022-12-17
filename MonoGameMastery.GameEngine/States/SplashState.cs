using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.States.Base;
namespace MonoGameMastery.GameEngine.States;

public class SplashState : BaseGameState
{
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

    public override void Render(SpriteBatch spriteBatch)
    {
        base.Render(spriteBatch);
        spriteBatch.DrawString(MainGame.Font, "Splash State", new Vector2(10, 20), Color.AliceBlue);
    }
    

    public override void LoadContent(ContentManager contentManager)
    {
    }

    public override void UnloadContent(ContentManager contentManager)
    {
    }
}