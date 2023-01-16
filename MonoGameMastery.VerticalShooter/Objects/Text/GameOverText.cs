using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Objects;

namespace MonoGameMastery.VerticalShooter.Objects.Text;

public class GameOverText : BaseTextObject { public GameOverText(SpriteFont font) : base(font) => Text = "Game Over"; }