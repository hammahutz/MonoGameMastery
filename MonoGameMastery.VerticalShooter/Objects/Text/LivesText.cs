using System.Reflection.Metadata.Ecma335;

using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Objects;

namespace MonoGameMastery.VerticalShooter.Objects.Text;

public class LivesText : BaseTextObject
{
    private int _lives = -1;

    public int Lives
    {
        get => _lives; set
        {
            _lives = value;
            Text = $"Lives: {_lives}";
        }
    }
    public LivesText(SpriteFont font) : base(font) { }
}