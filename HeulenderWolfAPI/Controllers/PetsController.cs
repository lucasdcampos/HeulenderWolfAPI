using HeulenderWolfAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HeulenderWolfAPI.Controllers;

[ApiController]
[Route("pets")]
public class PetsController : ControllerBase
{
    private readonly AppDbContext _context;

    public PetsController(AppDbContext context)
    {
        _context = context;
    }

    [Authorize(Roles = "Organizacao")]
    [HttpPost]
    public async Task<IActionResult> CreatePet([FromBody] Pet pet)
    {
        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPet), new { id = pet.ID }, pet);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPet(Guid id)
    {
        var pet = await _context.Pets.FindAsync(id);
        if (pet == null) return NotFound();
        return Ok(pet);
    }
}
