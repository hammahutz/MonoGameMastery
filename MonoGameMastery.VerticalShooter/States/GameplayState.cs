using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.Input;
using MonoGameMastery.GameEngine.Objects;

namespace MonoGameMastery.GameEngine.States
{
    public class GameplayState : BaseGameState
    {
        private const string PlayerFighter = "gfx/fighter";
        private const string Background = "gfx/Barren";
        private const string BulletTexture = "gfx/bullet";

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
            _terrainBackground = new TerrainBackground(LoadTexture(Background));
            _playerSprite = new PlayerSprite(LoadTexture(PlayerFighter));

            AddGameObject(_terrainBackground);
            AddGameObject(_playerSprite);

            _bulletTexture = LoadTexture(BulletTexture);
            _bulletList = new List<BulletSprite>();

            _playerSprite.Position = new Vector2(_viewportWidth / 2 - _playerSprite.Width / 2, _viewportHeight / 2 - _playerSprite.Height / 2 - 30);
        }

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GamePlayInputCommand.GameExit)
                    NotifyEvent(Events.QUIT_GAME);
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

        public override void Update(GameTime gameTime)
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