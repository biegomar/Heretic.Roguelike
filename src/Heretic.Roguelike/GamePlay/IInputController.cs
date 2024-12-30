namespace Heretic.Roguelike.GamePlay;

/// <summary>
/// Input controller that manages multiple input handlers like Keyboard or Gamepad.
/// </summary>
public interface IInputController
{
    /// <summary>
    /// Registers an input handler to listen for user input.
    /// </summary>
    /// <param name="handler">The specific handler to manage inputs.</param>
    void RegisterHandler(IInputHandler handler);

    /// <summary>
    /// Unregisters an input handler.
    /// </summary>
    /// <param name="handler">The handler to remove.</param>
    void UnregisterHandler(IInputHandler handler);

    /// <summary>
    /// Processes input from all handlers.
    /// </summary>
    void ProcessInput();
}