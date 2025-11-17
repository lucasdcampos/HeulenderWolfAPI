using HeulenderWolfAPI.Models;
using HeulenderWolfAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HeulenderWolfAPI.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly JwtService _jwt;

    public AuthController(AppDbContext context, JwtService jwt)
    {
        _context = context;
        _jwt = jwt;
    }

    [HttpPost("login/admin")]
    public async Task<IActionResult> LoginAdmin([FromBody] AdminLoginRequest request)
    {
        var admin = await _context.Admins
            .FirstOrDefaultAsync(a => a.Email == request.Email && a.PasswordHash == request.Password);

        if (admin == null || !BCrypt.Net.BCrypt.Verify(request.Password, admin.PasswordHash))
            return Unauthorized("Invalid credentials");

        var token = _jwt.GenerateToken(admin.Email, admin.ID, "Admin");

        return Ok(new { token });
    }

    [HttpPost("login/organizacao")]
    public async Task<IActionResult> LoginOrganizacao([FromBody] OrganizacaoLoginRequest request)
    {
        var org = await _context.Organizacoes
            .FirstOrDefaultAsync(o => o.CNPJ == request.CNPJ && o.PasswordHash == request.Password);

        if (org == null || !BCrypt.Net.BCrypt.Verify(request.Password, org.PasswordHash))
            return Unauthorized("Invalid credentials");

        var token = _jwt.GenerateToken(org.CNPJ, org.ID, "Organizacao");

        return Ok(new { token });
    }
}

public class AdminLoginRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class OrganizacaoLoginRequest
{
    public string CNPJ { get; set; }
    public string Password { get; set; }
}
