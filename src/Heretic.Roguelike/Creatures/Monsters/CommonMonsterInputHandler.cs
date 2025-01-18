using System;
using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Creatures.Monsters;

public class CommonMonsterInputHandler : IInputHandler
{
    public void Process()
    {
        OnMovement?.Invoke(Vector.Zero);
    }

    public event Action<Vector>? OnMovement;
    public event Action<GameCommand>? OnCommand;
    public event Action? OnQuitGame;
}