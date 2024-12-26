using System.Collections.Generic;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.PathFinding;

public interface IPathFinder
{
    public IList<Vector> GetShortestPath(Vector startPoint, Vector endPoint);
}