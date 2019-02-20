using System;

namespace Business.Dto.Agendamento
{
    public class AgendamentoInsertDto
    {
        public string Medico { get; set; }
        public string Paciente { get; set; }
        public DateTime Data { get; set; }
        public string Hora { get; set; }
    }
}
