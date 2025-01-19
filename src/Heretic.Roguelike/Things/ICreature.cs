using System.Collections.Generic;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Things;

public interface ICreature<T> : IThing<T>
{
    int Experience { get; set; }
    byte ExperienceLevel { get; set; }
    ushort HitPoints { get; set; }
    ushort MaxHitPoints { get; set; }
    ushort Strength { get; set; }
    ushort MaxStrength { get; set; }
    sbyte AmourClass { get; set; }
    IList<DiceThrow> Damage { get; init; }
}