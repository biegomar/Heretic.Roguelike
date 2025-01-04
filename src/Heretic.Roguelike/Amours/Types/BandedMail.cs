namespace Heretic.Roguelike.Amours.Types;

public class BandedMail : IArmourType
{
    public string Name => nameof(BandedMail);
    
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