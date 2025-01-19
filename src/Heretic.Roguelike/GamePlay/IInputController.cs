using Heretic.Roguelike.Things;

namespace Heretic.Roguelike.GamePlay;

/// <summary>
/// Input controller that manages multiple input handlers like Keyboard or Gamepad.
/// </summary>
public interface IInputController<T>
{
    /// <summary>
    /// Registers an input handler to listen for user input.
    /// </summary>
    /// <param name="handler">The specific handler to manage inputs.</param>
    /// <param name="creature"></param>
    void RegisterHandler(IInputHandler handler, ICreature<T> creature);

    /// <summary>
    /// Unregisters an input handler.
    /// </summary>
    /// <param name="handler">The handler to remove.</param>
    void UnregisterHandler(IInputHandler handler);
    
    void UnregisterCreatureFromHandler(ICreature<T> creature);

    /// <summary>
    /// Processes input from all handlers.
    /// </summary>
    void ProcessInput();
}