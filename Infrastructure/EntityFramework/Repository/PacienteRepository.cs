using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.EntityFramework.Repository
{
    public class PacienteRepository : GenericRepository<Paciente>
    {
        private readonly Model _model;

        public PacienteRepository(Model model)
            :base(model)
        {
            _model = model;
        }

        public List<Paciente> GetByIds(List<int> ids)
        {
            return _model.Paciente
                .Where(x => ids.Contains(x.Id))
                .ToList();
        }
    }
}
