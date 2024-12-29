namespace Heretic.Roguelike.Amours.Types;

public class PlateMail : IArmourType
{
    public string Name => nameof(PlateMail);
    
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