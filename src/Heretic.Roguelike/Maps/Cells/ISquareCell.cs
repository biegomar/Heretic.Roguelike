namespace Heretic.Roguelike.Maps.Cells;

public interface ISquareCell<T> : IOrthogonalCell<T>
{
    Cell<T>? NorthernEastNeighbour { get; set; }
    Cell<T>? SouthernWestNeighbour { get; set; }
    Cell<T>? SouthernEastNeighbour { get; set; }
    Cell<T>? NorthernWestNeighbour { get; set; }
}