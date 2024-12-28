using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.GamePlay;

public record GamePreparationInputStructure<T>(
    Vector LandscapeDimensions, 
    IMotionControllerFactory MotionControllerFactoryForPlayer,
    IMotionControllerFactory MotionControllerFactoryForMonsters,
    IBattleArena<T> BattleArena,
    IProceduralContentGenerator<T> ProceduralContentGenerator,
    IContentPrinter<T> ContentPrinter,
    IInputController InputController);