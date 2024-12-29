using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class Snake : IMonsterBreed
{
    public string Name => nameof(Snake);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 2;
        DiceThrow diceThrow = new(1, Dice.D3);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 1,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = 8,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = icon
        };
    }
}