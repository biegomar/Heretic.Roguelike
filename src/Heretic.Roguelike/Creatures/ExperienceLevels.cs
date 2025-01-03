using System.Collections.Generic;
using System.Linq;

namespace Heretic.Roguelike.Creatures;

public static class ExperienceLevels
{
    private static IDictionary<ushort, string> experienceLevelNames = new Dictionary<ushort, string>()
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
    private static IDictionary<byte, int> mapExperienceLevelToExperienceName = InitializeMapExperienceLevelToExperienceName();

    private static IDictionary<byte, int> InitializeMapExperienceLevelToExperienceName()
    {
        var result = new Dictionary<byte, int>();
        var value = 10;
        for (byte i = 0; i < 19; i++)
        {
            result.Add(i, value);
            value *= 2;
        }
        
        return result;
    }
    
    public static string GetExperienceLevelName(ushort experienceLevel)
    {
        
        var result = experienceLevelNames[experienceLevel];
        
        return result ?? string.Empty;
    }

    public static byte GetExperienceLevel(int fromExperience)
    {
        return mapExperienceLevelToExperienceName.LastOrDefault(x => x.Value <= fromExperience).Key;
    }

    public static void RegisterExperienceLevel(ushort minimalExperience, string name)
    {
        experienceLevelNames.TryAdd(minimalExperience, name);
    }
}