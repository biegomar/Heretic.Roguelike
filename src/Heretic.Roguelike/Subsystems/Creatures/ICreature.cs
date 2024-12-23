using System.Collections.Generic;
using Heretic.Roguelike.Subsystems.Dices;

namespace Heretic.Roguelike.Subsystems.Creatures;

public interface ICreature<T>
{
    public ushort Experience { get; set; }
    public byte ExperienceLevel { get; set; }
    public ushort HitPoints { get; set; }
    public ushort MaxHitPoints { get; set; }
    public ushort Strength { get; set; }
    public sbyte AmorClass { get; set; }
    public IList<DiceThrow> Damage { get; init; }

    public T Icon { get; set; }
}