using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class Troll : IMonsterBreed
{
    public string Name => nameof(Troll);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 6;
        var initialHitPoints = Dice.D8.Roll(expLevel);
        
        DiceThrow diceThrowD6 = new(2, Dice.D6);
        DiceThrow diceThrowD8 = new(1, Dice.D8);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 120,
            Flags = MonsterFlag.Mean | MonsterFlag.Regeneration,
            TreasurePercentage = 50,
            AmorClass = 4,
            Strength = Dice.Roll(diceThrowD8),
            HitPoints = initialHitPoints,
            MaxHitPoints = initialHitPoints,
            Damage = new List<DiceThrow>() {diceThrowD8, diceThrowD8, diceThrowD6},
            Icon = icon
        };
    }
}