using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things;

namespace Heretic.Roguelike.ArtificialIntelligence.Movements;

public interface IMotionController<T>
{
    ICreature<T> Entity { get; set; }
    Vector ActualPosition { get; set; }
    void Translate(Vector offset);
    void Translate();
}