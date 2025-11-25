using System;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaUniversal.Models
{
    /// <summary>
    /// Entidad que representa un usuario en la base de datos.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Identificador único del usuario.
        /// Se genera automáticamente al crear el objeto.
        /// </summary>
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Nombre completo del usuario.
        /// </summary>
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Correo electrónico del usuario.
        /// Debe ser único.
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña encriptada usando BCrypt.
        /// Nunca almacenar contraseñas en texto plano.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}
