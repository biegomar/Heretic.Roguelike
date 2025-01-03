using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Amours.Types;

namespace Heretic.Roguelike.Creatures;

public class BaseExperienceCalculator<T> : IExperienceCalculator<T>
{
    protected IDictionary<ushort, string> ExperienceLevelNames = new Dictionary<ushort, string>()
    {
        {1, string.Empty},
        {2, "Guild Novice"},
        {3, "Apprentice"},
        {4, "Journeyman"},
        {5, "Adventurer"},
        {6, "Fighter"},
        {7, "Warrior"},
        {8, "Rogue"},
        {9, "Champion"},
        {10, "Master Rogue"},
        {11, "Warlord"},
        {12, "Hero"},
        {13, "Guild Master"},
        {14, "Dragonlord"},
        {15, "Wizard"},
        {16, "Rogue Geek"},
        {17, "Rogue Addict"},
        {18, "Schmendrick"},
        {19, "Gunfighter"},
        {20, "Time Waster"},
        {21, "Bug Chaser"}
    };
    
    private readonly IDictionary<ushort, int> mapExperienceLevelToExperienceName = InitializeMapExperienceLevelToExperienceName();

    private static IDictionary<ushort, int> InitializeMapExperienceLevelToExperienceName()
    {
        var result = new Dictionary<ushort, int>();
        var value = 10;
        for (ushort i = 0; i < 19; i++)
        {
            result.Add(i, value);
            value *= 2;
        }
        
        return result;
    }
    
    public virtual int GainExperienceFromOpponent(ICreature<T> opponent)
    {
        return opponent.Experience;
    }

    public virtual string GetExperienceLevelName(int experience)
    {
        var index = this.mapExperienceLevelToExperienceName.LastOrDefault(x => x.Value <= experience).Key;
        var result = this.ExperienceLevelNames[index];
        
        return result ?? string.Empty;
    }

    public virtual void RegisterExperienceLevel(ushort minimalExperience, string name)
    {
        this.ExperienceLevelNames.TryAdd(minimalExperience, name);
    }
}