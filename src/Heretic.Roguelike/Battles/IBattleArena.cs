using System.Collections.Generic;
using Heretic.Roguelike.Creatures;

namespace Heretic.Roguelike.Battles;

public interface IBattleArena<T>
{
    IExperienceCalculator<T> ExperienceCalculator { get; init; }
    public void Fight(ICreature<T> attacker, ICreature<T> defender);
    public void Fight(IList<ICreature<T>> attackerGroup, IList<ICreature<T>> defenderGroup);
}