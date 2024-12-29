using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Monsters;

namespace Heretic.Roguelike.ArtificialIntelligence.Movements;

public interface IMotionControllerFactory
{
    IMotionController<T> CreateMotionController<T>(IMonsterBreed monsterBreed);
    IMotionController<T> CreateMotionController<T>(T icon);
}