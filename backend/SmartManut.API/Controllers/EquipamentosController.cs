using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartManut.API.Data;
using SmartManut.API.Models;

namespace SmartManut.API.Controllers;

[Route("api/equipamentos")]
[ApiController]
public class EquipamentosController : ControllerBase
{
    private readonly AppDbContext _context;
    public EquipamentosController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Equipamento>>> Get() => await _context.Equipamentos.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Equipamento>> Post(Equipamento item)
    {
        _context.Equipamentos.Add(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Equipamentos.FindAsync(id);
        if (item == null) return NotFound();
        _context.Equipamentos.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
