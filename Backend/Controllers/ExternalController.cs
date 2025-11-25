using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace PruebaTecnicaUniversal.Controllers
{
    /// <summary>
    /// Controlador para interactuar con APIs externas (ej: JSONPlaceholder).
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Requiere autenticación
    public class ExternalController : ControllerBase
    {
        private readonly HttpClient _http;

        /// <summary>
        /// Constructor que recibe un HttpClient a través de IHttpClientFactory.
        /// </summary>
        /// <param name="httpClientFactory">Fábrica de HttpClient inyectada por DI.</param>
        public ExternalController(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient();
        }

        /// <summary>
        /// Obtiene todos los posts de la API externa.
        /// GET: api/external/posts
        /// </summary>
        /// <returns>Lista de posts deserializada.</returns>
        [HttpGet("posts")]
        public async Task<IActionResult> GetPosts()
        {
            try
            {
                var response = await _http.GetAsync("https://jsonplaceholder.typicode.com/posts");

                // Asegurarse que la respuesta fue exitosa
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                // Deserializar a objeto dinámico para retornar
                var posts = JsonSerializer.Deserialize<object>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return Ok(posts);
            }
            catch (HttpRequestException ex)
            {
                // Manejo de errores de HTTP
                return StatusCode(500, new { message = "Error al consumir la API externa.", detail = ex.Message });
            }
        }

        /// <summary>
        /// Crea un nuevo post en la API externa.
        /// POST: api/external/posts
        /// </summary>
        /// <param name="post">Datos del post a crear.</param>
        /// <returns>Post creado con la respuesta de la API externa.</returns>
        [HttpPost("posts")]
        public async Task<IActionResult> CreatePost([FromBody] PostDto post)
        {
            if (post == null)
                return BadRequest(new { message = "El cuerpo de la solicitud no puede estar vacío." });

            try
            {
                // Convertir el objeto a JSON
                var jsonString = JsonSerializer.Serialize(post);
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                // Enviar la petición POST
                var response = await _http.PostAsync("https://jsonplaceholder.typicode.com/posts", content);
                var json = await response.Content.ReadAsStringAsync();

                // Retornar error si la API externa falla
                if (!response.IsSuccessStatusCode)
                    return StatusCode((int)response.StatusCode, new { message = "Error de la API externa", detail = json });

                // Deserializar la respuesta
                var result = JsonSerializer.Deserialize<object>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return Ok(result);
            }
            catch (HttpRequestException ex)
            {
                return StatusCode(500, new { message = "Error al consumir la API externa.", detail = ex.Message });
            }
        }
    }
}
