using System;
using Heretic.Roguelike.Things.Interfaces;
using Heretic.Roguelike.Things.Players;

namespace Heretic.Roguelike.GamePlay.PickHandling;

public interface IPickHandler<T>
{
    Action<string>? MessageHandler { get; set; }
    
    bool ProcessPick(ICreature<T> creature, IThing<T> item);
}