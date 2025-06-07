using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Players;

namespace Heretic.Roguelike.Utils;

public interface IDashboard<T, TK> where TK : ICell<T>
{
    void DrawDashboard(IList<TK> cells, Player<T> creature, int currentFloor, Vector startMazeVector);
}