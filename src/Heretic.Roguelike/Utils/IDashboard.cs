using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Players;

namespace Heretic.Roguelike.Utils;

public interface IDashboard<T>
{
    void DrawDashboard(IEnumerable<ICell<T>> cells, Player<T> creature, int currentFloor, Vector startMazeVector);
}