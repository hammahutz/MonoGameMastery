using System;
using System.Collections.Generic;
using System.Timers;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using static MonoGameMastery.GameEngine.Util.MathUtil;

namespace MonoGameMastery.VerticalShooter.Objects.Chopper;

public class ChopperGenerator
{
    private readonly Texture2D _texture2D;

    private readonly int _nbChoppers;
    private int _maxNbChoppers;
    private int _choppersGenerated = 0;

    private bool _generateLeft = true;
    private bool _generating = false;

    private readonly Action<ChopperSprite> _chopperHandler;

    private readonly Timer _timer;

    public ChopperGenerator(Texture2D texture2D, int nbChoppers, Action<ChopperSprite> handler)
    {
        _texture2D = texture2D;
        _nbChoppers = nbChoppers;
        _chopperHandler = handler;

        _maxNbChoppers = nbChoppers;
        _timer = new Timer(500);
        _timer.Elapsed += _timer_Elapsed;
    }

    public void GenerateChoppers()
    {
        if (_generating)
        {
            return;
        }
        _choppersGenerated = 0;
        _timer.Start();
    }

    private void _timer_Elapsed(object sender, ElapsedEventArgs e)
    {
        (Vector2, Vector2, Vector2) chopperData = _generateLeft ?
        (Right, DownRight, new Vector2(-200, 100)) : (Left, DownLeft, new Vector2(1500, 100));

        List<(int, Vector2)> path = new List<(int, Vector2)>()
        {
            (0, chopperData.Item1),
            (2 * FRAME_SECOND, chopperData.Item2),
        };
        var chopper = new ChopperSprite(_texture2D, path) { Position = chopperData.Item3 };
        _chopperHandler(chopper);

        _generateLeft = !_generateLeft;
        _choppersGenerated++;

        if (_choppersGenerated == _maxNbChoppers)
        {
            StopGenerating();
        }
    }

    private void StopGenerating()
    {
        _timer.Stop();
        _generating = false;
    }
}