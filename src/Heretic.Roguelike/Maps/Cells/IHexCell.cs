namespace Heretic.Roguelike.Maps.Cells;

public interface IHexCell<T> : ICell<T>
{
    Cell<T>? NorthernNeighbour { get; set; }
    Cell<T>? SouthernNeighbour { get; set; }
    Cell<T>? NorthernEastNeighbour { get; set; }
    Cell<T>? SouthernWestNeighbour { get; set; }
    Cell<T>? SouthernEastNeighbour { get; set; }
    Cell<T>? NorthernWestNeighbour { get; set; }
}