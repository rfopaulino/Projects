using Domain.Entity;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.EntityFramework.Repository
{
    public class UsuarioRepository : GenericRepository<Usuario>
    {
        private readonly Model _model;

        public UsuarioRepository(Model model)
            :base(model)
        {
            _model = model;
        }

        public List<Usuario> GetByIds(List<string> ids)
        {
            return _model.Usuario
                .Where(x => ids.Contains(x.UsuarioLogin))
                .ToList();
        }
    }
}
