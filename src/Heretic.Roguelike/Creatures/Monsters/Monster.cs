using System.Collections.Generic;
using Heretic.Roguelike.Subsystems.Dices;

namespace Heretic.Roguelike.Subsystems.Creatures.Monsters;

public class Monster<T> : ICreature<T>
{
    public MonsterBreed Breed { get; init; }
    public string Name => Breed.ToString();
    public byte TreasurePercentage { get; init; }
    public MonsterFlag Flags { get; init; }
    public ushort Experience { get; set; }
    public byte ExperienceLevel { get; set; }
    public ushort HitPoints { get; set; }
    public ushort MaxHitPoints { get; set; }
    public ushort Strength { get; set; }
    public sbyte AmorClass { get; set; }
    public IList<DiceThrow> Damage { get; init; } = new List<DiceThrow>();
    public T Icon { get; set; } = default!;

    public ushort Range { get; set; }

    public override string ToString()
    {
        if (this.Icon != null)
        {
            return this.Icon.ToString();
        }
        return base.ToString();
    }
}