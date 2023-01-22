using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.VerticalShooter.Levels;

public class Level
{
    private readonly LevelReader _levelReader;
    private List<List<BaseGameStateEvent>> _currentLevel;
    private int _currentLevelNumber;
    private int _currentLevelRow;
    private TimeSpan _startGameTime;
    private readonly TimeSpan TickTimeSpan = new TimeSpan(0, 0, 2);

    public event EventHandler<LevelEvents.GenerateEnemies> OnGenereateEnemies;
    public event EventHandler<LevelEvents.GenderateTurret> OnGenderateTurret;
    public event EventHandler<LevelEvents.StartLevel> OnStartLevel;
    public event EventHandler<LevelEvents.EndLevel> OnEndLevel;
    public event EventHandler<LevelEvents.NoRow> OnNoRow;

    public Level(LevelReader reader)
    {
        _levelReader = reader;
        _currentLevelNumber = 1;
        _currentLevelRow = 0;

        _currentLevel = _levelReader.LoadLevel(_currentLevelNumber);
    }

    public void LoadNextLevel()
    {
        _currentLevelNumber++;
        _currentLevel = _levelReader.LoadLevel(_currentLevelNumber);
        Reset();
    }

    public void Reset() => _currentLevelRow = 0;

    public void GenerateLevelEvents(GameTime gameTime)
    {
        if (_startGameTime == null)
        {
            _startGameTime = gameTime.TotalGameTime;
        }
        if (gameTime.TotalGameTime - _startGameTime < TickTimeSpan)
        {
            return;
        }

        _startGameTime = gameTime.TotalGameTime;

        foreach (var e in _currentLevel[_currentLevelRow])
        {
            switch (e)
            {
                case LevelEvents.GenerateEnemies g:
                    OnGenereateEnemies?.Invoke(this, g);
                    break;

                case LevelEvents.GenderateTurret g:
                    OnGenderateTurret?.Invoke(this, g);
                    break;

                case LevelEvents.StartLevel start:
                    OnStartLevel?.Invoke(this, start);
                    break;

                case LevelEvents.EndLevel end:
                    OnEndLevel?.Invoke(this, end);
                    break;

                case LevelEvents.NoRow n:
                    OnNoRow?.Invoke(this, n);
                    break;
            }
        }
        _currentLevelRow++;
    }

}