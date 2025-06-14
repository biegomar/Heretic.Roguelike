using Heretic.Roguelike.Maps.ContentGeneration.Dungeons;
using Heretic.Roguelike.Numerics;

namespace Heretic.Roguelike.Tests;

public class InvisibleRoomsTests
{
    [Theory]
    [InlineData(3, 3, 4)] 
    [InlineData(10, 10, 40)]
    [InlineData(5, 5, 10)]  
    [InlineData(7, 3, 9)]    
    public void GetNumberOfMaxInvisibleRooms_ReturnsExpectedValue(int gridWidth, int gridHeight, int expected)
    {
        // Arrange
        var properties = new DungeonProperties(new Vector(80, 24, 0), new Vector(gridWidth, gridHeight, 0), 0);
        var dungeon = new DungeonOfDoomGenerator<char>(properties);
        var numberOfRooms = gridWidth * gridHeight;

        // Act
        var result = dungeon.GetMaxNumberOfInvisibleRooms(numberOfRooms);

        // Assert
        Assert.Equal(expected, result);
    }

}