using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.SimpleConsoleSample.Utils;

public static class ColorMapper
{
    private static readonly Dictionary<GameColor, ConsoleColor> ColorMap = new()
    {
        { GameColor.OCBlack, ConsoleColor.Black },
        { GameColor.OCWhite, ConsoleColor.White },
        { GameColor.OCRed, ConsoleColor.Red },
        { GameColor.OCGreen, ConsoleColor.Green },
        { GameColor.OCBlue, ConsoleColor.Blue },
        { GameColor.OCYellow, ConsoleColor.Yellow },
        { GameColor.OCGray, ConsoleColor.Gray },
        { GameColor.OCDefault, ConsoleColor.Gray } 
    };

    public static ConsoleColor GetColor(GameColor color) => ColorMap.GetValueOrDefault(color, ConsoleColor.Gray);
}