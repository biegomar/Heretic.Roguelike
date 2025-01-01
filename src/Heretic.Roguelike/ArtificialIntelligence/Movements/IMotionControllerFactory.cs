using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.ArtificialIntelligence.Movements;

public interface IMotionControllerFactory<T>
{
    IMotionController<T> CreateMonsterMotionController(IMonsterBreed monsterBreed, Vector startingPosition, T icon);
}