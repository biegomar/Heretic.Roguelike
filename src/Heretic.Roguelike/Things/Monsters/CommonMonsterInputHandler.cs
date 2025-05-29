using System;
using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.Things.Monsters;

public class CommonMonsterInputHandler : IInputHandler
{
    public bool IsQuitGame { get; set; } = false;
    
    public void Process()
    {
        if (!IsQuitGame)
        {
            OnMovement?.Invoke(Vector.Zero);    
        }
    }

    public void ResetInputColor()
    {
        return;
    }

    public void SetInputColor(GameColor color)
    {
        return;
    }

    public string GetInputLine()
    {
        return string.Empty;
    }

    public event Action<Vector>? OnMovement;
    public event Action<GameCommand>? OnCommand;
    public event Action? OnQuitGame;
}