using System.Net.Http.Headers;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

public class MovieDto
{
    public int Id { get; set; }
    public string? Title { get; set; } = string.Empty;
    public DateOnly ReleaseDate { get; set; }
    public string? Genre { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Studio Studio { get; set; } = null!;
    public int StudioId { get; set; }
    public List<ActorDto> Actors { get; set; } = new();
}
public class ActorDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
}

[ApiController]
[Route("[controller]")]
public class MovieController : ControllerBase
{
    private readonly AppDbContext _context;

    public MovieController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Movie>>> GetAll()
    {
        var movies = await _context.Movies.Select(m => new MovieDto
        {
            Id = m.Id,
            Title = m.Title,
            ReleaseDate = m.ReleaseDate,
            Genre = m.Genre,
            Price = m.Price,
            Studio = m.Studio,
            StudioId = m.StudioId,
            Actors = m.Actors.Select(a => new ActorDto
            {
                Id = a.Id,
                Name = a.Name ?? ""
            }).ToList()
        })
        .ToListAsync();
        return Ok(movies);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Movie>> GetById(int id)
    {
        var movie = await _context.Movies.AsNoTracking().Include(m => m.Actors).FirstOrDefaultAsync(m => m.Id == id);
        if (movie is null) return NotFound();
        return Ok(movie);
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> Create(Movie movie)
    {
        movie.Id = 0;
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = movie.Id }, movie);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Movie>> Update(int id, Movie updatedMovie)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        if (movie is null) return NotFound();
        movie.Title = updatedMovie.Title;
        movie.ReleaseDate = updatedMovie.ReleaseDate;
        movie.Genre = updatedMovie.Genre;
        movie.Price = updatedMovie.Price;

        await _context.SaveChangesAsync();
        return Ok(movie);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
        if (movie is null) return NotFound();
        _context.Movies.Remove(movie);
        return NoContent();
    }
}
