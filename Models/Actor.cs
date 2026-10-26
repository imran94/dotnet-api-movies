using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class Actor
{
    public int Id { get; set; }
    [Required]
    [StringLength(60, MinimumLength = 3)]
    public string? Name { get; set; }

    public List<Movie> Movies { get; } = [];
}
