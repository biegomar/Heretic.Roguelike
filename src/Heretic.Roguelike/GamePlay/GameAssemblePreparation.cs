using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.GamePlay;

public record GameAssemblePreparation<T, TK>(GameLoop<T, TK> GameLoop) where TK : class, ICell<T>;
