using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Creatures.Monsters;

public class Monster<T> : ICreature<T>
{
    private readonly IMotionController<T> motionController;
    
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
    public ushort Range { get; set; }
    public IList<DiceThrow> Damage { get; init; } = new List<DiceThrow>();
    public T Icon { get; set; } = default!;
    public Vector ActualPosition => this.motionController.ActualPosition;
    public void Translate(Vector offset)
    {
        this.motionController.Translate(offset);
    }

    public void Translate()
    {
        this.motionController.Translate();
    }

    public Monster(IMotionControllerFactory motionControllerFactory)
    {
        this.motionController = motionControllerFactory.CreateMotionController(this);
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