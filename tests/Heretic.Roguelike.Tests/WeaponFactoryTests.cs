using System;
using System.Collections.Generic;
using Heretic.Roguelike.Dices;
using Heretic.Roguelike.Weapons;
using Xunit;

namespace Heretic.Roguelike.Tests
{
    public class WeaponFactoryTests
    {
        [Theory]
        [InlineData(WeaponType.Mace, 2, DiceType.D4, 1, DiceType.D3)]
        [InlineData(WeaponType.Sword, 3, DiceType.D4, 1, DiceType.D2)]
        [InlineData(WeaponType.Bow, 1, DiceType.D1, 1, DiceType.D1)]
        [InlineData(WeaponType.Arrow, 1, DiceType.D1, 2, DiceType.D3)]
        [InlineData(WeaponType.Dagger, 1, DiceType.D6, 1, DiceType.D4)]
        [InlineData(WeaponType.TwoSword, 4, DiceType.D4, 1, DiceType.D2)]
        [InlineData(WeaponType.Dart, 1, DiceType.D1, 1, DiceType.D3)]
        [InlineData(WeaponType.Crossbow, 1, DiceType.D1, 1, DiceType.D1)]
        [InlineData(WeaponType.Bolt, 1, DiceType.D2, 2, DiceType.D5)]
        [InlineData(WeaponType.Spear, 2, DiceType.D3, 1, DiceType.D6)]
        [InlineData(WeaponType.Flame, 2, DiceType.D4, 1, DiceType.D3)]
        public void CreateWeapon_ShouldReturnCorrectWeapon(
            WeaponType weaponType, 
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
            var factory = new WeaponFactory();
            var invalidWeaponType = (WeaponType)99; // Ungültiger Waffentyp

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => factory.CreateWeapon(invalidWeaponType));
        }
    }
}