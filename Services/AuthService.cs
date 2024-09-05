

using dotnetdev_assessment.Models.Entities;
using dotnetdev_assessment.Models.ViewModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace dotnetdev_assessment.Services
{
    public class AuthService(ApplicationDbContext context, IConfiguration config) : IAuthService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IConfiguration _config = config;

        public string? Authenticate(AuthLoginViewModel loginViewModel)
        {
            Employee? employee = _context.Employees.FirstOrDefault(x => x.Username == loginViewModel.Username);
            if (employee == null)
                return null;
            if (!BCrypt.Net.BCrypt.Verify(loginViewModel.Password, employee.PasswordHash))
                return null;
            return GenerateJWTToken(employee.Id, employee.IsAdmin ? "Admin" : "User");
        }

        public string? GenerateJWTToken(int id, string role)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, id.ToString()),
                new Claim(ClaimTypes.Name, id.ToString()),
                new Claim("Id", id.ToString()),
                new Claim(ClaimTypes.Role, role) // sets the role
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToInt32(jwtSettings["Exp"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

}
