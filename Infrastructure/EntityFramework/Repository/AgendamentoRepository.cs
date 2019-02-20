using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.EntityFramework.Repository
{
    public class AgendamentoRepository : GenericRepository<Agendamento>
    {
        private readonly Model _model;

        public AgendamentoRepository(Model model)
            :base(model)
        {
            _model = model;
        }

        public override Agendamento GetById(object id)
        {
            return _model.Agendamento.AsQueryable()
                .Include(x => x.Medico)
                .Include(x => x.Paciente)
                .Where(x => x.Id == (int)id).FirstOrDefault();
        }

        public override IQueryable<Agendamento> GetAll()
        {
            return _model.Agendamento.AsQueryable()
                .Include(x => x.Medico)
                .Include(x => x.Paciente);
        }

        public List<Agendamento> GetByIds(List<int> ids)
        {
            return _model.Agendamento
                .Where(x => ids.Contains(x.Id))
                .ToList();
        }
    }
}
