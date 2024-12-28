using System;
using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.Maps.ContentGeneration.Mazes;

public class AldousBroderMazeGenerator<T>: BaseMazeGenerator<T>
{
    private Random randomGenerator = new Random();
    private int countOfCells;
    
    public override IList<Cell<T>> Generate(IList<Cell<T>> cells)
    {
        var dimensionZeroLength = cells.Max(cell => cell.X) + 1;
        var dimensionOneLength = cells.Max(cell => cell.Y) + 1;

        var startPositionX = randomGenerator.Next(0, dimensionZeroLength);
        var startPositionY = randomGenerator.Next(0, dimensionOneLength);

        var actualCell = GetCellByColumnAndRow(cells, startPositionX, startPositionY);

        countOfCells = cells.Count - 1;
            
        do
        {
            Cell<T> nextCell = this.GetNextCellCandidate(actualCell);
                
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
    
    private Cell<T> GetNextCellCandidate(Cell<T> cell)
    {
        var allNeighbours = this.GetAllNeighbours(cell);

        var result = allNeighbours[this.randomGenerator.Next(0, allNeighbours.Length)];

        return result;
    }
    
    private Cell<T>[] GetAllNeighbours(Cell<T> cell)
    {
        var result = new List<Cell<T>>();

        cell.Neighbours.Values.ToList().ForEach(result!.Add);

        return result.ToArray();
    }
}