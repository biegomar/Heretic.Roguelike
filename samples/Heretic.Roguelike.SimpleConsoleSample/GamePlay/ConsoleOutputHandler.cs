using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.SimpleConsoleSample.Utils;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.SimpleConsoleSample.GamePlay;

public class ConsoleOutputHandler : IOutputHandler
{
    public void Output(string message)
    {
        Console.Write(message);
    }

    public void OutputLine(string message)
    {
        Console.WriteLine(message);
    }

    public void ResetOutputColor()
    {
        Console.ResetColor();
    }

    public void SetOutputColor(GameColor color)
    {
        Console.ForegroundColor = ColorMapper.GetColor(color);
    }
}