namespace Heretic.Roguelike.Amours.Types;

public class ChainMail : IArmourType
{
    public string Name => nameof(ChainMail);
    
    public Armour Create()
    {
        return new Armour
        {
            Type = Name,
            Flag = ArmourFlag.IsKnown, 
            Count = 1, 
            AmorClass = 5 
        };
    }
}