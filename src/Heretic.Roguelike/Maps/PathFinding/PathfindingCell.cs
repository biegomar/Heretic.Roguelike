using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.Maps.PathFinding
{
    public class PathfindingCell<T>(Cell<T> cell)
    {
        private readonly Cell<T> innerCell = cell;
        
        public int PathCount { get; set; }

        public bool IsVisited { get; set; }

        public PathfindingCell<T>? Predecessor { get; set; }
        
        public int X => innerCell.X;
        public int Y => innerCell.Y;
        public int Z => innerCell.Z;
        public T Item
        {
            get => innerCell.Item;
            set => innerCell.Item = value;
        }

        public bool IsVisible
        {
            get => innerCell.IsVisible;
            set => innerCell.IsVisible = value;
        }

        public IList<PathfindingCell<T>> LinkedCells
        {
            get
            {
                var linked = new List<PathfindingCell<T>>();
                foreach (var cell in innerCell.LinkedCells)
                {
                    linked.Add(new PathfindingCell<T>(cell));
                }
                return linked;
            }
        }
        
        public PathfindingCell<T>? GetNeighbour(Directions direction)
        {
            var neighbour = innerCell.Neighbours.TryGetValue(direction, out var cell) ? cell : null;
            return neighbour != null ? new PathfindingCell<T>(neighbour) : null;
        }

        public void LinkCell(PathfindingCell<T> cellToLink)
        {
            innerCell.LinkCell(cellToLink.innerCell);
        }
    }
}