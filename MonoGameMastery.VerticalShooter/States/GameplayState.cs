using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.Input;
using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.VerticalShooter.States
{
    public class GameplayState : BaseGameState
    {
        private const string GfxPlayer = "gfx/fighter";
        private const string GfxBackground = "gfx/Barren";
        private const string GfxBullet = "gfx/bullet";

        private const string MusicFutureAmbient1 = "music/FutureAmbient_1";
        private const string MusicFutureAmbient2 = "music/FutureAmbient_2";
        private const string MusicFutureAmbient3 = "music/FutureAmbient_3";
        private const string MusicFutureAmbient4 = "music/FutureAmbient_4";

        private const string SfxBullet = "sfx/bullet";
        private const string SfxEmpty = "sfx/empty";
        private const string SfxMissile = "sfx/missile";

        private const int _viewportWidth = 1280;
        private const int _viewportHeight = 720;


        private TerrainBackground _terrainBackground;
        private PlayerSprite _playerSprite;
        private Texture2D _bulletTexture;
        private List<BulletSprite> _bulletList;
        private bool _isShooting;
        private TimeSpan _lastShotAt;

        public const double FIRE_RATE = 0.2;

        public override void LoadContent()
        {
            _terrainBackground = new TerrainBackground(LoadTexture(GfxBackground));
            _playerSprite = new PlayerSprite(LoadTexture(GfxPlayer));

            AddGameObject(_terrainBackground);
            AddGameObject(_playerSprite);

            _bulletTexture = LoadTexture(GfxBullet);
            _bulletList = new List<BulletSprite>();

            _playerSprite.Position = new Vector2(_viewportWidth / 2 - _playerSprite.Width / 2, _viewportHeight / 2 - _playerSprite.Height / 2 - 30);

            _soundManager.SetSoundTrack(new List<SoundEffectInstance>()
            {
                LoadSounds(MusicFutureAmbient1).CreateInstance(),
                LoadSounds(MusicFutureAmbient2).CreateInstance(),
                LoadSounds(MusicFutureAmbient3).CreateInstance(),
                LoadSounds(MusicFutureAmbient4).CreateInstance(),
            });

            _soundManager.RegisterSound(new GamePlayEvents.PlayerShoot(), LoadSounds(SfxBullet));
        }

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GamePlayInputCommand.GameExit)
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                if (cmd is GamePlayInputCommand.PlayerMoveLeft)
                {
                    _playerSprite.MoveLeft();
                    KeepPlayerInBounds();
                }
                if (cmd is GamePlayInputCommand.PlayerMoveRight)
                {
                    _playerSprite.MoveRight();
                    KeepPlayerInBounds();
                }
                if (cmd is GamePlayInputCommand.PlayerShoots)
                    Shoot(gameTime);
            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            UpdateBullets(gameTime);
            RemoveDeadBullets();
        }

        private void RemoveDeadBullets()
        {
            var newBulletList = new List<BulletSprite>();
            foreach (var bullet in _bulletList)
            {
                var bulletStillOnScreen = bullet.Position.Y > -30;
                if (bulletStillOnScreen)
                    newBulletList.Add(bullet);
                else
                    RemoveGameObject(bullet);
            }

            _bulletList = newBulletList;
        }

        private void UpdateBullets(GameTime gameTime)
        {
            _bulletList.ForEach(bullet => bullet.MoveUp());

            var totalGameTime = gameTime.TotalGameTime - _lastShotAt;
            var timeSpan = TimeSpan.FromSeconds(FIRE_RATE);

            if (totalGameTime > timeSpan)
            {
                _isShooting = false;
            }
        }

        private void Shoot(GameTime gameTime)
        {
            if (!_isShooting)
            {
                CreateBullets();
                _isShooting = true;
                _lastShotAt = gameTime.TotalGameTime;

                NotifyEvent(new GamePlayEvents.PlayerShoot());
            }
        }

        private void CreateBullets()
        {
            var bulletSpriteLeft = new BulletSprite(_bulletTexture);
            var bulletSpriteRight = new BulletSprite(_bulletTexture);

            var bulletY = _playerSprite.Position.Y + 30;
            var bulletLeftX = _playerSprite.Position.X + _playerSprite.Width / 2 + 10;
            var bulletRightX = _playerSprite.Position.X + _playerSprite.Width / 2 - 40;

            bulletSpriteLeft.Position = new Vector2(bulletLeftX, bulletY);
            bulletSpriteRight.Position = new Vector2(bulletRightX, bulletY);

            _bulletList.Add(bulletSpriteLeft);
            _bulletList.Add(bulletSpriteRight);

            AddGameObject(bulletSpriteLeft);
            AddGameObject(bulletSpriteRight);


        }

        protected override void SetInputManager() => InputManager = new InputManager(new GameplayInputMapper());

        private void KeepPlayerInBounds()
        {
            if (_playerSprite.Position.X < 0)
                _playerSprite.Position = new Vector2(0, _playerSprite.Position.Y);
            if (_playerSprite.Position.X + _playerSprite.Width > _viewportWidth)
                _playerSprite.Position = new Vector2(_viewportWidth - _playerSprite.Width, _playerSprite.Position.Y);
            if (_playerSprite.Position.Y < 0)
                _playerSprite.Position = new Vector2(_playerSprite.Position.X, 0);
            if (_playerSprite.Position.Y < 0)
                _playerSprite.Position = new Vector2(_playerSprite.Position.X, _viewportWidth - _playerSprite.Position.Y);
        }

    }
}