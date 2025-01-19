using System;
using System.Collections.Generic;
using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Creatures.Players;

namespace Heretic.Roguelike.Battles;

public interface IBattleArena<T>
{
    void Fight(ICreature<T> attacker, ICreature<T> defender);
    void Fight(IList<ICreature<T>> attackerGroup, IList<ICreature<T>> defenderGroup);
    
    Action<string>? MessageHandler { get; set; }
    
    event Action<Monster<T>> OnKillMonster;
    event Action<Player<T>> OnKillPlayer;
}