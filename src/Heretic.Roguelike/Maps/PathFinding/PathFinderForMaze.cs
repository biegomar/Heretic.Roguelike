using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.PathFinding;

public class PathFinderForMaze<T, TK> : IPathFinder where TK : class, ICell<T>, new()
{
    private readonly Landscape<T, TK> _landscape;
    private readonly IList<TK> Cells;
        
    public PathFinderForMaze(Landscape<T, TK> landscape)
    {
        this._landscape = landscape;
        this.Cells = landscape.Cells;
    }
    
    public IList<Vector> GetShortestPath(Vector startPoint, Vector endPoint)
    {
        ResetMazeCells();
            
        var startCell = GetCellByColumnAndRow((int)startPoint.X, (int)startPoint.Y);
        var endCell = GetCellByColumnAndRow((int)endPoint.X, (int)endPoint.Y);
        var queue = new Queue<TK>();
        queue.Enqueue(startCell);
        startCell.IsVisited = true;
        startCell.PathCount = 0;

        while (queue.Count > 0)
        {
            var currentCell = queue.Dequeue();

            if (currentCell == endCell)
            {
                return ReconstructPath(startCell, endCell);
            }

            foreach (var linkedCell in GetUnvisitedLinkedCells(currentCell))
            {
                linkedCell.IsVisited = true;
                linkedCell.Predecessor = currentCell;
                queue.Enqueue(linkedCell);
            }
        }

        return new List<Vector>();
    }
    
    private List<Vector> ReconstructPath(TK start, TK end)
    {
        var path = new List<Vector>();
        var currentCell = end;

        while (currentCell != start)
        {
            path.Add(new Vector(currentCell!.X, currentCell.Y, currentCell.PathCount));
            currentCell = currentCell.Predecessor! as TK;
        }
            
        path.Add(new Vector(start.X, start.Y, 0));

        path.Reverse();
        return path;
    }
    
    private IEnumerable<TK> GetUnvisitedLinkedCells(TK cell)
    {
        var linkedCells = new List<TK>();
        var count = cell.PathCount + 1;

        foreach (var linkedCell in cell.LinkedCells)
        {
            if (linkedCell is TK {IsVisited: false} unvisitedLinkedCell)
            {
                unvisitedLinkedCell.PathCount = count;
                linkedCells.Add(unvisitedLinkedCell);
            }
        }

        return linkedCells;
    }

    private void ResetMazeCells()
    {
        foreach (var cell in Cells)
        {
            cell.IsVisited = false;
            cell.PathCount = 0;
            cell.Predecessor = null;
        }
    }
        
    protected TK GetCellByColumnAndRow(int column, int row)
    {
        return this.Cells.Single(cell => cell.X == column && cell.Y == row);
    }
}