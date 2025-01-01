using System.Collections.Generic;
using Heretic.Roguelike.Amours;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Weapons;

namespace Heretic.Roguelike.Creatures.Players;

public class Player<T>(IMotionController<T> motionController, IExperienceCalculator<T> experienceCalculator)
    : ICreature<T>
{
    public string Name { get; init; } = null!;
    public uint Gold { get; set; }
    public ushort Experience { get; set; }
    public byte ExperienceLevel { get; set; }
    public ushort HitPoints { get; set; }
    public ushort MaxHitPoints { get; set; }
    public ushort Strength { get; set; }
    public ushort MaxStrength { get; set; }
    public sbyte AmorClass { get; set; }
    public byte Food { get; set; }
    public Weapon? ActiveWeapon { get; set; }
    public IList<Weapon> Weapons { get; set; } = new List<Weapon>();
    public Armour? ActiveArmor { get; set; } 
    public IList<Armour> Armors { get; set; } = new List<Armour>();
    public IList<DiceThrow> Damage { get; init; } = new List<DiceThrow>();
    public T Icon { get; init; }

    public Vector ActualPosition => motionController.ActualPosition;
    
    public void Translate(Vector offset)
    {
        motionController.Translate(offset);
    }

    public void Translate()
    {
        motionController.Translate();
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