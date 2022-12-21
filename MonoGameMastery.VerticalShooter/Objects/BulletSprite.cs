using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameMastery.GameEngine.Objects
{
    public class BulletSprite : BaseGameObject
    {
        private const float BULLET_SPEED = 10.0f;
        public BulletSprite(Texture2D texture2D)
        {
            _texture2D = texture2D;
        }

        public void MoveUp()
        {
            Position = new Vector2(Position.X, Position.Y - BULLET_SPEED);
        }



    }
}