using System;
using Heretic.Roguelike.Things.Common;
using Heretic.Roguelike.Things.Interfaces;
using Heretic.Roguelike.Things.Players;

namespace Heretic.Roguelike.GamePlay.PickHandling;

public class GoldPickHandler<T> : IPickHandler<T>
{
    public Action<string>? MessageHandler { get; set; }

    public bool ProcessPick(ICreature<T> creature, IThing<T> item)
    {
        if (creature is Player<T> player && item is Gold<T> gold)
        {
            player.Gold += (uint)gold.ActualValue;
            MessageHandler?.Invoke($"You found {gold.ActualValue} gold pieces.");
            return true;
        }
        
        return false;
    }
}