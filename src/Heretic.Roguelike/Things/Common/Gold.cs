using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Interfaces;

namespace Heretic.Roguelike.Things.Common;

public class Gold<T> : IThing<T>, IValuable
{
    public IMotionController<T> MotionController { get; set; }
    public T Icon { get; init; } = default!;
    public Vector ActualPosition => MotionController.ActualPosition;
    public int ActualValue { get; set; }
    
    public Gold(IMotionController<T> motionController, int startingValue = 0)
    {
        this.MotionController = motionController;
        this.MotionController.Entity = this;
        this.ActualValue = startingValue;
    }
    
    public void Translate(Vector offset)
    {
        throw new System.NotImplementedException();
    }

    public void Translate()
    {
        throw new System.NotImplementedException();
    }
}