using Business.Dto.Paciente;
using Common;
using Domain.Entity;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Rule
{
    public class PacienteBusiness
    {
        private readonly UnitOfWork _uow;

        public PacienteBusiness(UnitOfWork uow)
        {
            _uow = uow;
        }

        private bool ExistsPaciente(int id)
        {
            return _uow.PacienteRepository.GetById(id) != null;
        }

        private void ExcluirPacientes(List<Paciente> pacientes)
        {
            foreach (var paciente in pacientes)
            {
                _uow.PacienteRepository.Delete(paciente);
            }
            _uow.SaveChanges();
        }

        public PacienteGetIdDto GetById(int id)
        {
            var db = _uow.PacienteRepository.GetById(id);
            if(db != null)
            {
                return new PacienteGetIdDto
                {
                    Id = db.Id,
                    Nome = db.Nome,
                    Sobrenome = db.Sobrenome,
                    Sexo = db.Sexo,
                    Rg = db.Rg,
                    Cpf = db.Cpf,
                    Cep = db.Cep,
                    Logradouro = db.Logradouro,
                    Bairro = db.Bairro,
                    Numero = db.Numero,
                    Nacionalidade = db.Nacionalidade,
                    Telefone = db.Telefone,
                    Celular = db.Celular
                };
            }
            else
                throw new Exception(Messages.NotExistsUser);
        }

        public Paciente Insert(PacienteInsertDto dto)
        {
            ValidateDocumentHelper.ValidateCpf(dto.Cpf);

            var db = new Paciente
            {
                Nome = dto.Nome,
                Sobrenome = dto.Sobrenome,
                Sexo = dto.Sexo,
                Rg = dto.Rg,
                Cpf = dto.Cpf,
                Cep = dto.Cep,
                Logradouro = dto.Logradouro,
                Bairro = dto.Bairro,
                Numero = dto.Numero,
                Nacionalidade = dto.Nacionalidade,
                Telefone = dto.Telefone,
                Celular = dto.Celular
            };

            _uow.PacienteRepository.Add(db);
            _uow.SaveChanges();

            return db;
        }

        public void Update(int id, PacienteUpdateDto dto)
        {
            if (id != dto.Id)
                throw new Exception(Messages.InconsistencyRequest);

            ValidateDocumentHelper.ValidateCpf(dto.Cpf);

            if (ExistsPaciente(id))
            {
                var db = _uow.PacienteRepository.GetById(id);

                db.Nome = dto.Nome;
                db.Sobrenome = dto.Sobrenome;
                db.Sexo = dto.Sexo;
                db.Rg = dto.Rg;
                db.Cpf = dto.Cpf;
                db.Cep = dto.Cep;
                db.Logradouro = dto.Logradouro;
                db.Bairro = dto.Bairro;
                db.Numero = dto.Numero;
                db.Nacionalidade = dto.Nacionalidade;
                db.Telefone = dto.Telefone;
                db.Celular = dto.Celular;

                _uow.PacienteRepository.Edit(db);
                _uow.SaveChanges();
            }
            else
                throw new Exception(Messages.NotExistsPatient);
        }

        public List<PacienteGridDto> Grid()
        {
            var query = _uow.PacienteRepository.GetAll();
            return query.Select(x => new PacienteGridDto
            {
                Id = x.Id,
                Nome = x.Nome.Trim(),
                Sobrenome = x.Sobrenome.Trim()
            }).ToList();
        }

        public string[] Busca(string filter)
        {
            var query = _uow.PacienteRepository.GetAll();
            var resultDto = query.Select(x => new SuggestionDto
            {
                View = x.Id + "-" + x.Nome.Trim() + " " + x.Sobrenome.Trim()
            })
            .Where(x => x.View.Contains(filter))
            .Take(10).ToList();

            return FormatterHelper.Suggestion(resultDto);
        }

        public int GetIdByViewSuggestion(string viewSuggestion)
        {
            return _uow.PacienteRepository.GetAll().Where(x => (x.Id + "-" + x.Nome + " " + x.Sobrenome) == viewSuggestion).Select(x => x.Id).FirstOrDefault();
        }

        public void ExcluirSelecionados(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                throw new Exception(Messages.InconsistencyRequest);

            var pacientes = _uow.PacienteRepository.GetByIds(ids);
            ExcluirPacientes(pacientes);
        }
    }
}
