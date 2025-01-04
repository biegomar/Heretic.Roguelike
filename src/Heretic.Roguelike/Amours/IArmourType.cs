namespace Heretic.Roguelike.Amours;

public interface IArmourType
{
    string Name { get; }
    
    Armour Create(sbyte amorClass);
}