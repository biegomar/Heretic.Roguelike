namespace Heretic.Roguelike.Amours.Types;

public class StuddedLeather : IArmourType
{
    public string Name => nameof(StuddedLeather);
    
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