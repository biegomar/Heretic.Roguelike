using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Things.Interfaces;

public interface IThing<T>
{
    IMotionController<T> MotionController { get; set; }
    T Icon { get; init; }
    bool IsVisible { get; set; }
    bool IsHidden { get; set; }
    Vector ActualPosition { get; }
    void Translate(Vector offset);
    
    void Translate();
}