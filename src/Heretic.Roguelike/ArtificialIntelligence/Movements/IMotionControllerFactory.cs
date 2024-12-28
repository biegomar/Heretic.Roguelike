using Heretic.Roguelike.Creatures;

namespace Heretic.Roguelike.ArtificialIntelligence.Movements;

public interface IMotionControllerFactory
{
    IMotionController<T> CreateMotionController<T>(ICreature<T> creature);
}