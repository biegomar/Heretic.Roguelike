namespace Heretic.Roguelike.Amours.Types;

public class SplintMail : IArmourType
{
    public string Name => nameof(SplintMail);
    
    public Armour Create(sbyte amorClass)
    {
        return new Armour
        {
            Type = Name,
            Flag = ArmourFlag.IsKnown, 
            Count = 1, 
            AmorClass = amorClass
        };
    }
}