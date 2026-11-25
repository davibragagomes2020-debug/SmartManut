using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartManut.API.Data;
using SmartManut.API.Models;

namespace SmartManut.API.Controllers;

[Route("api/setores")]
[ApiController]
public class SetoresController : ControllerBase
{
    private readonly AppDbContext _context;
    public SetoresController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Setor>>> Get() => await _context.Setores.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Setor>> Post(Setor item)
    {
        _context.Setores.Add(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Setores.FindAsync(id);
        if (item == null) return NotFound();
        _context.Setores.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
