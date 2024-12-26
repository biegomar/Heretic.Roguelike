using System.Collections.Generic;
using Heretic.Roguelike.Amors;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.Weapons;

namespace Heretic.Roguelike.Creatures.Players;

public class Player<T> : ICreature<T>
{
    public string Name { get; init; } = null!;
    public uint Gold { get; set; }
    public ushort Experience { get; set; }
    public byte ExperienceLevel { get; set; }
    public ushort HitPoints { get; set; }
    public ushort MaxHitPoints { get; set; }
    public ushort Strength { get; set; }
    public sbyte AmorClass { get; set; }
    public byte Food { get; set; }
    public Weapon? ActiveWeapon { get; set; }
    public IList<Weapon> Weapons { get; set; } = new List<Weapon>();
    public Armor? ActiveArmor { get; set; } 
    public IList<Armor> Armors { get; set; } = new List<Armor>();
    public IList<DiceThrow> Damage { get; init; } = new List<DiceThrow>();
    public T Icon { get; set; } = default!;

    private readonly IExperienceCalculator<T> experienceCalculator;

    public Player(IExperienceCalculator<T> experienceCalculator)
    {
        this.experienceCalculator = experienceCalculator;
    }
    
    public override string ToString()
    {
        if (this.Icon != null)
        {
            return this.Icon.ToString();
        }
        return base.ToString();
    }
}