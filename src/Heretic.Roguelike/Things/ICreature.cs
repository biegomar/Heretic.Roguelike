using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Things;

public interface ICreature<T>
{
    IMotionController<T> MotionController { get; set; }
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