namespace Heretic.Roguelike.Amours;

public class Armour
{
    public string? Type { get; init; }
    public ArmourFlag Flag { get; set; }
    public byte Count { get; set; }
    public sbyte AmorClass { get; set; } 
}