using System.Text.RegularExpressions;
using HvalfangstApi.model;

namespace HvalfangstApi.util;

public static partial class HeroContentUtils
{
    public static Hero FromCompactString(string input)
    {
        // Define a regular expression to match the compact string format for a Hero.
        // The pattern expects the following format: Name_Level_Class_HitPoints_ArmorClass_Attack_Damage
            // - Name: One or more characters that are not underscores
            // - Level: One or more digits
            // - Class: One or more characters that are not underscores
            // - HitPoints: One or more digits
            // - ArmorClass: One or more digits
            // - Attack: One or more digits
            // - Damage: One or more digits
        var regex = HeroContentsRegEx();
        var match = regex.Match(input);

        // If the input string does not match the expected format, throw an exception
        if (!match.Success) throw new ArgumentException($"Invalid input string: [{input}]");
        
        var name = match.Groups["Name"].Value;
        var level = int.Parse(match.Groups["Level"].Value);
        var heroClass = match.Groups["Class"].Value;
        var hitPoints = int.Parse(match.Groups["HitPoints"].Value);
        var armorClass = int.Parse(match.Groups["ArmorClass"].Value);
        var attack = int.Parse(match.Groups["Attack"].Value);
        var damage = int.Parse(match.Groups["Damage"].Value);

        // Use the extracted values as needed
            
        return new Hero
        {
            Name = name,
            Level = level,
            Class = heroClass,
            HitPoints = hitPoints,
            ArmorClass = armorClass,
            Attack = attack,
            Damage = damage
        };

    }

      private static Regex HeroContentsRegEx()
    {
        return new Regex(@"^(?<Name>[^_]+)_(?<Level>\d+)_(?<Class>[^_]+)_(?<HitPoints>\d+)_(?<ArmorClass>\d+)_(?<Attack>\d+)_(?<Damage>\d+)$");
    }
}