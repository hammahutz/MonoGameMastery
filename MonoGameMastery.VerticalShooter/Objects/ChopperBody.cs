using System;

using Microsoft.Xna.Framework;

namespace MonoGameMastery.VerticalShooter.Objects;

public class ChopperBody : BaseChopperPart
{
    public ChopperBody(Rectangle sourceRectangle, Vector2 origin) : base(sourceRectangle, origin) => _rotation = MathHelper.Pi;
}