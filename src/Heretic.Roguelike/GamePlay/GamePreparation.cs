using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Creatures.Players;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;

namespace Heretic.Roguelike.GamePlay;

public record GamePreparation<T, TK>(
    Player<T> Player,
    Landscape<T, TK> Landscape,
    IBattleArena<T> BattleArena,
    IInputController InputController) where TK : ICell<T>;