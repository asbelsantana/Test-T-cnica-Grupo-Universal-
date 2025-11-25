using BCrypt.Net;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaUniversal.Data;
using PruebaTecnicaUniversal.DTOs;
using PruebaTecnicaUniversal.Helpers;
using PruebaTecnicaUniversal.Models;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PruebaTecnicaUniversal.Services
{
    /// <summary>
    /// Servicio para manejar la lógica de usuarios: registro y login.
    /// </summary>
    public class UserService
    {
        private readonly AppDbContext _context;
        private readonly JwtService _jwt;

        public UserService(AppDbContext context, JwtService jwt)
        {
            _context = context;
            _jwt = jwt;
        }

        /// <summary>
        /// Registra un nuevo usuario en la base de datos.
        /// </summary>
        /// <param name="request">DTO con los datos del usuario</param>
        /// <returns>UserResponseDto con usuario y token JWT</returns>
        public async Task<UserResponseDto> Register(UserRegisterDto request)
        {
            // Validaciones internas (respaldo si no se usa FluentValidation)
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new Exception("El nombre no puede estar vacío.");

            if (!Regex.IsMatch(request.Email, RegexSettings.EmailPattern))
                throw new Exception("Correo inválido.");

            if (!Regex.IsMatch(request.Password, RegexSettings.PasswordPattern))
                throw new Exception("La contraseña no cumple con los requisitos.");

            if (await _context.Users.AnyAsync(u => u.Email == request.Email))
                throw new Exception("El correo ya está registrado.");

            // Crear usuario y encriptar contraseña
            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Generar token JWT
            var token = _jwt.GenerateToken(user.Id, user.Email);

            return new UserResponseDto
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = token
            };
        }

        /// <summary>
        /// Inicia sesión de un usuario verificando email y contraseña.
        /// </summary>
        /// <param name="request">DTO con email y contraseña</param>
        /// <returns>UserResponseDto con usuario y token JWT</returns>
        public async Task<UserResponseDto> Login(UserLoginDto request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new Exception("Correo o contraseña incorrectos.");

            // Generar token JWT
            var token = _jwt.GenerateToken(user.Id, user.Email);

            return new UserResponseDto
            {
                UserId = user.Id,
                Name = user.Name,
                Email = user.Email,
                Token = token
            };
        }
    }
}
