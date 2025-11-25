using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaUniversal.DTOs;
using PruebaTecnicaUniversal.Services;
using System;
using System.Threading.Tasks;

namespace PruebaTecnicaUniversal.Controllers
{
    /// <summary>
    /// Controlador para la gestión de usuarios: registro e inicio de sesión.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        /// <summary>
        /// Constructor que recibe UserService mediante inyección de dependencias.
        /// </summary>
        /// <param name="userService">Servicio de usuario</param>
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registra un nuevo usuario.
        /// POST: api/user/register
        /// </summary>
        /// <param name="request">Datos del usuario a registrar</param>
        /// <returns>Resultado del registro o error</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserRegisterDto request)
        {
            if (request == null)
                return BadRequest(new { error = "Los datos de registro son obligatorios." });

            try
            {
                var result = await _userService.Register(request);
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                // Errores esperados (usuario ya existe, datos inválidos, etc.)
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                // Errores inesperados
                return StatusCode(500, new { error = "Error interno del servidor", detail = ex.Message });
            }
        }

        /// <summary>
        /// Inicia sesión de un usuario.
        /// POST: api/user/login
        /// </summary>
        /// <param name="request">Credenciales del usuario</param>
        /// <returns>Token JWT y datos del usuario si las credenciales son correctas</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto request)
        {
            if (request == null)
                return BadRequest(new { error = "Las credenciales son obligatorias." });

            try
            {
                // Login devuelve un UserResponseDto que contiene el token y datos del usuario
                UserResponseDto userResponse = await _userService.Login(request);

                if (userResponse == null || string.IsNullOrEmpty(userResponse.Token))
                    return Unauthorized(new { error = "Usuario o contraseña incorrectos." });

                // Retornar solo el token o todo el objeto según tu necesidad
                return Ok(new
                {
                    token = userResponse.Token,
                    user = new
                    {
                        userResponse.UserId,
                        userResponse.Name,
                        userResponse.Email
                    }
                });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor", detail = ex.Message });
            }
        }
    }
}
