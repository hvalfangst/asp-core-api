namespace Api.Models;

public class Hero
{
    public string? Name { get; init; }
    public string? Class { get; init; }
    public int Level { get; init; }
    public int HitPoints { get; init; }
    public int Damage { get; init; }
    public int Attack { get; init; }
    public int ArmorClass { get; init; }
}