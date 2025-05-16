using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Interfaces;

namespace Heretic.Roguelike.Things.Common;

public class Exit<T> : IThing<T>
{
    public IMotionController<T> MotionController { get; set; }
    public T Icon { get; init; } = default!;
    public Vector ActualPosition => MotionController.ActualPosition;
    
    public Exit(IMotionController<T> motionController)
    {
        this.MotionController = motionController;
        this.MotionController.Entity = this;
    }
    
    public void Translate(Vector offset)
    {
        MotionController.Translate(offset);
    }

    public void Translate()
    {
        MotionController.Translate();
    }

    public override string ToString()
    {
        if (this.Icon != null)
        {
            return this.Icon.ToString();
        }
        return base.ToString();
    }
}