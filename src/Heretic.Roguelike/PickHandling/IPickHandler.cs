using System;
using Heretic.Roguelike.Things.Interfaces;

namespace Heretic.Roguelike.PickHandling;

public interface IPickHandler<T>
{
    Action<string>? MessageHandler { get; set; }
    
    bool ProcessPick(ICreature<T> creature, IThing<T> item);
}