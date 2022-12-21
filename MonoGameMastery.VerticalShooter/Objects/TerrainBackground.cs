using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameMastery.GameEngine.Objects;

public class TerrainBackground : BaseGameObject
{
    private const float SCROLLING_SPEED = 30;

    public TerrainBackground(Texture2D texture2D)
    {
        _texture2D = texture2D;
        _position = Vector2.Zero;
    }


    //TODO Move logic to update function with Game time to do frame independent and move workload form draw to update
    public override void Draw(SpriteBatch spriteBatch)
    {
        var viewport = spriteBatch.GraphicsDevice.Viewport;
        var sourceRectangle = new Rectangle(0, 0, _texture2D.Width, _texture2D.Height);

        for (int nbVertical = -1; nbVertical < viewport.Height / _texture2D.Height + 1; nbVertical++)
        {
            var y = (int)_position.Y + nbVertical * _texture2D.Height;
            for (int nbHorizontal = 0; nbHorizontal < viewport.Width / _texture2D.Width + 1; nbHorizontal++)
            {
                var x = (int)_position.X + nbHorizontal * _texture2D.Width;
                var destinationRectangle = new Rectangle(x, y, _texture2D.Width, _texture2D.Height);

                spriteBatch.Draw(_texture2D, destinationRectangle, sourceRectangle, Color.White);
            }
        }

        _position.Y = (int)(_position.Y + SCROLLING_SPEED) % _texture2D.Height;
    }

}