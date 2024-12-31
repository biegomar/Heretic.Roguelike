using System.Collections.Generic;
using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.Maps.ContentGeneration.Dungeons;

public class Room<T> : Cell<T>
{
    private readonly IDictionary<Directions, Room<T>?> neighbours = new Dictionary<Directions, Room<T>?>();

    public IList<Cell<T>> Cells { get; set; } = new List<Cell<T>>();
    public bool Connected { get; set; } = false;
        
    public IList<Room<T>> LinkedRooms { get; } = new List<Room<T>>();
}