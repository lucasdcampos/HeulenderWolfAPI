using HeulenderWolfAPI.DTOs;
using HeulenderWolfAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeulenderWolfAPI.Controllers;

[ApiController]
[Route("admins")]
public class AdminsController : ControllerBase
{
    private readonly AppDbContext _context;

    public AdminsController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdmin([FromBody] AdminCreateRequest request)
    {
        var admin = new Admin
        {
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        _context.Admins.Add(admin);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAdmin), new { id = admin.ID }, admin);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAllAdmins()
    {
        var admins = await _context.Admins.ToListAsync();
        return Ok(admins);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetAdmin(Guid id)
    {
        var admin = await _context.Admins.FindAsync(id);
        if (admin == null) return NotFound();
        return Ok(admin);
    }

    [Authorize(Roles = "Admin")]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAdmin(Guid id, [FromBody] Admin updatedAdmin)
    {
        var admin = await _context.Admins.FindAsync(id);
        if (admin == null) return NotFound();

        admin.Email = updatedAdmin.Email;
        admin.PasswordHash = updatedAdmin.PasswordHash;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAdmin(Guid id)
    {
        var admin = await _context.Admins.FindAsync(id);
        if (admin == null) return NotFound();

        _context.Admins.Remove(admin);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
