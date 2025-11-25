using PruebaTecnicaUniversal.DTOs;

namespace PruebaTecnicaUniversal.Services
{
    /// <summary>
    /// Interfaz para el servicio de usuarios.
    /// Define los métodos para registro y login de usuarios.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Registra un nuevo usuario en el sistema.
        /// </summary>
        /// <param name="dto">DTO con los datos del usuario a registrar</param>
        /// <returns>Objeto UserResponseDto con los datos del usuario registrado y token JWT</returns>
        Task<UserResponseDto> Register(UserRegisterDto dto);

        /// <summary>
        /// Realiza el inicio de sesión de un usuario.
        /// </summary>
        /// <param name="dto">DTO con credenciales del usuario</param>
        /// <returns>Objeto UserResponseDto con los datos del usuario y token JWT</returns>
        Task<UserResponseDto> Login(UserLoginDto dto);
    }
}
