using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class Urvile : IMonsterBreed
{
    public string Name => nameof(Urvile);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 7;
        var initialHitPoints = Dice.D8.Roll(expLevel);
        
        DiceThrow diceThrowD3 = new(1, Dice.D3);
        DiceThrow diceThrowD6 = new(4, Dice.D6);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 190,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = -2,
            Strength = 10,
            HitPoints = initialHitPoints,
            MaxHitPoints = initialHitPoints,
            Damage = new List<DiceThrow>() {diceThrowD3, diceThrowD3, diceThrowD3, diceThrowD6},
            Icon = icon
        };
    }
}