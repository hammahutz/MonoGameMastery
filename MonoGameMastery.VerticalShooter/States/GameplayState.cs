using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using MonoGameMastery.GameEngine.Input;
using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States;
using MonoGameMastery.VerticalShooter.Objects;

namespace MonoGameMastery.VerticalShooter.States
{
    public class GameplayState : BaseGameState
    {
        private const string GfxPlayer = "gfx/fighter";
        private const string GfxBackground = "gfx/Barren";
        private const string GfxBullet = "gfx/bullet";
        private const string GfxExhaust = "gfx/Cloud001";
        private const string GfxMissile = "gfx/Missile05";

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
        private Texture2D _missileTexture;
        private Texture2D _exhaustTexture;

        private List<BulletSprite> _bulletList;
        private List<MissileSprite> _missileList;
        private bool _isShooting;
        private bool _isShootingMissile;
        private TimeSpan _lastShotAt;
        private TimeSpan _lastShotMissileAt;

        public const double FIRE_RATE = 0.2;
        public const double MISSILE_FIRE_RATE = 1.0;

        public override void LoadContent()
        {
            _terrainBackground = new TerrainBackground(LoadTexture(GfxBackground));
            _playerSprite = new PlayerSprite(LoadTexture(GfxPlayer));

            AddGameObject(_terrainBackground);
            AddGameObject(_playerSprite);

            _bulletTexture = LoadTexture(GfxBullet);
            _bulletList = new List<BulletSprite>();

            _missileTexture = LoadTexture(GfxMissile);
            _missileList = new List<MissileSprite>();
            _exhaustTexture = LoadTexture(GfxExhaust);

            _playerSprite.Position = new Vector2(_viewportWidth / 2 - _playerSprite.Width / 2, _viewportHeight / 2 - _playerSprite.Height / 2 - 30);

            _soundManager.SetSoundTrack(new List<SoundEffectInstance>()
            {
                LoadSounds(MusicFutureAmbient1).CreateInstance(),
                LoadSounds(MusicFutureAmbient2).CreateInstance(),
                LoadSounds(MusicFutureAmbient3).CreateInstance(),
                LoadSounds(MusicFutureAmbient4).CreateInstance(),
            });

            _soundManager.RegisterSound(new GamePlayEvents.PlayerShoot(), LoadSounds(SfxBullet));
            _soundManager.RegisterSound(new GamePlayEvents.PlayerShootMissile(), LoadSounds(SfxMissile)); //TODO WHAT WHY DON't //TODO WHAT WHY DON'T I HAVE IMPLEMENT THIS IN PAGE 186!?!?!?!?!?!?!? 
        }

        public override void HandleInput(GameTime gameTime)
        {
            InputManager.GetCommands(cmd =>
            {
                if (cmd is GamePlayInputCommand.GameExit)
                    NotifyEvent(new BaseGameStateEvent.GameQuit());
                if (cmd is GamePlayInputCommand.PlayerMoveLeft)
                    _playerSprite.MoveLeft();
                KeepPlayerInBounds();
                if (cmd is GamePlayInputCommand.PlayerMoveRight)
                    _playerSprite.MoveRight();
                KeepPlayerInBounds();
                if (cmd is GamePlayInputCommand.PlayerShoots)
                    Shoot(gameTime);
                if (cmd is GamePlayInputCommand.PlayerShootsMissile)
                    ShootMissile(gameTime);


            });
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            UpdateProjectiles(gameTime, _bulletList, FIRE_RATE, _lastShotAt, ref _isShooting);
            UpdateProjectiles(gameTime, _missileList, MISSILE_FIRE_RATE, _lastShotMissileAt, ref _isShootingMissile);

            CleanObjects(_bulletList);
            CleanObjects(_missileList);
        }


        // private List<T> CleanObjects<T>(List<T> objectList) where T : BaseGameObject
        // {
        //     List<T> listOfItemsToKeep = new List<T>();
        //     foreach (T gameObject in objectList)
        //     {
        //         var stillOnScreen = gameObject.Position.Y > -50;
        //         if (stillOnScreen)
        //         {
        //             listOfItemsToKeep.Add(gameObject);
        //         }
        //         else
        //         {
        //             RemoveGameObject(gameObject);
        //         }
        //     }
        //     return listOfItemsToKeep;
        // }

        private static List<T> CleanObjects<T>(List<T> objectList) where T : BaseGameObject => (from x in objectList where IsWithinBounds(x) select x).ToList();
        private static bool IsWithinBounds<T>(T gameObject) where T : BaseGameObject => gameObject.Position.Y > -50;



        private static void UpdateProjectiles<T>(GameTime gameTime, List<T> projectiles, double fireRate, TimeSpan lastShotAt, ref bool isShooting) where T : BaseGameObject
        {
            projectiles.ForEach(p => p.Update(gameTime));

            var totalGameTime = gameTime.TotalGameTime - lastShotAt;
            var timeSpan = TimeSpan.FromSeconds(fireRate);

            if (totalGameTime > timeSpan)
            {
                isShooting = false;
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
        private void ShootMissile(GameTime gameTime)
        {
            if (!_isShootingMissile)
            {
                CreateMissile();
                _isShootingMissile = true;
                _lastShotMissileAt = gameTime.TotalGameTime;

                NotifyEvent(new GamePlayEvents.PlayerShootMissile());
            }
        }

        private void CreateMissile()
        {
            var missileSprite = new MissileSprite(_missileTexture, _exhaustTexture)
            {
                Position = new Vector2(_playerSprite.Position.X + 33, _playerSprite.Position.Y - 25),
            };

            _missileList.Add(missileSprite);
            AddGameObject(missileSprite);
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