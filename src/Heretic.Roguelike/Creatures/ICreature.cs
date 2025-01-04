using System.Collections.Generic;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Creatures;

public interface ICreature<T>
{
    int Experience { get; set; }
    byte ExperienceLevel { get; set; }
    ushort HitPoints { get; set; }
    ushort MaxHitPoints { get; set; }
    ushort Strength { get; set; }
    ushort MaxStrength { get; set; }
    sbyte AmourClass { get; set; }
    IList<DiceThrow> Damage { get; init; }
    T Icon { get; init; }
    Vector ActualPosition { get; }
    void Translate(Vector offset);
    void Translate();
}