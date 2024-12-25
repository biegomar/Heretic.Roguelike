﻿using System.Collections.Generic;

namespace Heretic.Roguelike.Maps.PathFinding;

public interface IPathFinder
{
    public IList<Vector> GetShortestPath(Vector startPoint, Vector endPoint);
}