using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("Agendamento")]
    public class Agendamento
    {
        public Agendamento()
        {
            DataCriacao = DateTime.Now;
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("data")]
        public DateTime Data { get; set; }

        [Column("id_medico")]
        public int IdMedico { get; set; }

        [Column("id_paciente")]
        public int IdPaciente { get; set; }

        [Column("datacriacao")]
        public DateTime DataCriacao { get; set; }

        public virtual Medico Medico { get; set; }

        public virtual Paciente Paciente { get; set; }
    }
}
