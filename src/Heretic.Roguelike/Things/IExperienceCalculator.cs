namespace Heretic.Roguelike.Things;

public interface IExperienceCalculator<T>
{
    int GainExperienceFromOpponent(ICreature<T> opponent);
    byte GetExperienceLevel(int fromExperience);
}