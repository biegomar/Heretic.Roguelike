using System;
using System.Collections.Generic;
using Heretic.Roguelike.Things;
using Heretic.Roguelike.Things.Monsters;
using Heretic.Roguelike.Things.Players;

namespace Heretic.Roguelike.Battles;

public interface IBattleArena<T>
{
    void Fight(ICreature<T> attacker, ICreature<T> defender);
    void Fight(IList<ICreature<T>> attackerGroup, IList<ICreature<T>> defenderGroup);
    
    Action<string>? MessageHandler { get; set; }
    
    event Action<Monster<T>> OnKillMonster;
    event Action<Player<T>> OnKillPlayer;
}