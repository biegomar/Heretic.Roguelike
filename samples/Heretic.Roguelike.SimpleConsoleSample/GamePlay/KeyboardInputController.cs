using Heretic.Roguelike.GamePlay;

namespace Heretic.Roguelike.SimpleConsoleSample.GamePlay;

public class KeyboardInputController : IInputController
{
    private readonly List<IInputHandler> inputHandlers = [];

    public void RegisterHandler(IInputHandler handler)
    {
        if (!inputHandlers.Contains(handler))
        {
            inputHandlers.Add(handler);
        }
    }

    public void UnregisterHandler(IInputHandler handler)
    {
        if (inputHandlers.Contains(handler))
        {
            inputHandlers.Remove(handler);
        }
    }

    public void ProcessInput()
    {
        foreach (var handler in inputHandlers)
        {
            handler.Process();
        }
    }
}