using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Things;

public interface IThing<T>
{
    IMotionController<T> MotionController { get; set; }
    T Icon { get; init; }
    Vector ActualPosition { get; }
    void Translate(Vector offset);
    void Translate();
}