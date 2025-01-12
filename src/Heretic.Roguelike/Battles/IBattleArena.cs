using System;
using System.Collections.Generic;
using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Creatures;

namespace Heretic.Roguelike.Battles;

public interface IBattleArena<T>
{
    IExperienceCalculator<T> ExperienceCalculator { get; init; }
    
    void Fight(ICreature<T> attacker, ICreature<T> defender);
    void Fight(IList<ICreature<T>> attackerGroup, IList<ICreature<T>> defenderGroup);
    
    Action<string>? MessageHandler { get; set; }
}