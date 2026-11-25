using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartManut.API.Data;
using SmartManut.API.Models;

namespace SmartManut.API.Controllers;

[Route("api/historicomanutencoes")]
[ApiController]
public class HistoricoManutencoesController : ControllerBase
{
    private readonly AppDbContext _context;
    public HistoricoManutencoesController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<HistoricoManutencao>>> Get() => await _context.HistoricoManutencoes.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<HistoricoManutencao>> Post(HistoricoManutencao item)
    {
        _context.HistoricoManutencoes.Add(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.HistoricoManutencoes.FindAsync(id);
        if (item == null) return NotFound();
        _context.HistoricoManutencoes.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
