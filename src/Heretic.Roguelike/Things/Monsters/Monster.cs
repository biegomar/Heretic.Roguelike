using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.PickHandling;
using Heretic.Roguelike.Things.Interfaces;

namespace Heretic.Roguelike.Things.Monsters;

public class Monster<T> : ICreature<T>
{
    public Monster(IMotionController<T> motionController, PickController<T>? pickController = null)
    {
        this.MotionController = motionController;
        this.MotionController.Entity = this;
        this.PickController = pickController;
    }

    public string? Breed { get; init; }
    public byte TreasurePercentage { get; init; }
    public MonsterFlag Flags { get; init; }
    public IMotionController<T> MotionController { get; set; }
    public PickController<T>? PickController { get; init; }
    public int Experience { get; set; }
    public byte ExperienceLevel { get; set; }
    public ushort HitPoints { get; set; }
    public ushort MaxHitPoints { get; set; }
    public ushort Strength { get; set; }
    public ushort MaxStrength { get; set; }
    public sbyte AmourClass { get; set; }
    public ushort Range { get; set; }
    public IList<DiceThrow> Damage { get; init; } = new List<DiceThrow>();
    public bool Pick(IThing<T> thing)
    {
        return this.PickController != null && this.PickController.ProcessPick(this, thing);
    }

    public T Icon { get; init; } = default!;
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