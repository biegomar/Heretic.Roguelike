namespace Heretic.Roguelike.Things.Interfaces;

public interface IExperienceCalculator<T>
{
    int GainExperienceFromOpponent(ICreature<T> opponent);
    byte GetExperienceLevel(int fromExperience);
}