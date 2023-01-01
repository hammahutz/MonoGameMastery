using Microsoft.Xna.Framework;

namespace MonoGameMastery.VerticalShooter.Objects;

public class ChopperBlades : BaseChopperPart
{
    public float BladeSpeed { get; set; } = 0.2f;

    public ChopperBlades(Rectangle sourceRectangle, Vector2 origin) : base(sourceRectangle, origin)
    {
        BladeSpeed = 0.2f;
    }

    protected override void UpdatePart(Vector2 position) => _rotation += BladeSpeed;
}