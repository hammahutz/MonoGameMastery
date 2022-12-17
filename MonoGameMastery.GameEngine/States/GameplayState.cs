using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.States.Base;

namespace MonoGameMastery.GameEngine.States
{
    public class GameplayState : BaseGameState
    {
        public override void HandleInput()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                NotifyEvent(Events.QUIT_GAME);
            }
        }

        public override void LoadContent(ContentManager contentManager)
        {
        }

        public override void Render(SpriteBatch spriteBatch)
        {
            base.Render(spriteBatch);
            spriteBatch.DrawString(MainGame.Font, "Gameplay State", new Vector2(10, 20), Color.Orange);
        }

        public override void UnloadContent(ContentManager contentManager)
        {
        }
    }
}