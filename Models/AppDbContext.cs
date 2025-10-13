using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace GestorTarefas.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Tarefa> Tarefas { get; set; }
    }
}