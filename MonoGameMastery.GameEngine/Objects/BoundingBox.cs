using Microsoft.Xna.Framework;

namespace MonoGameMastery.GameEngine.Objects;

public class BoundingBox
{
    public Vector2 Position { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public Rectangle Rectangle { get => new Rectangle(Position.ToPoint(), new Point((int)Width, (int)Height)); }

    public BoundingBox(Vector2 position, float width, float height)
    {
        Position = position;
        Width = width;
        Height = height;
    }

    public BoundingBox(Point position, Point size)
    {
        Position = position.ToVector2();
        Width = size.X;
        Height = size.Y;
    }

    public BoundingBox(Rectangle rectangle)
    {
        Position = rectangle.Location.ToVector2();
        Width = rectangle.Size.X;
        Height = rectangle.Size.Y;
    }

    public bool CollidesWith(BoundingBox bb) => Position.X < bb.Position.X + bb.Width &&
                                                Position.X + Width > bb.Position.X &&
                                                Position.Y < bb.Position.Y + bb.Height &&
                                                Position.Y + Height > bb.Position.Y;
}