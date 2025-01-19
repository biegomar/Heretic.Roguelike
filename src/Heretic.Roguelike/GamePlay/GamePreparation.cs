using System.Collections.Generic;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Things;
using Heretic.Roguelike.Things.Monsters;
using Heretic.Roguelike.Things.Players;

namespace Heretic.Roguelike.GamePlay;

public record GamePreparation<T, TK>(
    Player<T> Player,
    Landscape<T, TK> Landscape,
    IBattleArena<T> BattleArena,
    IInputController<T> InputController,
    IExperienceCalculator<T> ExperienceCalculator,
    IEnumerable<Monster<T>> Monsters) where TK : ICell<T>;