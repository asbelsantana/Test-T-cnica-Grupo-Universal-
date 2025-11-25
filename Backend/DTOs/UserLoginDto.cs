namespace PruebaTecnicaUniversal.DTOs
{
    /// <summary>
    /// DTO para el login de usuarios.
    /// </summary>
    public class UserLoginDto
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
