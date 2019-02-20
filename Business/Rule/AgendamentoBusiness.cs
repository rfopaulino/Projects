using Business.Dto.Agendamento;
using Domain.Entity;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Rule
{
    public class AgendamentoBusiness
    {
        private readonly UnitOfWork _uow;

        public AgendamentoBusiness(UnitOfWork uow)
        {
            _uow = uow;
        }

        private bool ExistsAgendamento(int id)
        {
            return _uow.AgendamentoRepository.GetById(id) != null;
        }

        private void ExcluirAgendamentos(List<Agendamento> agendamentos)
        {
            foreach (var agendamento in agendamentos)
            {
                _uow.AgendamentoRepository.Delete(agendamento);
            }
            _uow.SaveChanges();
        }

        public AgendamentoGetIdDto GetById(int id)
        {
            var db = _uow.AgendamentoRepository.GetById(id);
            if(db != null)
            {
                return new AgendamentoGetIdDto
                {
                    Id = db.Id,
                    Medico = db.Medico.Id + "-" + db.Medico.Nome + " " + db.Medico.Sobrenome,
                    Paciente = db.Paciente.Id + "-" + db.Paciente.Nome + " " + db.Paciente.Sobrenome,
                    Data = db.Data,
                    Hora = db.Data.ToString("dd:hh")
                };
            }
            else
                throw new Exception(Messages.NotExistsUser);
        }

        public Agendamento Insert(AgendamentoInsertDto dto)
        {
            MedicoBusiness medicoBusiness = new MedicoBusiness(_uow);
            PacienteBusiness pacienteBusiness = new PacienteBusiness(_uow);
            int idMedico = medicoBusiness.GetIdByViewSuggestion(dto.Medico);
            int idPaciente = pacienteBusiness.GetIdByViewSuggestion(dto.Paciente);
            if (idMedico == 0)
                throw new Exception("Preencha um Médico válido.");
            if (idPaciente == 0)
                throw new Exception("Preencha um Paciente válido.");

            var db = new Agendamento
            {
                IdMedico = idMedico,
                IdPaciente = idPaciente,
                Data = new DateTime(dto.Data.Year, dto.Data.Month, dto.Data.Day, Convert.ToInt32(dto.Hora.Split(':')[0]), Convert.ToInt32(dto.Hora.Split(':')[1]), 0)
            };

            _uow.AgendamentoRepository.Add(db);
            _uow.SaveChanges();

            return db;
        }

        public void Update(int id, AgendamentoUpdateDto dto)
        {
            MedicoBusiness medicoBusiness = new MedicoBusiness(_uow);
            PacienteBusiness pacienteBusiness = new PacienteBusiness(_uow);
            int idMedico = medicoBusiness.GetIdByViewSuggestion(dto.Medico);
            int idPaciente = pacienteBusiness.GetIdByViewSuggestion(dto.Paciente);
            if (idMedico == 0)
                throw new Exception("Preencha um Médico válido.");
            if (idPaciente == 0)
                throw new Exception("Preencha um Paciente válido.");

            if(ExistsAgendamento(id))
            {
                var db = _uow.AgendamentoRepository.GetById(id);

                db.IdMedico = idMedico;
                db.IdPaciente = idPaciente;
                db.Data = new DateTime(dto.Data.Year, dto.Data.Month, dto.Data.Day, Convert.ToInt32(dto.Hora.Split(':')[0]), Convert.ToInt32(dto.Hora.Split(':')[1]), 0);

                _uow.AgendamentoRepository.Edit(db);
                _uow.SaveChanges();
            }
            else
                throw new Exception(Messages.NotExistsUser);
        }

        public List<AgendamentoGridDto> Grid()
        {
            var query = _uow.AgendamentoRepository.GetAll();
            return query.Select(x => new AgendamentoGridDto
            {
                Id = x.Id,
                Medico = x.Medico.Nome,
                Paciente = x.Paciente.Nome,
                Data = x.Data.ToString("dd/MM/yyyy"),
                Hora = x.Data.ToString("hh:mm")
            }).ToList();
        }

        public void ExcluirSelecionados(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                throw new Exception(Messages.InconsistencyRequest);

            var agendamentos = _uow.AgendamentoRepository.GetByIds(ids);
            ExcluirAgendamentos(agendamentos);
        }
    }
}
