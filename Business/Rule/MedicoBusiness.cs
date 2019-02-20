using Business.Dto.Agendamento;
using Business.Dto.Medico;
using Common;
using Domain.Entity;
using Infrastructure.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Rule
{
    public class MedicoBusiness
    {
        private readonly UnitOfWork _uow;

        public MedicoBusiness(UnitOfWork uow)
        {
            _uow = uow;
        }

        private bool ExistsMedico(int id)
        {
            return _uow.MedicoRepository.GetById(id) != null;
        }

        private void ExcluirMedicos(List<Medico> medicos)
        {
            foreach (var medico in medicos)
            {
                _uow.MedicoRepository.Delete(medico);
            }
            _uow.SaveChanges();
        }

        public MedicoGetIdDto GetById(int id)
        {
            var db = _uow.MedicoRepository.GetById(id);
            if(db != null)
            {
                return new MedicoGetIdDto
                {
                    Id = db.Id,
                    Nome = db.Nome,
                    Sobrenome = db.Crm,
                    Crm = db.Crm,
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

        public Medico Insert(MedicoInsertDto dto)
        {
            ValidateDocumentHelper.ValidateCpf(dto.Cpf);

            var db = new Medico
            {
                Nome = dto.Nome,
                Sobrenome = dto.Sobrenome,
                Crm = dto.Crm,
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

            _uow.MedicoRepository.Add(db);
            _uow.SaveChanges();

            return db;
        }

        public void Update(int id, MedicoUpdateDto dto)
        {
            if (id != dto.Id)
                throw new Exception(Messages.InconsistencyRequest);

            ValidateDocumentHelper.ValidateCpf(dto.Cpf);

            if (ExistsMedico(id))
            {
                var db = _uow.MedicoRepository.GetById(id);
                if (db != null)
                {
                    db.Nome = dto.Nome;
                    db.Sobrenome = dto.Sobrenome;
                    db.Crm = dto.Crm;
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

                    _uow.MedicoRepository.Edit(db);
                    _uow.SaveChanges();
                }
                else
                    throw new Exception(Messages.NotExistsDoctor);
            }
        }

        public List<MedicoGridDto> Grid()
        {
            var query = _uow.MedicoRepository.GetAll();
            return query.Select(x => new MedicoGridDto
            {
                Id = x.Id,
                Nome = x.Nome.Trim(),
                Sobrenome = x.Sobrenome.Trim()
            }).ToList();
        }

        public string[] Busca(string filter)
        {
            var query = _uow.MedicoRepository.GetAll();
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
            return _uow.MedicoRepository.GetAll().Where(x => (x.Id + "-" + x.Nome + " " + x.Sobrenome) == viewSuggestion).Select(x => x.Id).FirstOrDefault();
        }

        public void ExcluirSelecionados(List<int> ids)
        {
            if (ids == null || ids.Count == 0)
                throw new Exception(Messages.InconsistencyRequest);

            var medicos = _uow.MedicoRepository.GetByIds(ids);
            ExcluirMedicos(medicos);
        }
    }
}
