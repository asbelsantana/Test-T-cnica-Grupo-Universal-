using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PruebaTecnicaUniversal.Services
{
    /// <summary>
    /// Servicio para generar tokens JWT para autenticación de usuarios.
    /// </summary>
    public class JwtService
    {
        private readonly string _secret;
        private readonly int _expirationHours;

        /// <summary>
        /// Constructor que recibe la configuración de la aplicación.
        /// </summary>
        /// <param name="config">Configuración para obtener la clave secreta del JWT</param>
        public JwtService(IConfiguration config)
        {
            _secret = config["Jwt:Key"] ?? throw new ArgumentNullException("Jwt:Key no configurada en appsettings.json");
            _expirationHours = int.TryParse(config["Jwt:ExpirationHours"], out var hours) ? hours : 1;
        }

        /// <summary>
        /// Genera un token JWT para un usuario.
        /// </summary>
        /// <param name="userId">Id del usuario</param>
        /// <param name="email">Correo electrónico del usuario</param>
        /// <returns>Token JWT como string</returns>
        public string GenerateToken(Guid userId, string email)
        {
            // Crear la clave de seguridad a partir del secreto
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Definir claims (información incluida en el token)
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // identificador único del token
            };

            // Crear el token
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(_expirationHours),
                signingCredentials: creds
            );

            // Retornar el token como string
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
