using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Objects.Base;

namespace MonoGameMastery.GameEngine.Objects
{
    public class PlayerSprite : BaseGameObject
    {
        public PlayerSprite(Texture2D texture2D) => _texture2D = texture2D;
    }
}