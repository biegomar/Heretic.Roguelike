using System.Collections.Generic;
using System.Numerics;


namespace Heretic.Roguelike.Maps.Cells;

public class Cell<T> : ISquareCell<T>, IHexCell<T>
    {
        private readonly IDictionary<Directions, Cell<T>?> neighbours = new Dictionary<Directions, Cell<T>?>();
        protected readonly IList<Cell<T>> linkedCells = new List<Cell<T>>();

        public int X { get; }
        public int Y { get; }
        public int Z { get; }
        public T Item { get; set; }

        public bool IsVisible { get; set; }
        
        public bool IsVisited { get; set; }

        public int PathCount { get; set; }

        public Cell<T> Predecessor { get; set; }
        
        public Cell<T>? NorthernNeighbour
        {
            get => neighbours[Directions.North];
            set => neighbours[Directions.North] = value;
        }

        public Cell<T>? EasternNeighbour
        {
            get => neighbours[Directions.East];
            set => neighbours[Directions.East] = value;
        }

        public Cell<T>? SouthernNeighbour
        {
            get => neighbours[Directions.South];
            set => neighbours[Directions.South] = value;
        }

        public Cell<T>? WesternNeighbour
        {
            get => neighbours[Directions.West];
            set => neighbours[Directions.West] = value;
        }
        
        public Cell<T>? NorthernEastNeighbour 
        {
            get => neighbours[Directions.Northeast];
            set => neighbours[Directions.Northeast] = value;
        }
        
        public Cell<T>? SouthernWestNeighbour
        {
            get => neighbours[Directions.Southwest];
            set => neighbours[Directions.Southwest] = value;
        }
        
        public Cell<T>? SouthernEastNeighbour 
        {
            get => neighbours[Directions.Southeast];
            set => neighbours[Directions.Southeast] = value;
        }
        
        public Cell<T>? NorthernWestNeighbour
        {
            get => neighbours[Directions.Northwest];
            set => neighbours[Directions.Northwest] = value;
        }

        public IList<Cell<T>> LinkedCells => this.linkedCells;

        public IDictionary<Directions, Cell<T>?> Neighbours => this.neighbours;
        
        public Cell(int x = 0, int y = 0, int z = 0, 
            Cell<T>? northernNeighbour = null, 
            Cell<T>? easternNeighbour = null, 
            Cell<T>? southernNeighbour = null, 
            Cell<T>? westernNeighbour = null,
            Cell<T>? northEastNeighbour = null,
            Cell<T>? southEastNeighbour = null,
            Cell<T>? southWestNeighbour = null,
            Cell<T>? northWestNeighbour = null)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
            neighbours.Add(Directions.North, northernNeighbour);
            neighbours.Add(Directions.East, easternNeighbour);
            neighbours.Add(Directions.South, southernNeighbour);
            neighbours.Add(Directions.West, westernNeighbour);
            neighbours.Add(Directions.Northeast, northEastNeighbour);
            neighbours.Add(Directions.Southeast, southEastNeighbour);
            neighbours.Add(Directions.Southwest, southWestNeighbour);
            neighbours.Add(Directions.Northwest, northWestNeighbour);
        }

        public void LinkCell(Cell<T>? cellToLink)
        {
            if (!this.LinkedCells.Contains(cellToLink))
            {
                this.LinkedCells.Add(cellToLink);
                cellToLink.LinkCell(this);
            }
        }
    }