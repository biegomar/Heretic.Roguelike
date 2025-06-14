using System;
using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.ContentGeneration.Mazes;

public class AldousBroderMazeGenerator<T>: BaseMazeGenerator<T>
{
    private readonly Random randomGenerator = new();

    public AldousBroderMazeGenerator()
    {
        this.MapType = MapTypes.Maze;
    }
    
    public override IEnumerable<ICell<T>> Generate(IEnumerable<ICell<T>> elements)
    {
        var dimensionZeroLength = elements.Max(cell => cell.X) + 1;
        var dimensionOneLength = elements.Max(cell => cell.Y) + 1;

        var startPositionX = randomGenerator.Next(0, dimensionZeroLength);
        var startPositionY = randomGenerator.Next(0, dimensionOneLength);

        var actualCell = GetCellByColumnAndRow(elements, startPositionX, startPositionY);

        var countOfCells = elements.Count() - 1;
        
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

        return elements;
    }
    
    public override IEnumerable<ICell<T>> LinkCells(IEnumerable<ICell<T>> elements)
    {
        var linkCells = elements.ToList();
        var width = linkCells.Max(cell => cell.X) + 1;
        var height = linkCells.Max(cell => cell.Y) + 1;
            
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var cellToLink = GetCellByColumnAndRow(linkCells, x, y);
                cellToLink.SetNeighbours(linkCells, new Vector(width, height, 0));
            }
        }

        return linkCells;
    }
    
    private ICell<T> GetNextCellCandidate(ICell<T> cell)
    {
        var allNeighbours = this.GetAllNeighbours(cell);

        var result = allNeighbours[this.randomGenerator.Next(0, allNeighbours.Length)];

        return result;
    }
    
    private ICell<T>[] GetAllNeighbours(ICell<T> cell)
    {
        var result = new List<ICell<T>>();
        foreach (ICell<T> value in cell.Neighbours.Values)
        {
            if (value != null)
            {
                result.Add(value);
            }
        }

        return result.ToArray();
    }
}