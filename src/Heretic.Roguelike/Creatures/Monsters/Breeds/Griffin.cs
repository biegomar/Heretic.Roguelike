using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class Griffin : IMonsterBreed
{
    public string Name => nameof(Griffin);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 13;
        var initialHitPoints = Dice.D8.Roll(expLevel);
        
        DiceThrow diceThrowD3 = new(4, Dice.D3);
        DiceThrow diceThrowD5 = new(3, Dice.D5);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 2000,
            Flags = MonsterFlag.Mean | MonsterFlag.Flying | MonsterFlag.Regeneration,
            TreasurePercentage = 20,
            AmorClass = 2,
            Strength = 10,
            HitPoints = initialHitPoints,
            MaxHitPoints = initialHitPoints,
            Damage = new List<DiceThrow>() {diceThrowD3, diceThrowD5, diceThrowD3},
            Icon = icon
        };
    }
}