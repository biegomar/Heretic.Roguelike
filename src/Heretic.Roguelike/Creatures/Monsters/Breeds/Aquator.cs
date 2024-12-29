using System.Collections.Generic;
using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Dices;

namespace Heretic.Roguelike.Creatures.Monsters.Breeds;

public class Aquator : IMonsterBreed
{
    public string Name => nameof(Aquator);

    public Monster<T> Spawn<T>(IMotionController<T> motionController, T icon)
    {
        byte expLevel = 5;
        DiceThrow diceThrow = new(0, Dice.D0);
        
        return new(motionController)
        {
            Breed = Name,
            ExperienceLevel = expLevel,
            Experience = 20,
            Flags = MonsterFlag.Mean,
            TreasurePercentage = 0,
            AmorClass = 2,
            Strength = 10,
            HitPoints = Dice.D8.Roll(expLevel),
            Damage = new List<DiceThrow>() {diceThrow, diceThrow},
            Icon = icon
        };
    }
}