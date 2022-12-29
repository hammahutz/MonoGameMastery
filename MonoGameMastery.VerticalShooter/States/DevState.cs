using System;

using Microsoft.VisualBasic.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Input;
using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States;
using MonoGameMastery.VerticalShooter.Input;
using MonoGameMastery.VerticalShooter.Objects;
using MonoGameMastery.VerticalShooter.Particles;

namespace MonoGameMastery.VerticalShooter.States;

public class DevState : BaseGameState
{
    private const string GFX_EXHAUST = "gfx/Cloud001";
    private const string GFX_MISSILE = "gfx/Missile05";
    private const string GFX_FIGHTER = "gfx/Fighter";
    private const string FONT_DEBUG = "font/font";


    private PlayerSprite _player;
    private MissileSprite _missile;
    private ExhaustEmitter _exhaustEmitter;
    private SpriteFont _debugFont;

    private string _debugText;

    public override void LoadContent()
    {
        _exhaustEmitter = new ExhaustEmitter(LoadTexture(GFX_EXHAUST), new Vector2(300, 300));
        _player = new PlayerSprite(LoadTexture(GFX_FIGHTER)) { Position = new Vector2(500, 500) };

        AddGameObject(_exhaustEmitter);
        AddGameObject(_player);

        _debugFont = LoadAsset<SpriteFont>(FONT_DEBUG);
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
            }
        });
    }

    public override void UpdateGameState(GameTime gameTime)
    {
        UpdateDebugText(gameTime);

        _exhaustEmitter.Update(gameTime);
        _exhaustEmitter.Position = new Vector2(_exhaustEmitter.Position.X, _exhaustEmitter.Position.Y - 3f);
        if (_exhaustEmitter.Position.Y < 50)
        {
            RemoveGameObject(_exhaustEmitter);
        }

        if(_missile != null)
        {
            _missile.Update(gameTime);
            if (_missile.Position.Y < -100)
            {
                RemoveGameObject(_missile);
            }
        }
    }

    private void UpdateDebugText(GameTime gameTime)
    {
        _debugText = "";

        AddDebugText("FPS", 1 / gameTime.ElapsedGameTime.TotalSeconds);
        AddDebugText("Frame time milliseconds", gameTime.ElapsedGameTime.Milliseconds);
        AddDebugText("Is running slowly?", gameTime.IsRunningSlowly);
        AddDebugText("Active particles", _exhaustEmitter.ActiveParticles);
        AddDebugText("Inactive particles", _exhaustEmitter.InactiveParticles);
    }

    private void AddDebugText<T>(string label, T text) => _debugText = String.IsNullOrEmpty(_debugText) ? label + ": " + text.ToString() : _debugText + '\n' + label + ": " + text.ToString();

    public override void DrawGameState(SpriteBatch spriteBatch) => spriteBatch.DrawString(_debugFont, _debugText, Vector2.Zero, Color.YellowGreen);




}