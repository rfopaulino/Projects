using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.EntityFramework.Repository
{
    public class MedicoRepository : GenericRepository<Medico>
    {
        private readonly Model _model;

        public MedicoRepository(Model model)
            :base(model)
        {
            _model = model;
        }

        public List<Medico> GetByIds(List<int> ids)
        {
            return _model.Medico
                .Where(x => ids.Contains(x.Id))
                .ToList();
        }
    }
}
