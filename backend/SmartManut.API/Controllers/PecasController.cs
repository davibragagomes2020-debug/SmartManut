using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartManut.API.Data;
using SmartManut.API.Models;

namespace SmartManut.API.Controllers;

[Route("api/pecas")]
[ApiController]
public class PecasController : ControllerBase
{
    private readonly AppDbContext _context;
    public PecasController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Peca>>> Get() => await _context.Pecas.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Peca>> Post(Peca item)
    {
        _context.Pecas.Add(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Pecas.FindAsync(id);
        if (item == null) return NotFound();
        _context.Pecas.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
