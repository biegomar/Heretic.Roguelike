using System;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.GamePlay;

/// <summary>
/// Defines a specific input handler for a particular device or input type.
/// </summary>
public interface IInputHandler
{
    /// <summary>
    /// Processes input from a specific source (e.g., Keyboard, Gamepad).
    /// </summary>
    void Process();

    /// <summary>
    /// Occurs when a movement input is detected.
    /// </summary>
    event Action<Vector> OnMovement;

    /// <summary>
    /// Occurs when a command input is detected.
    /// </summary>
    event Action<GameCommand> OnCommand;

    /// <summary>
    /// Occurs when the GameCommand.Quit is detected.
    /// </summary>
    event Action OnQuitGame;
}