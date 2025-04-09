using Microsoft.EntityFrameworkCore;
using ToDo.Models;

namespace ToDo.DB
{
    public class ToDoContext:DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options):base(options) 
        {
                
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Tarefa> Tarefa { get; set; }
        

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Tarefa>().ToTable("Tarefa");
            
        }
    }
}
