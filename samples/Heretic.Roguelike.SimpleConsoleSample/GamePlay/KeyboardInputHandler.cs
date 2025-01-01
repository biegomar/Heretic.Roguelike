using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.SimpleConsoleSample.GamePlay;

public class KeyboardInputHandler : IInputHandler
{
    public void Process()
    {
        if (!Console.KeyAvailable)
        {
            return;
        }

        var key = ReadConsoleKey();
        
        ProcessDirectionalInput(key);
        
        ProcessGameCommands(key);
    }

    private static ConsoleKey ReadConsoleKey()
    {
        return Console.ReadKey(true).Key;
    }

    private void ProcessGameCommands(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.Escape:
            case ConsoleKey.Q:
                OnCommand?.Invoke(GameCommand.Quit);
                break;
            case ConsoleKey.P:
                OnCommand?.Invoke(GameCommand.Pause);
                break;
            case ConsoleKey.I:
                OnCommand?.Invoke(GameCommand.OpenInventory);
                break;
        }
    }

    private void ProcessDirectionalInput(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
            case ConsoleKey.W:
                OnMovement?.Invoke(new Vector(0, -1, 0));
                break;
            case ConsoleKey.LeftArrow:
            case ConsoleKey.A:
                OnMovement?.Invoke(new Vector(-1, 0, 0)); 
                break;
            case ConsoleKey.DownArrow:
            case ConsoleKey.S:
                OnMovement?.Invoke(new Vector(0, 1, 0)); 
                break;
            case ConsoleKey.RightArrow:
            case ConsoleKey.D:
                OnMovement?.Invoke(new Vector(1, 0, 0)); 
                break;
        }
    }

    public event Action<Vector>? OnMovement;
    public event Action<GameCommand>? OnCommand;
}