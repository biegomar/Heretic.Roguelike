using System;
using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.ContentGeneration.Mazes;

public class AldousBroderMazeGenerator<T, TK>: BaseMazeGenerator<T, TK> where TK : class, IOrthogonalCell<T>, new()
{
    private readonly Random randomGenerator = new();
    
    public override IList<TK> Generate(IList<TK> cells)
    {
        var dimensionZeroLength = cells.Max(cell => cell.X) + 1;
        var dimensionOneLength = cells.Max(cell => cell.Y) + 1;

        var startPositionX = randomGenerator.Next(0, dimensionZeroLength);
        var startPositionY = randomGenerator.Next(0, dimensionOneLength);

        var actualCell = GetCellByColumnAndRow(cells, startPositionX, startPositionY);

        var countOfCells = cells.Count - 1;
        
        do
        {
            var nextCell = this.GetNextCellCandidate(actualCell);
                
            if (!actualCell.LinkedCells.Contains(nextCell))
            {
                if (!nextCell.LinkedCells.Any())
                {
                    countOfCells--;
                    actualCell.LinkCell(nextCell);
                }
            }
            
            actualCell = nextCell;
        } while (countOfCells > 0);

        return cells;
    }
    
    public override IList<TK> LinkCells(IList<TK> cells)
    {
        var width = cells.Max(cell => cell.X) + 1;
        var height = cells.Max(cell => cell.Y) + 1;
            
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cellToLink = GetCellByColumnAndRow(cells, x, y);
                cellToLink.SetNeighbours(cells, new Vector(width, height, 0));
            }
        }

        return cells;
    }
    
    private TK GetNextCellCandidate(TK cell)
    {
        var allNeighbours = this.GetAllNeighbours(cell);

        var result = allNeighbours[this.randomGenerator.Next(0, allNeighbours.Length)];

        return result;
    }
    
    private TK[] GetAllNeighbours(TK cell)
    {
        var result = new List<TK>();
        foreach (TK value in cell.Neighbours.Values)
        {
            if (value != null)
            {
                result.Add(value);
            }
        }

        return result.ToArray();
    }
}