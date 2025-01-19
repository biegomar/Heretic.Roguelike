using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Monsters;

namespace Heretic.Roguelike.ArtificialIntelligence.Movements;

public interface IMotionControllerFactory<T>
{
    IMotionController<T> CreateMonsterMotionController(IMonsterBreed monsterBreed, Vector startingPosition);
}