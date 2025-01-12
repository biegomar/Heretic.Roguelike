using System;

namespace Heretic.Roguelike.Amours;

public class AdvancedDungeonsDragonsArmourCalculator : IArmourCalculator
{
    public int CalculateArmourFromArmourClass(int armourClass)
    {
        // old AD&D-Amour-Rules.
        return Math.Abs(armourClass - 11);
    }
}