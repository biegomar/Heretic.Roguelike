using Heretic.Roguelike.Things;
using Heretic.Roguelike.Things.Interfaces;

namespace Heretic.Roguelike.SimpleConsoleSample.Creatures;

public class ExperienceCalculator : IExperienceCalculator<char>
{
    public int GainExperienceFromOpponent(ICreature<char> opponent)
    {
        return opponent.Experience;
    }

    public byte GetExperienceLevel(int fromExperience)
    {
        return ExperienceLevels.GetExperienceLevel(fromExperience);
    }
}