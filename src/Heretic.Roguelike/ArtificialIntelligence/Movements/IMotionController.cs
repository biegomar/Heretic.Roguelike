using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.ArtificialIntelligence.Movements;

public interface IMotionController<T>
{
    Vector ActualPosition { get; set; }
    void Translate(Vector offset);
    void Translate();
}