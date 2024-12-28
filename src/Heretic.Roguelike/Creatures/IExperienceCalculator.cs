namespace Heretic.Roguelike.Creatures;

public interface IExperienceCalculator<T>
{
    int GainExperienceFromOpponent(ICreature<T> opponent);   
}