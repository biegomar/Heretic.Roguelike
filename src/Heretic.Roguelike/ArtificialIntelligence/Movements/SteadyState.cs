using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Interfaces;

namespace Heretic.Roguelike.ArtificialIntelligence.Movements;

/// <summary>
/// For things that don't really move.
/// </summary>
/// <param name="startingPosition"></param>
/// <typeparam name="T"></typeparam>
public class SteadyState<T>(Vector startingPosition) : IMotionController<T>
{
    public IThing<T> Entity { get; set; }
    public Vector ActualPosition { get; set; } = startingPosition;
    public void Translate(Vector offset)
    {
    }

    public void Translate()
    {
    }
}