using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameMastery.GameEngine.Util;

public static class DebugText
{
    private static string s_debugText;

    public static void AddDebugText<T>(string label, T text) => s_debugText = string.IsNullOrEmpty(s_debugText) ? label + ": " + text.ToString() : s_debugText + '\n' + label + ": " + text.ToString();

    public static void DrawDebugText(SpriteBatch spriteBatch, SpriteFont spriteFont)
    {
        spriteBatch.DrawString(spriteFont, s_debugText, new Vector2(10), Color.YellowGreen);
        s_debugText = "";
    }
}