using System;
using System.Collections.Generic;
using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Amours.Types;

namespace Heretic.Roguelike.Creatures;

public interface IExperienceCalculator<T>
{
    int GainExperienceFromOpponent(ICreature<T> opponent);
    byte GetExperienceLevel(int fromExperience);
}