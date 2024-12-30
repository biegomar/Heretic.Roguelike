using Heretic.Roguelike.Amours;
using Heretic.Roguelike.Amours.Types;
using Moq;

namespace Heretic.Roguelike.Tests
{
    public class ArmourFactoryTests
    {
        private readonly Mock<IArmourType> woodenShieldMock = new Mock<IArmourType>();
        
        [Theory]
        [InlineData(nameof(Leather), 8)]
        [InlineData(nameof(RingMail), 7)]
        [InlineData(nameof(StuddedLeather), 7)]
        [InlineData(nameof(ScaleMail), 6)]
        [InlineData(nameof(ChainMail), 5)]
        [InlineData(nameof(SplintMail), 4)]
        [InlineData(nameof(BandedMail), 4)]
        [InlineData(nameof(PlateMail), 3)]
        public void CreateArmor_ShouldReturnCorrectArmor(string armorType, int expectedArmorClass)
        {
            // Arrange
            var factory = new ArmourFactory();

            // Act
            var armor = factory.CreateArmour(armorType);

            // Assert
            Assert.NotNull(armor); // Das zurückgegebene Objekt darf nicht null sein
            Assert.Equal(armorType, armor.Type); // Der Typ der Rüstung muss übereinstimmen
            Assert.Equal(ArmourFlag.IsKnown, armor.Flag); // Die Rüstung sollte das Flag "IsKnown" besitzen
            Assert.Equal(1, armor.Count); // Die Rüstungsanzahl ist immer 1
            Assert.Equal(expectedArmorClass, armor.AmorClass); // Der Rüstungswert (ArmorClass) muss korrekt sein
        }

        [Fact]
        public void CreateArmor_InvalidArmorType_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            // WoodenShield-Mock vorbereiten
            woodenShieldMock.Setup(z => z.Name).Returns("WoodenShield");
            woodenShieldMock
                .Setup(z => z.Create())
                .Returns(() => new Armour()
                {
                    Type = "WoodenShield",
                });
            
            var factory = new ArmourFactory();
            var invalidArmorType = woodenShieldMock.Object.Name; // Ungültiger Rüstungstyp

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => factory.CreateArmour(invalidArmorType));
        }
    }
}