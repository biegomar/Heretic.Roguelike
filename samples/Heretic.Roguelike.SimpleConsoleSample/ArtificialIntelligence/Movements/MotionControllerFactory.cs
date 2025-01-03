using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.SimpleConsoleSample.ArtificialIntelligence.Movements;

public class MotionControllerFactory(Landscape<char, Cell<char>> landscape, IBattleArena<char> battleArena) : IMotionControllerFactory<char>
{
    public IMotionController<char> CreateMonsterMotionController(IMonsterBreed monsterBreed, Vector startingPosition)
    {
        return new MonsterMovement(landscape, battleArena, startingPosition);
    }
}