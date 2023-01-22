using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

using MonoGameMastery.GameEngine.States;

namespace MonoGameMastery.VerticalShooter.Levels;

public class LevelReader
{
    private int _viewportWidth;
    private int _viewportHeight;

    private const int NB_ROWS = 11;
    private const int NB_TILES_ROWS = 10;

    public LevelReader(int viewportWidth) => _viewportWidth = viewportWidth;


    public List<List<BaseGameStateEvent>> LoadLevel(int nb)
    {
        string levelString = ReadLevel(nb);

        List<string> rows = levelString.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).ToList();
        var convertedRows = rows
                            .Select(r => ToEventRow(r))
                            .Reverse()
                            .ToList();

        return convertedRows;
    }

    private string ReadLevel(int nb)
    {
        Assembly assembly = Assembly.GetExecutingAssembly();
        string assemblyName = assembly.FullName.Split(',')[0];
        string filename = $"{assemblyName}.Levels.LevelData.Level{nb}.txt";

        Stream stream = assembly.GetManifestResourceStream(filename);
        StreamReader reader = new StreamReader(stream);
        return reader.ReadToEnd();
    }

    private List<BaseGameStateEvent> ToEventRow(string rowString)
    {
        string[] elements = rowString.Split(',');
        var newRow = new List<BaseGameStateEvent>();

        for (int i = 0; i < NB_ROWS; i++)
        {
            newRow.Add(ToEvent(i, elements[i]));
        }

        return newRow;
    }

    private BaseGameStateEvent ToEvent(int elementNumber, string input)
    {
        switch (input)
        {
            case "0":
                return new BaseGameStateEvent.Nothing();
            case "_":
                return new LevelEvents.NoRow();
            case "1":
                int xPosition = elementNumber * _viewportWidth / NB_TILES_ROWS;
                return new LevelEvents.GenderateTurret(xPosition);
            case "s":
                return new LevelEvents.StartLevel();
            case "e":
                return new LevelEvents.EndLevel();
            case string g when g.StartsWith('g'):
                var nb = int.Parse(g.Substring(1));
                return new LevelEvents.GenerateEnemies(nb);
            default:
                return new BaseGameStateEvent.Nothing();
        }
    }

}