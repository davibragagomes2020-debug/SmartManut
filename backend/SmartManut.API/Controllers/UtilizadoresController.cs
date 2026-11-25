using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartManut.API.Data;
using SmartManut.API.Models;

namespace SmartManut.API.Controllers;

[Route("api/utilizadores")]
[ApiController]
public class UtilizadoresController : ControllerBase
{
    private readonly AppDbContext _context;
    public UtilizadoresController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Utilizador>>> Get() => await _context.Utilizadores.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Utilizador>> Post(Utilizador item)
    {
        _context.Utilizadores.Add(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Utilizadores.FindAsync(id);
        if (item == null) return NotFound();
        _context.Utilizadores.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
