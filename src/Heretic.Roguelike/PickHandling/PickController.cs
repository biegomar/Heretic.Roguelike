using System;
using System.Collections.Generic;
using Heretic.Roguelike.Things.Interfaces;

namespace Heretic.Roguelike.PickHandling;

public class PickController<T>
{
    private readonly Dictionary<Type, IPickHandler<T>> pickHandlers = new();
    
    public void RegisterHandler<TItem>(IPickHandler<T> handler) where TItem : IThing<T>
    {
        pickHandlers.TryAdd(typeof(TItem), handler);
    }
    
    public void UnregisterHandler<TItem>() where TItem : IThing<T>
    {
        pickHandlers.Remove(typeof(TItem));
    }

    public bool ProcessPick(ICreature<T> creature, IThing<T> item)
    {
        if (pickHandlers.TryGetValue(item.GetType(), out var handler))
        {
            return handler.ProcessPick(creature, item);
        }
        
        return false;
    }
}