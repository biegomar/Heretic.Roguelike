using Heretic.Roguelike.GamePlay;
using Heretic.Roguelike.Things;

namespace Heretic.Roguelike.SimpleConsoleSample.GamePlay;

public class KeyboardInputController : IInputController<char>
{
    private readonly List<IInputHandler> inputHandlers = [];
    private readonly IDictionary<ICreature<char>, IInputHandler> creatureInputHandlers = new Dictionary<ICreature<char>, IInputHandler>();

    public void RegisterHandler(IInputHandler handler, ICreature<char> creature)
    {
        if (!inputHandlers.Contains(handler))
        {
            inputHandlers.Add(handler);
        }

        if (creatureInputHandlers.TryAdd(creature, handler))
        {
            handler.OnMovement += creature.Translate;
        }
    }

    public void UnregisterHandler(IInputHandler handler)
    {
        if (inputHandlers.Contains(handler))
        {
            inputHandlers.Remove(handler);
        }
    }

    public void UnregisterCreatureFromHandler(ICreature<char> creature)
    {
        if (creatureInputHandlers.TryGetValue(creature, out var handler))
        {
            handler.OnMovement -= creature.Translate;
            creatureInputHandlers.Remove(creature);
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