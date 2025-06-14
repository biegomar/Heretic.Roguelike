using System;
using System.Collections.Generic;
using System.Linq;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Maps.ContentGeneration.Dungeons;

public class DungeonOfDoomGenerator<T> : BaseDungeonGenerator<T>
{
    private readonly Random randomGenerator = new();
    
    private readonly int dungeonWidth;
    private readonly int dungeonHeight;
    private readonly int gridWidth;
    private readonly int gridHeight;
    private readonly int roomWidth;
    private readonly int roomHeight;

    public DungeonOfDoomGenerator(DungeonProperties dungeonProperties)
    {
        this.MapType = MapTypes.Dungeon;
        dungeonWidth = (int)dungeonProperties.DungeonSize.X;
        dungeonHeight = (int)dungeonProperties.DungeonSize.Y;
        gridWidth = (int)dungeonProperties.GridSize.X;
        gridHeight = (int)dungeonProperties.GridSize.Y;
        roomWidth = (int)dungeonProperties.DungeonSize.X / (int)dungeonProperties.GridSize.X;
        roomHeight = (int)dungeonProperties.DungeonSize.Y / (int)dungeonProperties.GridSize.Y;
    }
    
    public override IEnumerable<ICell<T>> Generate(IEnumerable<ICell<T>> elements)
    {
        elements = this.SetInvisibleRooms(elements);
        
        for (var row = 0; row < gridHeight; row++)
        {
            for (var column = 0; column < gridWidth; column++)
            {
            
            }        
        }

        return elements;
    }

    public override IEnumerable<ICell<T>> LinkCells(IEnumerable<ICell<T>> elements)
    {
        throw new NotImplementedException();
    }
    
    internal IEnumerable<ICell<T>> SetInvisibleRooms(IEnumerable<ICell<T>> elements)
    {
        var maxNumberOfInvisibleRooms = this.GetMaxNumberOfInvisibleRooms(elements.Count());
        var numberOfInvisibleRooms = this.GetNumberOfInVisibleRooms(maxNumberOfInvisibleRooms);

        for (var i = 0; i < numberOfInvisibleRooms; i++)
        {
            var room = this.GetNextVisibleRoom(elements);
            room.IsVisible = false;
        }
        
        return elements;
    }

    private ICell<T> GetNextVisibleRoom(IEnumerable<ICell<T>> elements)
    {
        ICell<T> room;
        
        do
        {
            room = this.GetRandomRoom(elements);
        } while (room.IsVisible);

        return room;
    }

    private ICell<T> GetRandomRoom(IEnumerable<ICell<T>> elements)
    {
        var randomIndex = randomGenerator.Next(0, elements.Count());
        var room = elements.ToList()[randomIndex];
        
        return room;
    }
    
    internal int GetMaxNumberOfInvisibleRooms(int numberOfRooms)
    {
        return (int)Math.Ceiling(numberOfRooms * 0.4);   
    }
    
    private int GetNumberOfInVisibleRooms(int maxNumberOfInvisibleRooms)
    {
        return randomGenerator.Next(0, maxNumberOfInvisibleRooms + 1);
    }
}