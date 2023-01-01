using System;
using System.Collections.Generic;
using System.Reflection.Metadata;

using Microsoft.VisualBasic;
using Microsoft.Win32;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States;
using MonoGameMastery.GameEngine.Util;
using MonoGameMastery.VerticalShooter.States;


namespace MonoGameMastery.VerticalShooter.Objects;

public class ChopperSprite : BaseGameObject
{
    private const float SPEED = 4.0f;
    private Vector2 _direction = Vector2.Zero;
    private int age = 0;
    private List<(int, Vector2)> _path;

    private readonly List<BaseChopperPart> _bodyParts;

    private int _life = 40;
    private int _hitAt;



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

    public override void Update(GameTime gameTime) => _bodyParts.ForEach(x => x.Update(Position));

    public override void Draw(SpriteBatch spriteBatch) => _bodyParts.ForEach(x => x.Draw(spriteBatch, _texture2D));
}
