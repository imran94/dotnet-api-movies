using System;

namespace api.Models;

public class MovieActor
{
    public int MoviesId { get; set; }
    public int ActorsId { get; set; }
    public Movie Movie { get; set; } = null!;
    public Actor Actor { get; set; } = null!;
}
