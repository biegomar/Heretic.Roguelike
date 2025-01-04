namespace Heretic.Roguelike.Amours.Types;

public class RingMail : IArmourType
{
    public string Name => nameof(RingMail);
    
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