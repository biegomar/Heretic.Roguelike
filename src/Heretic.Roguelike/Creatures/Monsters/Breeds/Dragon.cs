using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class Dragon : IMonsterBreed
{
    public string Name => nameof(Dragon);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 10;
        var initialHitPoints = Dice.D8.Roll(expLevel);
        
        DiceThrow diceThrowD8 = new(1, Dice.D8);
        DiceThrow diceThrowD10 = new(3, Dice.D10);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 6800,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 100,
            AmorClass = -1,
            Strength = Dice.Roll(diceThrowD8),
            HitPoints = initialHitPoints,
            MaxHitPoints = initialHitPoints,
            Damage = new List<DiceThrow>() {diceThrowD8, diceThrowD8, diceThrowD10},
            Icon = icon
        };
    }
}