using System.ComponentModel.DataAnnotations;

namespace api.Models;

public class Studio
{
    public int Id { get; set; }
    [Required]
    [StringLength(60, MinimumLength = 3)]
    public string Name { get; set; }

    public ICollection<Movie> Movies { get; set; } = new List<Movie>();
}
