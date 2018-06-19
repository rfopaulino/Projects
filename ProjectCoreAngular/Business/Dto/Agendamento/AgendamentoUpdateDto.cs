using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Dto.Agendamento
{
    public class AgendamentoUpdateDto
    {
        public string Medico { get; set; }
        public string Paciente { get; set; }
        public DateTime Data { get; set; }
        public string Hora { get; set; }
    }
}
