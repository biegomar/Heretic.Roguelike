using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class Medusa : IMonsterBreed
{
    public string Name => nameof(Medusa);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 8;
        var initialHitPoints = Dice.D8.Roll(expLevel);
        
        DiceThrow diceThrowD4 = new(3, Dice.D4);
        DiceThrow diceThrowD5 = new(2, Dice.D5);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 200,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 40,
            AmorClass = 2,
            Strength = 10,
            HitPoints = initialHitPoints,
            MaxHitPoints = initialHitPoints,
            Damage = new List<DiceThrow>() {diceThrowD4, diceThrowD4, diceThrowD5},
            Icon = icon
        };
    }
}