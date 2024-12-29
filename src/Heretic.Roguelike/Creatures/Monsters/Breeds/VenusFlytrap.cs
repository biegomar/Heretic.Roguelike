using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class VenusFlytrap : IMonsterBreed
{
    public string Name => nameof(VenusFlytrap);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        //special behaviour needed.
        
        byte expLevel = 8;
        DiceThrow diceThrow = new(1, Dice.D0);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 80,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = 3,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = icon
        };
    }
}