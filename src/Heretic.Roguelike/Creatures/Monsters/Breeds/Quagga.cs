using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class Quagga : IMonsterBreed
{
    public string Name => nameof(Quagga);
    
    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 3;
        DiceThrow diceThrowD2 = new(1, Dice.D2);
        DiceThrow diceThrowD4 = new(1, Dice.D4);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 32,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 30,
            AmorClass = 2,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrowD2, diceThrowD2, diceThrowD4},
            Icon = icon
        };
    }
}