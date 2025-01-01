namespace Heretic.Roguelike.Amours.Types;

public class ScaleMail : IArmourType
{
    public string Name => nameof(ScaleMail);
    
    public Armour Create()
    {
        return new Armour
        {
            Type = Name,
            Flag = ArmourFlag.IsKnown, 
            Count = 1, 
            AmorClass = 4 
        };
    }
}