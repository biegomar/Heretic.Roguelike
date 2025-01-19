using Heretic.Roguelike.Creatures;
using Heretic.Roguelike.Creatures.Monsters;
using Heretic.Roguelike.Maps.Cells;
using Heretic.Roguelike.Maps.ContentGeneration;
using Heretic.Roguelike.Numerics;
using Heretic.Roguelike.Utils;
using Moq;

namespace Heretic.Roguelike.Tests;

public class LandscapeTests
{
    private readonly Mock<IProceduralContentGenerator<int, Cell<int>>> proceduralContentGeneratorMock = new();
    private readonly Mock<IContentPrinter<int, Cell<int>>> contentPrinterMock = new();
    private readonly Mock<ICreature<int>> intMonsterMock = new();

    // Testet, ob die Cells-Liste korrekt initialisiert wird
    [Fact]
    public void Landscape_InitializesCells_Correctly()
    {
        // Arrange
        var dimension = new Vector(10, 10, 1);
        var initializedCells = new List<Cell<int>>
        {
            new()
            {
                X = 0,
                Y = 0,
                Z = 0
            },
            new()
            {
                X = 1,
                Y = 0,
                Z = 0
            },
        };

        proceduralContentGeneratorMock
            .Setup(gen => gen.InitializeCells(dimension))
            .Returns(initializedCells);

        proceduralContentGeneratorMock
            .Setup(gen => gen.LinkCells(initializedCells))
            .Returns(initializedCells);
        
        proceduralContentGeneratorMock
            .Setup(gen => gen.Generate(initializedCells))
            .Returns(initializedCells);

        // Act
        var landscape = new Landscape<int, Cell<int>>(dimension, proceduralContentGeneratorMock.Object, contentPrinterMock.Object);

        // Assert
        Assert.Equal(initializedCells, landscape.Cells);
        proceduralContentGeneratorMock.Verify(gen => gen.InitializeCells(dimension), Times.Once);
        proceduralContentGeneratorMock.Verify(gen => gen.LinkCells(initializedCells), Times.Once);
    }

    // Testet, ob ein Cell Item korrekt gesetzt wird
    [Fact]
    public void SetCellItem_SetsItemCorrectly()
    {
        // Arrange
        var dimension = new Vector(5, 5, 1);
        var cells = new List<Cell<int>>
        {
            new()
            {
                X = 0,
                Y = 0,
                Z = 0,
                Item = null!
            },
            new()
            {
                X = 1,
                Y = 1,
                Z = 0,
                Item = null!
            },
        };

        proceduralContentGeneratorMock
            .Setup(gen => gen.InitializeCells(dimension))
            .Returns(cells);
        proceduralContentGeneratorMock
            .Setup(gen => gen.LinkCells(cells))
            .Returns(cells);
        proceduralContentGeneratorMock
            .Setup(gen => gen.Generate(cells))
            .Returns(cells);
        
        var landscape = new Landscape<int, Cell<int>>(dimension, proceduralContentGeneratorMock.Object, contentPrinterMock.Object);
        var cellItem = new CellItem<int>(intMonsterMock.Object, new Vector(1, 1, 0));

        // Act
        landscape.SetCellItem(cellItem);

        // Assert
        Assert.Equal(intMonsterMock.Object, landscape.Cells[1].Item); // Der Wert sollte gesetzt werden
    }

    // Testet, ob eine Zelle korrekt geleert wird
    [Fact]
    public void ClearCellItem_RemovesItem()
    {
        // Arrange
        var dimension = new Vector(5, 5, 1);
        var cells = new List<Cell<int>>
        {
            new()
            {
                X = 0,
                Y = 0,
                Z = 0,
                Item = intMonsterMock.Object
            },
            new()
            {
                X = 1,
                Y = 1,
                Z = 0,
                Item = intMonsterMock.Object
            },
        };

        proceduralContentGeneratorMock
            .Setup(gen => gen.InitializeCells(dimension))
            .Returns(cells);
        proceduralContentGeneratorMock
            .Setup(gen => gen.LinkCells(cells))
            .Returns(cells);
        proceduralContentGeneratorMock
            .Setup(gen => gen.Generate(cells))
            .Returns(cells);

        var landscape = new Landscape<int, Cell<int>>(dimension, proceduralContentGeneratorMock.Object, contentPrinterMock.Object);
        var position = new Vector(0, 0, 0);

        // Act
        landscape.RemoveCellItem(position);

        // Assert
        Assert.Null(landscape.Cells[0].Item); // Der Wert sollte auf den Standardwert gesetzt sein
    }

    // Testet die Draw-Methode
    [Fact]
    public void Draw_CallsContentPrinter()
    {
        // Arrange
        var dimension = new Vector(10, 10, 1);
        proceduralContentGeneratorMock
            .Setup(gen => gen.InitializeCells(dimension))
            .Returns(new List<Cell<int>>());
        proceduralContentGeneratorMock
            .Setup(gen => gen.LinkCells(It.IsAny<IList<Cell<int>>>()))
            .Returns(new List<Cell<int>>());

        var landscape = new Landscape<int, Cell<int>>(dimension, proceduralContentGeneratorMock.Object, contentPrinterMock.Object);
        var startVector = new Vector(0, 0, 0);

        // Act
        landscape.Draw(startVector);

        // Assert
        contentPrinterMock.Verify(printer => printer.DrawCells(
                It.IsAny<IList<Cell<int>>>(), 
                startVector, 
                It.IsAny<string>(), 
                false), Times.Once
        );
    }
}