using System;
using System.Collections.Generic;

using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.Input;
using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States;
using MonoGameMastery.GameEngine.Util;
using MonoGameMastery.VerticalShooter.Input;
using MonoGameMastery.VerticalShooter.Objects;
using MonoGameMastery.VerticalShooter.Objects.Chopper;
using MonoGameMastery.VerticalShooter.Particles;

using static MonoGameMastery.VerticalShooter.Globals;

namespace MonoGameMastery.VerticalShooter.States;

public class DevState : BaseGameState
{
    private PlayerSprite _player;
    private MissileSprite _missile;
    private ExhaustEmitter _exhaustEmitter;
    private ExplosionEmitter _explosionEmitter;
    private ChopperSprite _chopperSprite;
    private SpriteFont _debugFont;


    private Rectangle _debugRect = new Rectangle(1280 - 100, 720 - 100, 100, 100);
    private Texture2D _debugTexture;

    private string _debugText;
    private int _debugFrames;

    public override void LoadContent()
    {
        _exhaustEmitter = new ExhaustEmitter(LoadTexture(GFX_EXHAUST), new Vector2(300, 300));
        _explosionEmitter = new ExplosionEmitter(LoadTexture(GFX_EXPLOSION), new Vector2(300, 300));
        _player = new PlayerSprite(LoadTexture(GFX_PLAYER)) { Position = new Vector2(500, 600) };
        _chopperSprite = new ChopperSprite(LoadTexture(GFX_CHOPPER), new List<(int, Vector2)>()
        {
            (0, new Vector2()),
            (60, new Vector2(50, 50)),
            (240, new Vector2(50, -50)),
            (300, new Vector2(-50, -50)),
        });

        AddGameObject(_exhaustEmitter);
        AddGameObject(_player);
        AddGameObject(_chopperSprite);

        _debugFont = LoadAsset<SpriteFont>(FONT_DEBUG);
        _debugTexture = LoadAsset<Texture2D>(GFX_DEBUG);

    }
    protected override void SetInputManager() => InputManager = new InputManager(new DevInputMapper());
    public override void HandleInput(GameTime gameTime)
    {
        InputManager.GetCommands(cmd =>
        {
            if (cmd is DevInputCommand.DevQuit)
            {
                NotifyEvent(new BaseGameStateEvent.GameQuit());
            }
            if (cmd is DevInputCommand.DevShoot)
            {

                _missile = new MissileSprite(LoadTexture(GFX_MISSILE), LoadTexture(GFX_EXHAUST))
                {
                    Position = new Vector2(_player.Position.X, _player.Position.Y - 25)
                };
                AddGameObject(_missile);

                _explosionEmitter = new ExplosionEmitter(LoadTexture(GFX_EXPLOSION), new Vector2(100, 100));
                AddGameObject(_explosionEmitter);
            }
        });
    }

    public override void UpdateGameState(GameTime gameTime)
    {
        UpdateDebugText(gameTime);

        _exhaustEmitter.Update(gameTime);
        _explosionEmitter.Update(gameTime);
        _exhaustEmitter.Position = new Vector2(_exhaustEmitter.Position.X, _exhaustEmitter.Position.Y - 3f);
        if (_exhaustEmitter.Position.Y < 50)
        {
            RemoveGameObject(_exhaustEmitter);
        }

        if (_missile != null)
        {
            _missile.Update(gameTime);
            if (_missile.Position.Y < -100)
            {
                RemoveGameObject(_missile);
            }
        }

        _chopperSprite.Update(gameTime);

    }

    private void UpdateDebugText(GameTime gameTime)
    {
        _debugText = "";

        DebugText.AddDebugText("FPS", (int)(1 / gameTime.ElapsedGameTime.TotalSeconds));
        DebugText.AddDebugText("Frame time milliseconds", gameTime.ElapsedGameTime.Milliseconds);
        DebugText.AddDebugText("Total Frames", _debugFrames);
        DebugText.AddDebugText("Game time", (int)gameTime.TotalGameTime.TotalSeconds);
        DebugText.AddDebugText("Is running slowly?", gameTime.IsRunningSlowly);
        DebugText.AddDebugText("Active particles", _exhaustEmitter.ActiveParticles);
        DebugText.AddDebugText("Inactive particles", _exhaustEmitter.InactiveParticles);
        DebugText.AddDebugText("Mouse Position: ", Mouse.GetState().Position);

        _debugFrames++;
    }
    public override void DrawGameState(SpriteBatch spriteBatch)
    {
        spriteBatch.DrawString(_debugFont, _debugText, new Vector2(10), Color.YellowGreen);
        spriteBatch.Draw(_debugTexture, _debugRect, null, Color.White, 0, Vector2.Zero, SpriteEffects.None, 0.0f);
        DebugText.DrawDebugText(spriteBatch, _debugFont);
    }
}