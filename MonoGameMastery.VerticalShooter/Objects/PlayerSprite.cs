using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace MonoGameMastery.GameEngine.Objects
{
    public class PlayerSprite : BaseGameObject
    {
        private const float PLAYER_SPEED = 10.0f;
        public PlayerSprite(Texture2D texture2D) : base(texture2D) { }

        public void MoveLeft() => Position = new Vector2(Position.X - PLAYER_SPEED, Position.Y);
        public void MoveRight() => Position = new Vector2(Position.X + PLAYER_SPEED, Position.Y);

    }
}