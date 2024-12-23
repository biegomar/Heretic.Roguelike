using Heretic.Roguelike.Subsystems.Dices;
using System;
using Xunit;

namespace Heretic.Roguelike.Tests
{
    public class DiceTests
    {
        [Fact]
        public void Dice_Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var diceType = DiceType.D6; 

            // Act
            var dice = new Dice(diceType);
            var expectedSides = (byte)diceType;

            // Assert
            Assert.Equal(diceType, dice.TypeOfDice);
            Assert.Equal(expectedSides, dice.Sides); 
        }

        [Fact]
        public void Dice_SingleRoll_ShouldReturnResultWithinRange()
        {
            // Arrange
            var diceType = DiceType.D6;
            var dice = new Dice(diceType);
            var expectedResultLow = 1;
            var expectedResultHigh = 6;

            // Act
            ushort result = dice.Roll(1);

            // Assert
            Assert.InRange(result, expectedResultLow, expectedResultHigh);
        }

        [Fact]
        public void Dice_MultipleRoll_ShouldReturnCorrectSum()
        {
            // Arrange
            var diceType = DiceType.D6;
            var dice = new Dice(diceType);
            byte rolls = 3;
            var expectedResultLow = 3;
            var expectedResultHigh = 18;

            // Act
            ushort result = dice.Roll(rolls); 

            // Assert
            Assert.InRange(result, expectedResultLow, expectedResultHigh); // Minimalwert: 3*1, Maximalwert: 3*6
        }

        [Fact]
        public void Dice_Roll_WithInjectedRandom_ShouldProduceExpectedResult()
        {
            // Arrange
            var diceType = DiceType.D6;
            ushort expectedResult = 5;

            // Test mit festgelegtem Random-Seed (für wiederholbare Tests)
            var random = new Random(42);
            var dice = new Dice(diceType, random);

            // Act
            ushort result = dice.Roll(1);

            // Assert
            Assert.Equal(expectedResult, result); 
        }

        [Fact]
        public void Dice_RollAndGetBestTry_ShouldReturnHighestResult()
        {
            // Arrange
            var diceType = DiceType.D6;
            var random = new Random(42); 
            var dice = new Dice(diceType, random);
            byte rolls = 5; 
            ushort expectedResult = 5;
            var expectedResultLow = 1;
            var expectedResultHigh = 6;

            // Act
            ushort actual = dice.RollAndGetBestTry(rolls);

            // Assert
            Assert.InRange(actual, expectedResultLow, expectedResultHigh); // Der beste Wurf muss zwischen 1 und 6 liegen
            Assert.Equal(expectedResult, actual); // Erwartetes Ergebnis für den Seed und beste Roll-Suche
        }

        [Fact]
        public void Dice_RollAndGetLowestTry_ShouldReturnLowestResult()
        {
            // Arrange
            var diceType = DiceType.D6;
            var random = new Random(42); 
            var dice = new Dice(diceType, random);
            byte rolls = 5; 
            var expectedResultLow = 1;
            var expectedResultHigh = 6;

            // Act
            ushort lowestTry = dice.RollAndGetLowestTry(rolls);

            // Assert
            Assert.InRange(lowestTry, expectedResultLow, expectedResultHigh); // Der niedrigste Wurf muss zwischen 1 und 6 liegen
            Assert.Equal(1, lowestTry); // Erwartetes Ergebnis für den Seed und niedrigste Roll-Suche
        }
    }
}