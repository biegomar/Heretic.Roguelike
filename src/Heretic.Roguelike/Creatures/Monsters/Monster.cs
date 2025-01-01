using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Creatures.Monsters;

public class Monster<T>(IMotionController<T> motionController) : ICreature<T>
{
    public string? Breed { get; init; }
    public byte TreasurePercentage { get; init; }
    public MonsterFlag Flags { get; init; }
    public ushort Experience { get; set; }
    public byte ExperienceLevel { get; set; }
    public ushort HitPoints { get; set; }
    public ushort MaxHitPoints { get; set; }
    public ushort Strength { get; set; }
    public sbyte AmorClass { get; set; }
    public ushort Range { get; set; }
    public IList<DiceThrow> Damage { get; init; } = new List<DiceThrow>();
    public T Icon { get; init; } = default!;
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