using Heretic.Roguelike.Maps;
using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.Tests
{
    public class CellTests
    {
        [Fact]
        public void Cell_ShouldInitialize_WithCorrectCoordinates()
        {
            // Act
            var cell = new Cell<int>(x: 1, y: 2, z: 3);

            // Assert
            Assert.Equal(1, cell.X);
            Assert.Equal(2, cell.Y);
            Assert.Equal(3, cell.Z);
        }

        [Fact]
        public void Cell_ShouldImplementBothSquareAndHexInterfaces()
        {
            // Arrange
            var cell = new Cell<int>();

            // Act + Assert
            Assert.IsAssignableFrom<ISquareCell<int>>(cell);
            Assert.IsAssignableFrom<IHexCell<int>>(cell);
        }

        [Fact]
        public void Cell_ShouldSetAndRetrieveNeighboursCorrectly()
        {
            // Arrange
            var cell = new Cell<int>();
            var northCell = new Cell<int>();
            var eastCell = new Cell<int>();

            // Act
            cell.NorthernNeighbour = northCell;
            cell.EasternNeighbour = eastCell;

            // Assert
            Assert.Equal(northCell, cell.NorthernNeighbour);
            Assert.Equal(eastCell, cell.EasternNeighbour);
        }

        [Fact]
        public void Cell_ShouldLinkTwoCellsTogether()
        {
            // Arrange
            var cell1 = new Cell<int>();
            var cell2 = new Cell<int>();

            // Act
            cell1.LinkCell(cell2);

            // Assert
            Assert.Contains(cell2, cell1.LinkedCells);
            Assert.Contains(cell1, cell2.LinkedCells);
        }

        [Fact]
        public void Cell_ShouldStoreNeighboursInDirectionsDictionary()
        {
            // Arrange
            var cell = new Cell<int>();
            var southCell = new Cell<int>();

            // Act
            cell.SouthernNeighbour = southCell;

            // Assert
            Assert.Equal(southCell, cell.Neighbours[Directions.South]);
        }

        [Fact]
        public void Cell_ShouldSetAndGetVisibilityStateAndItem()
        {
            // Arrange
            var cell = new Cell<string> { IsVisible = true, Item = "Sword" };

            // Assert
            Assert.True(cell.IsVisible);
            Assert.Equal("Sword", cell.Item);
        }

        [Fact]
        public void Cell_ShouldSetAndRetrieveAllNeighbours()
        {
            // Arrange
            var cell = new Cell<int>();

            var north = new Cell<int>();
            var south = new Cell<int>();
            var east = new Cell<int>();
            var west = new Cell<int>();
            var northeast = new Cell<int>();
            var southwest = new Cell<int>();

            // Act
            cell.NorthernNeighbour = north;
            cell.SouthernNeighbour = south;
            cell.EasternNeighbour = east;
            cell.WesternNeighbour = west;
            cell.NorthernEastNeighbour = northeast;
            cell.SouthernWestNeighbour = southwest;

            // Assert
            Assert.Equal(north, cell.NorthernNeighbour);
            Assert.Equal(south, cell.SouthernNeighbour);
            Assert.Equal(east, cell.EasternNeighbour);
            Assert.Equal(west, cell.WesternNeighbour);
            Assert.Equal(northeast, cell.NorthernEastNeighbour);
            Assert.Equal(southwest, cell.SouthernWestNeighbour);
        }

        [Fact]
        public void Cell_ShouldHandleNullNeighbours()
        {
            // Arrange
            var cell = new Cell<int>();

            // Act
            cell.NorthernNeighbour = null;

            // Assert
            Assert.Null(cell.NorthernNeighbour);
        }

        [Fact]
        public void Cell_AsISquareCell_ShouldExposeExpectedPropertiesAndMethods()
        {
            // Arrange
            ISquareCell<int> squareCell = new Cell<int>();

            // Act & Assert
            Assert.Equal(0, squareCell.X);
            Assert.Equal(0, squareCell.Y);
        }

        [Fact]
        public void Cell_AsIHexCell_ShouldSupportHexSpecificOperations()
        {
            // Arrange
            IHexCell<int> hexCell = new Cell<int>();

            // Act & Assert
            Assert.NotNull(hexCell);
        }

        [Fact]
        public void Cell_ShouldSupportDifferentGenericTypes()
        {
            // Arrange
            var intCell = new Cell<int> { Item = 42 };
            var stringCell = new Cell<string> { Item = "Hello" };

            // Assert
            Assert.Equal(42, intCell.Item);
            Assert.Equal("Hello", stringCell.Item);
        }
    }
}