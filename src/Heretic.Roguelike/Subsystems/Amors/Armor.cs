namespace Heretic.Roguelike.Subsystems.Amors;

public class Armor
{
    public ArmorType Type { get; set; }
    public ArmorFlag Flag { get; set; }
    public byte Count { get; set; }
    public sbyte AmorClass { get; set; } 
}