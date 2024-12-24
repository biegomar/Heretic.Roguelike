using Heretic.Roguelike.Subsystems.Creatures.Monsters;

namespace Heretic.Roguelike.Subsystems.Creatures;

public interface IExperienceCalculator<T>
{
    int GainExperience(ICreature<T> monster);   
}