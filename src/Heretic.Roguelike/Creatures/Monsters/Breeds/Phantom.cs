using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class Phantom : IMonsterBreed
{
    public string Name => nameof(Phantom);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 8;
        DiceThrow diceThrow = new(4, Dice.D4);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 120,
            Flags = MonsterFlag.Invisible,
            TreasurePercentage = 0,
            AmorClass = 3,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow},
            Icon = icon
        };
    }
}