using Heretic.Roguelike.Subsystems.Creatures.Monsters;

namespace Heretic.Roguelike.Subsystems.Creatures;

public interface IExperienceCalculator<T>
{
    ushort GainExperience(Monster<T> monster);   
}