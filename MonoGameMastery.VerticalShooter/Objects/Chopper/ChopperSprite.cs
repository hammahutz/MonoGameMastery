using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Xml;

using Microsoft.VisualBasic;
using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States;
using MonoGameMastery.GameEngine.Util;
using MonoGameMastery.VerticalShooter.States;


namespace MonoGameMastery.VerticalShooter.Objects.Chopper;

public class ChopperSprite : BaseGameObject
{
    private const float SPEED = 4.0f;
    private Vector2 _direction = Vector2.Zero;
    private float _age = 0.0f;
    private readonly List<(int, Vector2)> _path;

    private readonly List<BaseChopperPart> _bodyParts;

    private int _life = 40;
    private int _hitAt;
    private Rectangle BB = new(-16, -63, 34, 98);



    public ChopperSprite(Texture2D texture2D, List<(int, Vector2)> path) : base(texture2D)
    {

        var chopperSprite = new Rectangle(0, 0, 44, 98);
        var bladeSprite = new Rectangle(133, 98, 94, 94);

        _bodyParts = new List<BaseChopperPart>()
        {
            new ChopperBody(chopperSprite, new Vector2(chopperSprite.Width / 2f, 34)),
            new ChopperBlades(bladeSprite, bladeSprite.Origin()),
        };

        _path = path;

        AddBoundingBox(new GameEngine.Objects.BoundingBox(BB.Location.ToVector2(), BB.Width, BB.Height));
    }

    public override void OnNotify(BaseGameStateEvent eventType)
    {
        switch (eventType)
        {
            case GamePlayEvents.ChopperHitBy m:
                JustHit(m.HitBy);
                SendEvent(new GamePlayEvents.EnemyLostLife(_life));
                break;
        }
    }

    private void JustHit(IGameObjectWithDamage gameObject)
    {
        _hitAt = 0;
        _life -= gameObject.Damage;
    }

    public override void Update(GameTime gameTime)
    {
        _path
            .Where(p => _age > p.Item1).ToList()
            .ForEach(p => _direction = p.Item2);

        if(_direction.Length() > 0.0f)
            _direction.Normalize();

        Position += _direction * SPEED;
        _age++;

        _bodyParts.ForEach(x => x.Update(Position));
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        DrawBoundingBoxes(spriteBatch);
        _bodyParts.ForEach(x => x.Draw(spriteBatch, _texture2D));
    }

    internal void Destroy()
    {
        throw new NotImplementedException();
    }
}
