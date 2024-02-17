using System.ComponentModel.DataAnnotations;

namespace Api.Models;

public class HeroInputModel
{
    [Required(ErrorMessage = "Name is required.")]
    [StringLength(50, ErrorMessage = "Name cannot exceed 50 characters.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "Class is required.")]
    [StringLength(20, ErrorMessage = "Class cannot exceed 20 characters.")]
    public string? Class { get; set; }

    [Required(ErrorMessage = "Level is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Level must be at least 1.")]
    public int Level { get; set; }

    [Required(ErrorMessage = "HitPoints is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "HitPoints must be non-negative.")]
    public int HitPoints { get; set; }

    [Required(ErrorMessage = "Damage is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Damage must be non-negative.")]
    public int Damage { get; set; }

    [Required(ErrorMessage = "Attack is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Attack must be non-negative.")]
    public int Attack { get; set; }

    [Required(ErrorMessage = "ArmorClass is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "ArmorClass must be non-negative.")]
    public int ArmorClass { get; set; }
}