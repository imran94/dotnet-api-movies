using System.Net.Http.Headers;
using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers;

[ApiController]
[Route("[controller]")]
public class ActorController : ControllerBase
{
    private readonly AppDbContext _context;

    public ActorController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Actor>>> GetAll()
    {
        var actor = await _context.Actors.AsNoTracking().ToListAsync();
        return Ok(actor);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Actor>> GetById(int id)
    {
        var actor = await _context.Actors.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);
        if (actor is null) return NotFound();
        return Ok(actor);
    }

    [HttpPost]
    public async Task<ActionResult<Movie>> Create(Actor actor)
    {
        actor.Id = 0;
        _context.Actors.Add(actor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = actor.Id }, actor);
    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<Movie>> Update(int id, Actor updatedActor)
    {
        var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);
        if (actor is null) return NotFound();
        actor.Name = updatedActor.Name;

        await _context.SaveChangesAsync();
        return Ok(actor);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var actor = await _context.Actors.FirstOrDefaultAsync(a => a.Id == id);
        if (actor is null) return NotFound();
        _context.Actors.Remove(actor);
        return NoContent();
    }
}
