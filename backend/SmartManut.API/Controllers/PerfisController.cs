using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartManut.API.Data;
using SmartManut.API.Models;

namespace SmartManut.API.Controllers;

[Route("api/perfis")]
[ApiController]
public class PerfisController : ControllerBase
{
    private readonly AppDbContext _context;
    public PerfisController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Perfil>>> Get() => await _context.Perfis.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Perfil>> Post(Perfil item)
    {
        _context.Perfis.Add(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Perfis.FindAsync(id);
        if (item == null) return NotFound();
        _context.Perfis.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
