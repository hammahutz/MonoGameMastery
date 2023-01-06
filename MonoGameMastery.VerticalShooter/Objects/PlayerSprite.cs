using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameMastery.GameEngine.Objects
{
    public class PlayerSprite : BaseGameObject
    {
        private const float PLAYER_SPEED = 10.0f;
        private List<Rectangle> BB;

        public PlayerSprite(Texture2D texture2D) : base(texture2D)
        {
            BB = new List<Rectangle>()
            {
                new Rectangle(29,2,57,147),
                new Rectangle(2,77,111,37),
            };

            BB.ForEach(bb => AddBoundingBox(new BoundingBox(bb)));
        }

        public void MoveLeft() => Position = new Vector2(Position.X - PLAYER_SPEED, Position.Y);

        public void MoveRight() => Position = new Vector2(Position.X + PLAYER_SPEED, Position.Y);
    }
}