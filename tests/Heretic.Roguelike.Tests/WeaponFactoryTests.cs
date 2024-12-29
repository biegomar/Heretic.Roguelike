using System;
using System.Collections.Generic;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.Weapons;
using Heretic.Roguelike.Weapons.Types;
using Moq;
using Xunit;

namespace Heretic.Roguelike.Tests
{
    public class WeaponFactoryTests
    {
        private readonly Mock<IWeaponType> woodenSwordMock = new Mock<IWeaponType>();
        
        [Theory]
        [InlineData(nameof(Mace), 2, DiceType.D4, 1, DiceType.D3)]
        [InlineData(nameof(Sword), 3, DiceType.D4, 1, DiceType.D2)]
        [InlineData(nameof(Bow), 1, DiceType.D1, 1, DiceType.D1)]
        [InlineData(nameof(Arrow), 1, DiceType.D1, 2, DiceType.D3)]
        [InlineData(nameof(Dagger), 1, DiceType.D6, 1, DiceType.D4)]
        [InlineData(nameof(TwoSword), 4, DiceType.D4, 1, DiceType.D2)]
        [InlineData(nameof(Dart), 1, DiceType.D1, 1, DiceType.D3)]
        [InlineData(nameof(Crossbow), 1, DiceType.D1, 1, DiceType.D1)]
        [InlineData(nameof(Bolt), 1, DiceType.D2, 2, DiceType.D5)]
        [InlineData(nameof(Spear), 2, DiceType.D3, 1, DiceType.D6)]
        [InlineData(nameof(Flame), 2, DiceType.D4, 1, DiceType.D3)]
        public void CreateWeapon_ShouldReturnCorrectWeapon(
            string weaponType, 
            int damageCount, DiceType damageDiceType, 
            int hurlCount, DiceType hurlDiceType)
        {
            // Arrange
            var factory = new WeaponFactory();

            // Act
            var weapon = factory.CreateWeapon(weaponType);

            // Assert
            Assert.NotNull(weapon); // Die zurückgegebene Waffe darf nicht null sein
            Assert.Equal(weaponType, weapon.Type); // Der Typ der Waffe muss dem gewünschten Waffentyp entsprechen
            Assert.NotNull(weapon.Damage);
            Assert.NotNull(weapon.HurlDamage);

            // Überprüfen der Schaden-Würfel
            Assert.Single(weapon.Damage);
            Assert.Equal(damageCount, weapon.Damage[0].Tries);
            Assert.Equal(damageDiceType, weapon.Damage[0].Dice.TypeOfDice);

            // Überprüfen der Wurf-Schaden-Würfel
            Assert.Single(weapon.HurlDamage);
            Assert.Equal(hurlCount, weapon.HurlDamage[0].Tries);
            Assert.Equal(hurlDiceType, weapon.HurlDamage[0].Dice.TypeOfDice);
        }

        [Fact]
        public void CreateWeapon_InvalidWeaponType_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange
            // Arrange
            // WoodenShield-Mock vorbereiten
            woodenSwordMock.Setup(z => z.Name).Returns("woodenSword");
            woodenSwordMock
                .Setup(z => z.Create())
                .Returns(() => new Weapon()
                {
                    Type = "woodenSword",
                });

            
            var factory = new WeaponFactory();
            var invalidWeaponType = woodenSwordMock.Object.Name;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => factory.CreateWeapon(invalidWeaponType));
        }
    }
}