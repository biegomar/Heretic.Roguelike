using System.Collections.Generic;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Daemons;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Things.Interfaces;
using Heretic.Roguelike.Things.Monsters;
using Heretic.Roguelike.Things.Players;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.GamePlay;

public record GameAssembleResult<T, TK>(
    Player<T> Player,
    Landscape<T, TK> Landscape,
    DaemonHandler DaemonHandler,
    IBattleArena<T> BattleArena,
    IInputController<T> InputController,
    IOutputHandler OutputHandler,
    IExperienceCalculator<T> ExperienceCalculator,
    IContentPrinter<T, TK> ContentPrinter,
    IDashboard<T, TK> Dashboard,
    IMessagePrinter MessagePrinter,
    IEnumerable<Monster<T>> Monsters) where TK : ICell<T>;