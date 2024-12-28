using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Maps;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.ArtificialIntelligence.Movements;

public interface IMotionController<T>
{
    ICreature<T> Creature { get; set; }
    Vector ActualPosition { get; set; }
    void Translate(Vector offset);
    void Translate();
}