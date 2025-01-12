namespace Heretic.Roguelike.Amours;

public class PassThruArmourCalculator : IArmourCalculator
{
    public int CalculateArmourFromArmourClass(int armourClass)
    {
        return armourClass;
    }
}