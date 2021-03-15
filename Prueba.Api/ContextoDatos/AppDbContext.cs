using Microsoft.EntityFrameworkCore;
using Prueba.Api.Entidades;

namespace Prueba.Api.ContextoDatos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
    }
}
