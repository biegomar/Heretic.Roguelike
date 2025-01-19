using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Things.Monsters;

public class Monster<T> : ICreature<T>
{
    public Monster(IMotionController<T> motionController)
    {
        this.MotionController = motionController;
        this.MotionController.Entity = this;
    }

    public string? Breed { get; init; }
    public byte TreasurePercentage { get; init; }
    public MonsterFlag Flags { get; init; }
    public IMotionController<T> MotionController { get; set; }
    public int Experience { get; set; }
    public byte ExperienceLevel { get; set; }
    public ushort HitPoints { get; set; }
    public ushort MaxHitPoints { get; set; }
    public ushort Strength { get; set; }
    public ushort MaxStrength { get; set; }
    public sbyte AmourClass { get; set; }
    public ushort Range { get; set; }
    public IList<DiceThrow> Damage { get; init; } = new List<DiceThrow>();
    public T Icon { get; init; } = default!;
    public Vector ActualPosition => MotionController.ActualPosition;
    public void Translate(Vector offset)
    {
        MotionController.Translate(offset);
    }

    public void Translate()
    {
        MotionController.Translate();
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