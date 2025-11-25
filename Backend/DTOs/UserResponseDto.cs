using System;

namespace PruebaTecnicaUniversal.DTOs
{
    /// <summary>
    /// DTO que representa la respuesta al realizar login o registro de un usuario.
    /// Contiene información del usuario y su token JWT.
    /// </summary>
    public class UserResponseDto
    {
        /// <summary>
        /// Identificador único del usuario.
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario.
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Token JWT generado al iniciar sesión o registrarse.
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}
