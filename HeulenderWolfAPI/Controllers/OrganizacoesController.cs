using HeulenderWolfAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeulenderWolfAPI.Controllers;

[ApiController]
[Route("organizacoes")]
public class OrganizacoesController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrganizacoesController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> CreateOrganizacao([FromBody] Organizacao organizacao)
    {
        organizacao.PasswordHash = BCrypt.Net.BCrypt.HashPassword(organizacao.PasswordHash);

        _context.Organizacoes.Add(organizacao);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetOrganizacao), new { id = organizacao.ID }, organizacao);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrganizacoes()
    {
        var organizacoes = await _context.Organizacoes
            .Include(o => o.Pets)
            .ToListAsync();

        return Ok(organizacoes);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrganizacao(Guid id)
    {
        var organizacao = await _context.Organizacoes
            .Include(o => o.Pets)
            .FirstOrDefaultAsync(o => o.ID == id);

        if (organizacao == null) return NotFound();
        return Ok(organizacao);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateOrganizacao(Guid id, [FromBody] Organizacao organizacaoAtualizada)
    {
        var organizacaoExistente = await _context.Organizacoes.FindAsync(id);
        if (organizacaoExistente == null) return NotFound();

        organizacaoExistente.CNPJ = organizacaoAtualizada.CNPJ;
        organizacaoExistente.PasswordHash = organizacaoAtualizada.PasswordHash;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteOrganizacao(Guid id)
    {
        var organizacao = await _context.Organizacoes.FindAsync(id);
        if (organizacao == null) return NotFound();

        _context.Organizacoes.Remove(organizacao);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{id:guid}/pets")]
    public async Task<IActionResult> GetPetsByOrganizacao(Guid id)
    {
        var exists = await _context.Organizacoes.AnyAsync(o => o.ID == id);
        if (!exists) return NotFound();

        var pets = await _context.Pets
            .Where(p => p.OrganizacaoID == id)
            .ToListAsync();

        return Ok(pets);
    }
}
