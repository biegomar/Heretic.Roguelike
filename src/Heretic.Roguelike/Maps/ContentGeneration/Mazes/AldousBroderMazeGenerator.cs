using System;
using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.Maps.ContentGeneration.Mazes;

public class AldousBroderMazeGenerator<T, TK>: BaseMazeGenerator<T, TK> where TK : class, ICell<T>, new()
{
    private Random randomGenerator = new Random();
    private int countOfCells;
    
    public override IList<TK> Generate(IList<TK> cells)
    {
        var dimensionZeroLength = cells.Max(cell => cell.X) + 1;
        var dimensionOneLength = cells.Max(cell => cell.Y) + 1;

        var startPositionX = randomGenerator.Next(0, dimensionZeroLength);
        var startPositionY = randomGenerator.Next(0, dimensionOneLength);

        var actualCell = GetCellByColumnAndRow(cells, startPositionX, startPositionY);

        countOfCells = cells.Count - 1;
        
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