using System.Reflection;
using Heretic.Roguelike.Maps.Cells;

namespace Heretic.Roguelike.Tests
{
    public class CellPolymorphismTests
    {
        [Fact]
        public void ISquareCell_ShouldHaveAccessToOrthogonalAndDiagonalNeighbours()
        {
            // Arrange
            ISquareCell<int> squareCell = new Cell<int>();

            // Act
            squareCell.NorthernNeighbour = new Cell<int>();
            squareCell.SouthernNeighbour = new Cell<int>();
            squareCell.EasternNeighbour = new Cell<int>();
            squareCell.WesternNeighbour = new Cell<int>();
            squareCell.NorthernEastNeighbour = new Cell<int>();
            squareCell.SouthernWestNeighbour = new Cell<int>();
            squareCell.SouthernEastNeighbour = new Cell<int>();
            squareCell.NorthernWestNeighbour = new Cell<int>();

            // Assert
            Assert.NotNull(squareCell.NorthernNeighbour);
            Assert.NotNull(squareCell.SouthernNeighbour);
            Assert.NotNull(squareCell.EasternNeighbour);
            Assert.NotNull(squareCell.WesternNeighbour);
            Assert.NotNull(squareCell.NorthernEastNeighbour);
            Assert.NotNull(squareCell.SouthernWestNeighbour);
            Assert.NotNull(squareCell.SouthernEastNeighbour);
            Assert.NotNull(squareCell.NorthernWestNeighbour);
        }

        [Fact]
        public void IHexCell_ShouldHaveAccessToHexNeighbours()
        {
            // Arrange
            IHexCell<int> hexCell = new Cell<int>();

            // Act & Assert
            hexCell.NorthernNeighbour = new Cell<int>();
            hexCell.SouthernNeighbour = new Cell<int>();
            hexCell.NorthernEastNeighbour = new Cell<int>();
            hexCell.SouthernWestNeighbour = new Cell<int>();
            hexCell.SouthernEastNeighbour = new Cell<int>();
            hexCell.NorthernWestNeighbour = new Cell<int>();

            Assert.NotNull(hexCell.NorthernNeighbour);
            Assert.NotNull(hexCell.SouthernNeighbour);
            Assert.NotNull(hexCell.NorthernEastNeighbour);
            Assert.NotNull(hexCell.SouthernWestNeighbour);
            Assert.NotNull(hexCell.SouthernEastNeighbour);
            Assert.NotNull(hexCell.NorthernWestNeighbour);
        }
        
        [Fact]
        public void IHexCell_ShouldNotHaveAccessToOrthogonalNeighbours()
        {
            // Arrange
            var hexCellType = typeof(IHexCell<int>);

            // Act
            var easternNeighbourProperty = hexCellType.GetProperty("EasternNeighbour");
            
            // Assert
            Assert.Null(easternNeighbourProperty); 
        }

        [Fact]
        public void IOrthogonalCell_ShouldOnlyHaveOrthogonalNeighbours()
        {
            // Arrange
            IOrthogonalCell<int> orthogonalCell = new Cell<int>();

            // Act
            orthogonalCell.NorthernNeighbour = new Cell<int>();
            orthogonalCell.SouthernNeighbour = new Cell<int>();
            orthogonalCell.EasternNeighbour = new Cell<int>();
            orthogonalCell.WesternNeighbour = new Cell<int>();

            // Assert
            Assert.NotNull(orthogonalCell.NorthernNeighbour);
            Assert.NotNull(orthogonalCell.SouthernNeighbour);
            Assert.NotNull(orthogonalCell.EasternNeighbour);
            Assert.NotNull(orthogonalCell.WesternNeighbour);
        }

        [Fact]
        public void IHexCell_ShouldNotContainOrthogonalNeighbours()
        {
            // Arrange & Act - Try accessing EasternNeighbour via Reflection
            var easternNeighbourProperty = typeof(IHexCell<int>).GetProperty("EasternNeighbour");
            var westernNeighbourProperty = typeof(IHexCell<int>).GetProperty("WesternNeighbour");

            // Assert
            Assert.Null(easternNeighbourProperty); // EasternNeighbour sollte nicht existieren
            Assert.Null(westernNeighbourProperty); // WesternNeighbour sollte nicht existieren
        }

        [Fact]
        public void ISquareCell_ShouldContainOrthogonalAndDiagonalNeighbours()
        {
            // Arrange & Act - Suche nach Eigenschaften in ISquareCell und allen geerbten Interfaces
            var northernNeighbourProperty = GetPropertyRecursive(typeof(ISquareCell<int>), "NorthernNeighbour");
            var easternNeighbourProperty = GetPropertyRecursive(typeof(ISquareCell<int>), "EasternNeighbour");
            var southernNeighbourProperty = GetPropertyRecursive(typeof(ISquareCell<int>), "SouthernNeighbour");
            var westernNeighbourProperty = GetPropertyRecursive(typeof(ISquareCell<int>), "WesternNeighbour");

            var northernEastNeighbourProperty = GetPropertyRecursive(typeof(ISquareCell<int>), "NorthernEastNeighbour");
            var southernWestNeighbourProperty = GetPropertyRecursive(typeof(ISquareCell<int>), "SouthernWestNeighbour");
            var southernEastNeighbourProperty = GetPropertyRecursive(typeof(ISquareCell<int>), "SouthernEastNeighbour");
            var northernWestNeighbourProperty = GetPropertyRecursive(typeof(ISquareCell<int>), "NorthernWestNeighbour");

            // Assert - Orthogonal neighbours should exist
            Assert.NotNull(northernNeighbourProperty);
            Assert.NotNull(easternNeighbourProperty);
            Assert.NotNull(southernNeighbourProperty);
            Assert.NotNull(westernNeighbourProperty);

            // Assert - Diagonal neighbours should also exist
            Assert.NotNull(northernEastNeighbourProperty);
            Assert.NotNull(southernWestNeighbourProperty);
            Assert.NotNull(southernEastNeighbourProperty);
            Assert.NotNull(northernWestNeighbourProperty);
        }

        [Fact]
        public void IOrthogonalCell_ShouldNotContainDiagonalNeighbours()
        {
            // Arrange & Act - Check for diagonal neighbours via Reflection
            var northernEastNeighbourProperty = typeof(IOrthogonalCell<int>).GetProperty("NorthernEastNeighbour");
            var southernWestNeighbourProperty = typeof(IOrthogonalCell<int>).GetProperty("SouthernWestNeighbour");
            var southernEastNeighbourProperty = typeof(IOrthogonalCell<int>).GetProperty("SouthernEastNeighbour");
            var northernWestNeighbourProperty = typeof(IOrthogonalCell<int>).GetProperty("NorthernWestNeighbour");

            // Assert - Diagonal neighbours should not exist
            Assert.Null(northernEastNeighbourProperty);
            Assert.Null(southernWestNeighbourProperty);
            Assert.Null(southernEastNeighbourProperty);
            Assert.Null(northernWestNeighbourProperty);
        }

        [Fact]
        public void ICell_ShouldNotContainNeighbourSpecificProperties()
        {
            // Arrange & Act - Check for all neighbour properties via Reflection
            var northernNeighbourProperty = typeof(ICell<int>).GetProperty("NorthernNeighbour");
            var easternNeighbourProperty = typeof(ICell<int>).GetProperty("EasternNeighbour");
            var southernNeighbourProperty = typeof(ICell<int>).GetProperty("SouthernNeighbour");
            var westernNeighbourProperty = typeof(ICell<int>).GetProperty("WesternNeighbour");

            var northernEastNeighbourProperty = typeof(ICell<int>).GetProperty("NorthernEastNeighbour");
            var southernWestNeighbourProperty = typeof(ICell<int>).GetProperty("SouthernWestNeighbour");
            var southernEastNeighbourProperty = typeof(ICell<int>).GetProperty("SouthernEastNeighbour");
            var northernWestNeighbourProperty = typeof(ICell<int>).GetProperty("NorthernWestNeighbour");

            // Assert - None of these properties should exist in the base ICell<T>
            Assert.Null(northernNeighbourProperty);
            Assert.Null(easternNeighbourProperty);
            Assert.Null(southernNeighbourProperty);
            Assert.Null(westernNeighbourProperty);

            Assert.Null(northernEastNeighbourProperty);
            Assert.Null(southernWestNeighbourProperty);
            Assert.Null(southernEastNeighbourProperty);
            Assert.Null(northernWestNeighbourProperty);
        }
        
        private PropertyInfo GetPropertyRecursive(Type type, string propertyName)
        {
            // Prüfe, ob die Eigenschaft direkt in der Interface-Deklaration existiert
            var property = type.GetProperty(propertyName);
            if (property != null)
            {
                return property;
            }

            // Rekursives Überprüfen der Basisschnittstellen
            foreach (var baseInterface in type.GetInterfaces())
            {
                var baseProperty = GetPropertyRecursive(baseInterface, propertyName);
                if (baseProperty != null)
                {
                    return baseProperty;
                }
            }

            return null;
        }
    }
}