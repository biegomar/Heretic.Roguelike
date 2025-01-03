using System;
using System.Collections.Generic;
using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Amours.Types;

namespace Heretic.Roguelike.Creatures;

public interface IExperienceCalculator<T>
{
    int GainExperienceFromOpponent(ICreature<T> opponent);

    public string GetExperienceLevelName(int experience);

    public void RegisterExperienceLevel(ushort minimalExperience, string name);
}