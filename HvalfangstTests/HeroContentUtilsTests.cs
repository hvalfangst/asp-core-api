using HvalfangstApi.util;

namespace HvalfangstTests
{
    
    public class HeroContentUtilsTests
    {
        [TestCase("HeroName_10_Warrior_100_50_20_30", "HeroName", 10, "Warrior", 100, 50, 20, 30)]
        [TestCase("AnotherHero_5_Mage_80_40_15_25", "AnotherHero", 5, "Mage", 80, 40, 15, 25)]
        public void FromCompactString_ValidInput_ShouldReturnHero(string input, string expectedName, int expectedLevel, string expectedClass, int expectedHitPoints, int expectedArmorClass, int expectedAttack, int expectedDamage)
        {
            // Act
            var result = HeroContentUtils.FromCompactString(input);
    
            // Assert
            Assert.That(result.Name, Is.EqualTo(expectedName));
            Assert.That(result.Level, Is.EqualTo(expectedLevel));
            Assert.That(result.Class, Is.EqualTo(expectedClass));
            Assert.That(result.HitPoints, Is.EqualTo(expectedHitPoints));
            Assert.That(result.ArmorClass, Is.EqualTo(expectedArmorClass));
            Assert.That(result.Attack, Is.EqualTo(expectedAttack));
            Assert.That(result.Damage, Is.EqualTo(expectedDamage));
        }
    
        [TestCase("InvalidString")]
        [TestCase("HeroName_10_Warrior_100_50_20")]
        [TestCase("HeroName_10_Warrior_100_50_20_30_Extra")]
        public void FromCompactString_InvalidInput_ShouldThrowArgumentException(string input)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => HeroContentUtils.FromCompactString(input));
        }
    }
}