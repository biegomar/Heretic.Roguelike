using Heretic.Roguelike.Creatures;

namespace Heretic.Roguelike.SimpleConsoleSample.Creatures.Players;

public class ExperienceCalculator : IExperienceCalculator<char>
{
    public int GainExperienceFromOpponent(ICreature<char> opponent)
    {
        return opponent.Experience;
    }
}