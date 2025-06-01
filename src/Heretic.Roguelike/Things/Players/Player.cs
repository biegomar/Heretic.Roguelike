using System.Collections.Generic;
using Heretic.Roguelike.Amours;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.PickHandling;
using Heretic.Roguelike.Things.Interfaces;
using Heretic.Roguelike.Weapons;

namespace Heretic.Roguelike.Things.Players;

public class Player<T> : ICreature<T>
{
    public Player(IMotionController<T> motionController, PickController<T> pickController)
    {
        this.MotionController = motionController;
        this.MotionController.Entity = this;
        this.PickController = pickController;
    }

    public string Name { get; set; }
    public uint Gold { get; set; }
    public IMotionController<T> MotionController { get; set; }
    public PickController<T>? PickController { get; init; }
    public int Experience { get; set; }
    public byte ExperienceLevel { get; set; }
    public ushort HitPoints { get; set; }
    public ushort MaxHitPoints { get; set; }
    public ushort Strength { get; set; }
    public ushort MaxStrength { get; set; }
    public sbyte AmourClass { get; set; }
    public uint Food { get; set; }
    public Weapon? ActiveWeapon { get; set; }
    public IList<Weapon> Weapons { get; set; } = new List<Weapon>();
    public Armour? ActiveArmour { get; set; } 
    public IList<Armour> Armours { get; set; } = new List<Armour>();
    public IList<DiceThrow> Damage { get; init; } = new List<DiceThrow>();
    public bool Pick(IThing<T> thing)
    {
        return this.PickController != null && this.PickController.ProcessPick(this, thing);
    }

    public T Icon { get; init; }
    public bool IsVisible { get; set; }

    public Vector ActualPosition => MotionController.ActualPosition;
    
    public void Translate(Vector offset)
    {
        this.MotionController.Translate(offset);
    }

    public void Translate()
    {
        this.MotionController.Translate();
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