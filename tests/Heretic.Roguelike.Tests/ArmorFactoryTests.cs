using System;
using Heretic.Roguelike.Amors;
using Xunit;

namespace Heretic.Roguelike.Tests
{
    public class ArmorFactoryTests
    {
        [Theory]
        [InlineData(ArmorType.Leather, 8)]
        [InlineData(ArmorType.RingMail, 7)]
        [InlineData(ArmorType.StuddedLeather, 7)]
        [InlineData(ArmorType.ScaleMail, 6)]
        [InlineData(ArmorType.ChainMail, 5)]
        [InlineData(ArmorType.SplintMail, 4)]
        [InlineData(ArmorType.BandedMail, 4)]
        [InlineData(ArmorType.PlateMail, 3)]
        public void CreateArmor_ShouldReturnCorrectArmor(ArmorType armorType, int expectedArmorClass)
        {
            // Arrange
            var factory = new ArmorFactory();

            // Act
            var armor = factory.CreateArmor(armorType);

            // Assert
            Assert.NotNull(armor); // Das zurückgegebene Objekt darf nicht null sein
            Assert.Equal(armorType, armor.Type); // Der Typ der Rüstung muss übereinstimmen
            Assert.Equal(ArmorFlag.IsKnown, armor.Flag); // Die Rüstung sollte das Flag "IsKnown" besitzen
            Assert.Equal(1, armor.Count); // Die Rüstungsanzahl ist immer 1
            Assert.Equal(expectedArmorClass, armor.AmorClass); // Der Rüstungswert (ArmorClass) muss korrekt sein
        }

        [Fact]
        public void CreateArmor_InvalidArmorType_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            var factory = new ArmorFactory();
            var invalidArmorType = (ArmorType)99; // Ungültiger Rüstungstyp

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => factory.CreateArmor(invalidArmorType));
        }
    }
}