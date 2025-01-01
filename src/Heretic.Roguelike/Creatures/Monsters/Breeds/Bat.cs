using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class Bat : IMonsterBreed
{
    public string Name => nameof(Bat);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 1;
        var initialHitPoints = Dice.D8.Roll(expLevel);
        
        DiceThrow diceThrow = new(1, Dice.D2);
        ushort strength = Dice.Roll(diceThrow);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 1,
            Flags = MonsterFlag.Flying,
            TreasurePercentage = 0,
            AmorClass = 3,
            Strength = strength,
            MaxStrength = strength,
            HitPoints = initialHitPoints,
            MaxHitPoints = initialHitPoints,
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = icon
        };
    }
}