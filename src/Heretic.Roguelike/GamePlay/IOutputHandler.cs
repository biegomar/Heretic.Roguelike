using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.GamePlay;

public interface IOutputHandler
{
    void Output(string message);
    void OutputLine(string message);
    void ResetOutputColor();
    void SetOutputColor(GameColor color);
}