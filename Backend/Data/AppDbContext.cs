using Microsoft.EntityFrameworkCore;
using PruebaTecnicaUniversal.Models;

namespace PruebaTecnicaUniversal.Data
{
    /// <summary>
    /// Contexto principal de la base de datos de la aplicación.
    /// Permite la configuración de las entidades y la conexión con la base de datos.
    /// </summary>
    public class AppDbContext : DbContext
    {
        /// <summary>
        /// Constructor que recibe las opciones de DbContext.
        /// </summary>
        /// <param name="options">Opciones de configuración del DbContext</param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// DbSet que representa la tabla de usuarios.
        /// </summary>
        public DbSet<User> Users => Set<User>();      
    }
}
