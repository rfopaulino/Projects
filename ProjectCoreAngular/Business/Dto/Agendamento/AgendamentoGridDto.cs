using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dto.Agendamento
{
    public class AgendamentoGridDto
    {
        public int Id { get; set; }
        public string Medico { get; set; }
        public string Paciente { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }
    }
}
