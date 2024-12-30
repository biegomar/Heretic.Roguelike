using System.Collections.Generic;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Creatures;

public interface ICreature<T>
{
    ushort Experience { get; set; }
    byte ExperienceLevel { get; set; }
    ushort HitPoints { get; set; }
    ushort MaxHitPoints { get; set; }
    ushort Strength { get; set; }
    sbyte AmorClass { get; set; }
    IList<DiceThrow> Damage { get; init; }
    T Icon { get;  }
    Vector ActualPosition { get; }
    void Translate(Vector offset);
    void Translate();
}