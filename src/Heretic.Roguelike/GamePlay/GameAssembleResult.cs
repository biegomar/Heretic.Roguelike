using System.Collections.Generic;
using Heretic.Roguelike.Battles;
using Heretic.Roguelike.Daemons;
using Heretic.Roguelike.Maps;
using Heretic.Roguelike.Things.Interfaces;
using Heretic.Roguelike.Things.Monsters;
using Heretic.Roguelike.Things.Players;
using Heretic.Roguelike.Utils;

namespace Heretic.Roguelike.GamePlay;

public record GameAssembleResult<T>(
    Player<T> Player,
    ILandscape<T> Landscape,
    DaemonHandler DaemonHandler,
    IBattleArena<T> BattleArena,
    IInputController<T> InputController,
    IOutputHandler OutputHandler,
    IExperienceCalculator<T> ExperienceCalculator,
    IContentPrinter<T> ContentPrinter,
    IDashboard<T> Dashboard,
    IMessagePrinter MessagePrinter,
    IEnumerable<Monster<T>> Monsters);