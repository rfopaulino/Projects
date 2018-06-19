using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity
{
    [Table("Paciente")]
    public class Paciente
    {
        public Paciente()
        {
            DataCriacao = DateTime.Now;
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("nome")]
        public string Nome { get; set; }

        [Column("sobrenome")]
        public string Sobrenome { get; set; }

        [Column("sexo")]
        public int Sexo { get; set; }

        [Column("rg")]
        public string Rg { get; set; }

        [Column("cpf")]
        public string Cpf { get; set; }

        [Column("cep")]
        public string Cep { get; set; }

        [Column("logradouro")]
        public string Logradouro { get; set; }

        [Column("bairro")]
        public string Bairro { get; set; }

        [Column("numero")]
        public int Numero { get; set; }

        [Column("nacionalidade")]
        public string Nacionalidade { get; set; }

        [Column("telefone")]
        public string Telefone { get; set; }

        [Column("celular")]
        public string Celular { get; set; }

        [Column("datacriacao")]
        public DateTime DataCriacao { get; set; }

        public virtual ICollection<Agendamento> Agenda { get; set; }
    }
}
