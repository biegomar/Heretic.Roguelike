using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.SimpleConsoleSample.ArtificialIntelligence.Movements;

/// <summary>
/// Simple player movement. 
/// </summary>
/// <param name="startingPosition">The starting position of the player.</param>
/// <param name="icon">The icon that is used to display the player on the map.</param>
public class PlayerMovement(Vector startingPosition, char icon) : IMotionController<char>
{
    public char Icon { get; set; } = icon;
    public Vector ActualPosition { get; set; } = startingPosition;

    public void Translate(Vector offset)
    {
        this.ActualPosition += offset;
    }

    public void Translate()
    {
        this.Translate(Vector.Zero);
    }
}