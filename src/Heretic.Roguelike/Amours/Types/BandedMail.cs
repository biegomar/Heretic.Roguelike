namespace Heretic.Roguelike.Amours.Types;

public class BandedMail : IArmourType
{
    public string Name => nameof(BandedMail);
    
    public Armour Create()
    {
        return new Armour
        {
            Type = Name,
            Flag = ArmourFlag.IsKnown, 
            Count = 1, 
            AmorClass = 6 
        };
    }
}