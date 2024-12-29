using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class IceMonster : IMonsterBreed
{
    public string Name => nameof(IceMonster);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 1;
        DiceThrow diceThrow = new(1, Dice.D2);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 15,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = 9,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = icon
        };
    }
}