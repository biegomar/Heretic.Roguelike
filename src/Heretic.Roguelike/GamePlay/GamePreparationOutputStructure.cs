using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Maps.ContentGeneration;

namespace Heretic.Roguelike.GamePlay;

public record GamePreparationOutputStructure<T>(
    Player<T> Player,
    Landscape<ICreature<T>> Landscape,
    IBattleArena<T> BattleArena,
    IInputController InputController);