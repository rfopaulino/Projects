using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Map
{
    public class PacienteMap : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder
                .HasMany(x => x.Agenda)
                .WithOne(x => x.Paciente)
                .HasForeignKey(x => x.IdPaciente);
        }
    }
}
