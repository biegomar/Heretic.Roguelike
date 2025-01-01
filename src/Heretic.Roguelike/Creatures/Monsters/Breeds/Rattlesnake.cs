using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class Rattlesnake : IMonsterBreed
{
    public string Name => nameof(Rattlesnake);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 2;
        var initialHitPoints = Dice.D8.Roll(expLevel);
        
        DiceThrow diceThrow = new(1, Dice.D6);
        ushort strength = Dice.Roll(diceThrow);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 9,
            Flags = MonsterFlag.Mean,
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