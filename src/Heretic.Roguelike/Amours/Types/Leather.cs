namespace Heretic.Roguelike.Amours.Types;

public class Leather : IArmourType
{
    public string Name => nameof(Leather);
    
    public Armour Create()
    {
        return new Armour
        {
            Type = Name,
            Flag = ArmourFlag.IsKnown, 
            Count = 1, 
            AmorClass = 2 
        };
    }
}