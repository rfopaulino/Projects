using Domain.Entity;
using Infrastructure.Map;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class Model : DbContext
    {
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Medico> Medico { get; set; }
        public virtual DbSet<Paciente> Paciente { get; set; }
        public virtual DbSet<Agendamento> Agendamento { get; set; }

        public Model(DbContextOptions<Model> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
            modelBuilder.ApplyConfiguration(new MedicoMap());
            modelBuilder.ApplyConfiguration(new PacienteMap());
            modelBuilder.ApplyConfiguration(new AgendamentoMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}
