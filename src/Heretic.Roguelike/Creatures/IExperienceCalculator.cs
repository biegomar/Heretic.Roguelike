namespace Heretic.Roguelike.Creatures;

public interface IExperienceCalculator<T>
{
    int GainExperience(ICreature<T> monster);   
}