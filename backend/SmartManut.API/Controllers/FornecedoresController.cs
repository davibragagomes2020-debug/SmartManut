using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartManut.API.Data;
using SmartManut.API.Models;

namespace SmartManut.API.Controllers;

[Route("api/fornecedores")]
[ApiController]
public class FornecedoresController : ControllerBase
{
    private readonly AppDbContext _context;
    public FornecedoresController(AppDbContext context) => _context = context;

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Fornecedor>>> Get() => await _context.Fornecedores.ToListAsync();

    [HttpPost]
    public async Task<ActionResult<Fornecedor>> Post(Fornecedor item)
    {
        _context.Fornecedores.Add(item);
        await _context.SaveChangesAsync();
        return Ok(item);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _context.Fornecedores.FindAsync(id);
        if (item == null) return NotFound();
        _context.Fornecedores.Remove(item);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
