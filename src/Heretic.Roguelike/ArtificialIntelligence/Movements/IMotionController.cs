using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.ArtificialIntelligence.Movements;

public interface IMotionController<T>
{
    T Icon { get; set; }
    Vector ActualPosition { get; set; }
    void Translate(Vector offset);
    void Translate();
}