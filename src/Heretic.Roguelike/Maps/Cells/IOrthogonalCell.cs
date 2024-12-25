namespace Heretic.Roguelike.Maps.Cells;

public interface IOrthogonalCell<T>: ICell<T>
{
    Cell<T>? NorthernNeighbour { get; set; }
    Cell<T>? SouthernNeighbour { get; set; }
    Cell<T>? EasternNeighbour { get; set; }
    Cell<T>? WesternNeighbour { get; set; }
}