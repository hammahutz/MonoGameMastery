using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

using MonoGameMastery.GameEngine.Input;
using MonoGameMastery.GameEngine.Objects;
using MonoGameMastery.GameEngine.States;
using MonoGameMastery.VerticalShooter.Objects;
using MonoGameMastery.VerticalShooter.Objects.Chopper;
using MonoGameMastery.VerticalShooter.Objects.Text;
using MonoGameMastery.VerticalShooter.Particles;
using MonoGameMastery.VerticalShooter.Util;

namespace MonoGameMastery.VerticalShooter.States
{
    public class GameplayState : BaseGameState
    {
        private const int VIEWPORT_WIDTH = 1280;
        private const int VIEWPORT_HEIGHT = 720;
        public const double FIRE_RATE = 0.2;
        public const double MISSILE_FIRE_RATE = 1.0;
        private const int MAX_EXPLOSION_AGE = 600;
        private const int EXPLOSION_ACTIVE_LENGTH = 75;
        private const int STARTING_PLAYER_LIVES = 3;

        private int _playerLives = STARTING_PLAYER_LIVES;
        private bool _isShooting;
        private bool _isShootingMissile;
        private TimeSpan _lastShotAt;
        private TimeSpan _lastShotMissileAt;
        private ChopperGenerator _chopperGenerator;

        private TerrainBackground _terrainBackground;
        private PlayerSprite _playerSprite;
        private Texture2D _bulletTexture;
        private Texture2D _missileTexture;
        private Texture2D _exhaustTexture;
        private Texture2D _chopperTexture;
        private Texture2D _explosionTexture;
        private LivesText _livesText;

        private List<BulletSprite> _bulletList;
        private List<MissileSprite> _missileList;
        private List<ExplosionEmitter> _explosionList;
        private List<ChopperSprite> _enemyList;

        private bool _playerDead;
        private bool _gameOver = false;
        private Texture2D _screenBoxTexture;

        public override void LoadContent()
        {
            _terrainBackground = new TerrainBackground(LoadTexture(Assets.GFX_BACKGROUND));
            _bulletTexture = LoadTexture(Assets.GFX_BULLET);
            _playerSprite = new PlayerSprite(LoadTexture(Assets.GFX_PLAYER));
            _missileTexture = LoadTexture(Assets.GFX_MISSILE);
            _exhaustTexture = LoadTexture(Assets.GFX_EXHAUST);
            _explosionTexture = LoadTexture(Assets.GFX_EXPLOSION);
            _chopperTexture = LoadTexture(Assets.GFX_CHOPPER);

            _livesText = new LivesText(LoadAsset<SpriteFont>(Assets.FONT_LIVES))
            {
                Lives = _playerLives,
                Position = new Vector2(10.0f, 10.0f)
            };
            AddObject(_livesText);

            _bulletList = new List<BulletSprite>();
            _missileList = new List<MissileSprite>();
            _explosionList = new List<ExplosionEmitter>();
            _enemyList = new List<ChopperSprite>();

            _playerSprite.Position = new Vector2(VIEWPORT_WIDTH / 2, VIEWPORT_HEIGHT - _playerSprite.Height - 10);
            _chopperGenerator = new ChopperGenerator(_chopperTexture, 4, AddChopper);

            AddObject(_terrainBackground);
            AddObject(_playerSprite);
            _chopperGenerator.GenerateChoppers();

            _soundManager.SetSoundTrack(new List<SoundEffectInstance>()
            {
                LoadSounds(Assets.MUSIC_FUTURE_AMBIENT1).CreateInstance(),
                LoadSounds(Assets.MUSIC_FUTURE_AMBIENT2).CreateInstance(),
                LoadSounds(Assets.MUSIC_FUTURE_AMBIENT3).CreateInstance(),
                LoadSounds(Assets.MUSIC_FUTURE_AMBIENT4).CreateInstance(),
            });

            _soundManager.RegisterSound(new GamePlayEvents.PlayerShoot(), LoadSounds(Assets.SFX_BULLET));
            _soundManager.RegisterSound(new GamePlayEvents.PlayerShootMissile(), LoadSounds(Assets.SFX_MISSILE)); //TODO WHAT WHY DON't //TODO WHAT WHY DON'T I HAVE IMPLEMENT THIS IN PAGE 186!?!?!?!?!?!?!?
        }

        private void AddChopper(ChopperSprite chopper)
        {
            chopper.OnObjectChanged += _chopperSprite_OnObjectChanged;
            _enemyList.Add(chopper);
            AddObject(chopper);
        }

        private void _chopperSprite_OnObjectChanged(object sender, BaseGameStateEvent e)
        {
            var chopper = (ChopperSprite)sender;

            switch (e)
            {
                case GamePlayEvents.EnemyLostLife ge:
                    if (ge.CurrentLife <= 0)
                    {
                        AddExplosion(new Vector2(chopper.Position.X - 40, chopper.Position.Y - 40));
                        chopper.Destroy();
                    }
                    break;
            }
        }

        private void AddExplosion(Vector2 position)
        {
            var explosion = new ExplosionEmitter(_explosionTexture, position);
            _explosionList.Add(explosion);
            AddObject(explosion);
        }

        private void UpdateExplosion(GameTime gameTime)
        {
            foreach (var explosion in _explosionList)
            {
                explosion.Update(gameTime);
                if (explosion.Age > EXPLOSION_ACTIVE_LENGTH)
                {
                    explosion.Deactivate();
                }
                if (explosion.Age > MAX_EXPLOSION_AGE)
                {
                    RemoveGameObject(explosion);
                }
            }
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
                if (cmd is GamePlayInputCommand.PlayerShootsMissile)
                    ShootMissile(gameTime);
                if (cmd is GamePlayInputCommand.PlayerStopsMoving)
                    _playerSprite.StopMoving();
            });
        }

        public override void DrawGameState(SpriteBatch spriteBatch)
        {
            if (_gameOver)
            {
                Texture2D screenBoxTexture = GetScreenBoxTexture(spriteBatch.GraphicsDevice);
                Rectangle viewportRectangle = new(0,0,VIEWPORT_WIDTH,VIEWPORT_HEIGHT);
                spriteBatch.Draw(screenBoxTexture, viewportRectangle, Color.Black * 0.3f);

            }
        }

        private Texture2D GetScreenBoxTexture(GraphicsDevice graphicsDevice)
        {
            if (_screenBoxTexture == null)
            {
                _screenBoxTexture = new Texture2D(graphicsDevice, 1, 1);
                _screenBoxTexture.SetData<Color>(new Color[] { Color.White });
            }
            return _screenBoxTexture;
        }

        public override void UpdateGameState(GameTime gameTime)
        {
            UpdateProjectiles(gameTime, _bulletList, FIRE_RATE, _lastShotAt, ref _isShooting);
            UpdateProjectiles(gameTime, _missileList, MISSILE_FIRE_RATE, _lastShotMissileAt, ref _isShootingMissile);
            UpdateExplosion(gameTime);
            _enemyList.ForEach(x => x.Update(gameTime));

            _bulletList = CleanObjects(_bulletList);
            _missileList = CleanObjects(_missileList);
            _enemyList = CleanObjects(_enemyList);
            _explosionList = CleanObjects(_explosionList);

            DetectCollision();

            _playerSprite.Update(gameTime);
        }

        private void DetectCollision()
        {
            var bulletCollsionDetector = new AABBCollisionDetector<BulletSprite, ChopperSprite>(_bulletList);
            var missileCollsionDetector = new AABBCollisionDetector<MissileSprite, ChopperSprite>(_missileList);
            var playerCollsionDetector = new AABBCollisionDetector<ChopperSprite, PlayerSprite>(_enemyList);

            bulletCollsionDetector.DetectCollisions(_enemyList, (bullet, choppper) =>
            {
                var hitEvent = new GamePlayEvents.ChopperHitBy(bullet);
                choppper.OnNotify(hitEvent);
                _soundManager.OnNotify(hitEvent);
                bullet.Destroy();
            });

            missileCollsionDetector.DetectCollisions(_enemyList, (missile, choppper) =>
            {
                var hitEvent = new GamePlayEvents.ChopperHitBy(missile);
                choppper.OnNotify(hitEvent);
                _soundManager.OnNotify(hitEvent);
                missile.Destroy();
            });
            playerCollsionDetector.DetectCollisions(_playerSprite, (chopper, player) => { KillPlayer(); });
        }

        private async void KillPlayer()
        {
            Console.WriteLine("Kill Player");
            _playerLives -= 1;
            if (_playerLives > 0)
            {
                _playerDead = true;
                AddExplosion(_playerSprite.Position);
                RemoveGameObject(_playerSprite);

                await Task.Delay(TimeSpan.FromSeconds(2));
                ResetGame();
            }
            else
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            AddObject(new GameOverText(LoadAsset<SpriteFont>(Assets.FONT_GAME_OVER))
            {
                Position = new Vector2(460, 300)
            });
            _gameOver = true;
        }

        private void ResetGame()
        {
            Console.WriteLine("Reset Game");
        }

        protected List<T> CleanObjects<T>(List<T> objectList) where T : BaseGameObject
        {
            (from x in objectList
             where !IsWithinBounds(x) || x.Destroyed
             select x).ToList()
             .ForEach(x => RemoveGameObject(x));

            return (from x in objectList where IsWithinBounds(x) && !x.Destroyed select x).ToList();
        }

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
            AddObject(missileSprite);
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

            AddObject(bulletSpriteLeft);
            AddObject(bulletSpriteRight);
        }

        protected override void SetInputManager() => InputManager = new InputManager(new GameplayInputMapper());

        private void KeepPlayerInBounds()
        {
            if (_playerSprite.Position.X < 0)
                _playerSprite.Position = new Vector2(0, _playerSprite.Position.Y);
            if (_playerSprite.Position.X + _playerSprite.Width > VIEWPORT_WIDTH)
                _playerSprite.Position = new Vector2(VIEWPORT_WIDTH - _playerSprite.Width, _playerSprite.Position.Y);
            if (_playerSprite.Position.Y < 0)
                _playerSprite.Position = new Vector2(_playerSprite.Position.X, 0);
            if (_playerSprite.Position.Y < 0)
                _playerSprite.Position = new Vector2(_playerSprite.Position.X, VIEWPORT_WIDTH - _playerSprite.Position.Y);
        }
    }
}