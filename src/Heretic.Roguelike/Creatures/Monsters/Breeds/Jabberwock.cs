using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class Jabberwock : IMonsterBreed
{
    public string Name => nameof(Jabberwock);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 15;
        var initialHitPoints = Dice.D8.Roll(expLevel);
        
        DiceThrow diceThrowD4 = new(2, Dice.D4);
        DiceThrow diceThrowD12 = new(2, Dice.D12);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 4000,
            TreasurePercentage = 70,
            AmorClass = 6,
            Strength = Dice.Roll(diceThrowD12),
            HitPoints = initialHitPoints,
            MaxHitPoints = initialHitPoints,
            Damage = new List<DiceThrow>() {diceThrowD12, diceThrowD4},
            Icon = icon
        };
    }
}