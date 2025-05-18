using System.Collections.Generic;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.PickHandling;

namespace Heretic.Roguelike.Things.Interfaces;

public interface ICreature<T> : IThing<T>
{
    PickController<T>? PickController { get; init; }
    int Experience { get; set; }
    byte ExperienceLevel { get; set; }
    ushort HitPoints { get; set; }
    ushort MaxHitPoints { get; set; }
    ushort Strength { get; set; }
    ushort MaxStrength { get; set; }
    sbyte AmourClass { get; set; }
    IList<DiceThrow> Damage { get; init; }
    bool Pick(IThing<T> thing);
}