using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States.Base;

namespace MonoGameMastery.GameEngine.States
{
    public class GameplayState : BaseGameState
    {
        private const string PlayerFighter = "fighter";
        private const string Background = "Barren";
        public override void LoadContent()
        {
            AddGameObject(new SplashImage(LoadTexture(Background)));
            AddGameObject(new PlayerSprite(LoadTexture(PlayerFighter)));
        }

        public override void HandleInput()
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                NotifyEvent(Events.QUIT_GAME);
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(MainGame.Font, "Gameplay State", new Vector2(10, 20), Color.Orange);
        }

    }
}