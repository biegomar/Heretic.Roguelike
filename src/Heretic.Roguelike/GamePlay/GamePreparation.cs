using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Maps.ContentGeneration;

namespace Heretic.Roguelike.GamePlay;

public record GamePreparation<T>(
    Player<T> Player,
    Landscape<T> Landscape,
    IBattleArena<T> BattleArena,
    IInputController InputController);