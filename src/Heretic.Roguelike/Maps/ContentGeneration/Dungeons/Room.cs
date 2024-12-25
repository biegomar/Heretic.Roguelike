using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.Maps.ContentGeneration.Dungeons;

public class Room<T> : Cell<T>
{
    private readonly IDictionary<Directions, Room<T>?> neighbours = new Dictionary<Directions, Room<T>?>();

    public IList<Cell<T>> Cells { get; set; } = new List<Cell<T>>();
    public bool Connected { get; set; } = false;
        
    public IList<Room<T>> LinkedRooms { get; } = new List<Room<T>>();

    public Room(int x = 0, int y = 0, int z = 0,
        Cell<T>? northernNeighbour = null,
        Cell<T>? easternNeighbour = null,
        Cell<T>? southernNeighbour = null,
        Cell<T>? westernNeighbour = null,
        Cell<T>? northEastNeighbour = null,
        Cell<T>? southEastNeighbour = null,
        Cell<T>? southWestNeighbour = null,
        Cell<T>? northWestNeighbour = null) : base(x, y, z, northernNeighbour, easternNeighbour, southernNeighbour,
        westernNeighbour, northEastNeighbour, southEastNeighbour, southWestNeighbour, northWestNeighbour)
    {

    }
}