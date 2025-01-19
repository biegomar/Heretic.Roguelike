using Heretic.Roguelike.ArtificialIntelligence.Movements;
using Heretic.Roguelike.Maps;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Things.Monsters;
using Moq;

namespace Heretic.Roguelike.Tests
{
    public class CellTests
    {
        private readonly Mock<IMotionControllerFactory<string>> motionControllerFactoryMock =
            new Mock<IMotionControllerFactory<string>>();
        private readonly Mock<IMotionController<string>> motionControllerMock = new Mock<IMotionController<string>>();
        
        private readonly Mock<IMotionControllerFactory<int>> motionControllerFactoryIntMock =
            new Mock<IMotionControllerFactory<int>>();
        private readonly Mock<IMotionController<int>> motionControllerIntMock = new Mock<IMotionController<int>>();

        public CellTests()
        {
            // Arrange
            motionControllerFactoryMock
                .Setup(factory => factory.CreateMonsterMotionController(It.IsAny<IMonsterBreed>(), It.IsAny<Vector>()))
                .Returns(motionControllerMock.Object);
            
            motionControllerFactoryIntMock
                .Setup(factory => factory.CreateMonsterMotionController(It.IsAny<IMonsterBreed>(), It.IsAny<Vector>()))
                .Returns(motionControllerIntMock.Object);
        }
        
        [Fact]
        public void Cell_ShouldInitialize_WithCorrectCoordinates()
        {
            // Act
            var cell = new Cell<int>()
            {
                X = 1, 
                Y = 2, 
                Z = 3
            };

            // Assert
            Assert.Equal(1, cell.X);
            Assert.Equal(2, cell.Y);
            Assert.Equal(3, cell.Z);
        }

        [Fact]
        public void Cell_ShouldImplementBothSquareAndHexInterfaces()
        {
            // Arrange
            var cell = new SquareCell<int>();

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
            var monster = new Monster<string>(motionControllerMock.Object);
            var cell = new Cell<string> { IsVisible = true, Item = monster };

            // Assert
            Assert.True(cell.IsVisible);
            Assert.Equal(monster, cell.Item);
        }

        [Fact]
        public void Cell_ShouldSetAndRetrieveAllNeighbours()
        {
            // Arrange
            var cell = new SquareCell<int>();

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
            ISquareCell<int> squareCell = new SquareCell<int>();

            // Act & Assert
            Assert.Equal(0, squareCell.X);
            Assert.Equal(0, squareCell.Y);
        }

        [Fact]
        public void Cell_AsIHexCell_ShouldSupportHexSpecificOperations()
        {
            // Arrange
            IOrthogonalCell<int> hexCell = new Cell<int>();

            // Act & Assert
            Assert.NotNull(hexCell);
        }

        [Fact]
        public void Cell_ShouldSupportDifferentGenericTypes()
        {
            // Arrange
            var intMonster = new Monster<int>(motionControllerIntMock.Object);
            var intCell = new Cell<int> { Item = intMonster };
            
            var monster = new Monster<string>(motionControllerMock.Object);
            var stringCell = new Cell<string> { Item = monster };

            // Assert
            Assert.IsType<Monster<int>>(intCell.Item);
            Assert.IsType<Monster<string>>(stringCell.Item);
        }
    }
}