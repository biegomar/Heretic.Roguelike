namespace Heretic.Roguelike.Amours.Types;

public class RingMail : IArmourType
{
    public string Name => nameof(RingMail);
    
    public Armour Create()
    {
        return new Armour
        {
            Type = Name,
            Flag = ArmourFlag.IsKnown, 
            Count = 1, 
            AmorClass = 3 
        };
    }
}