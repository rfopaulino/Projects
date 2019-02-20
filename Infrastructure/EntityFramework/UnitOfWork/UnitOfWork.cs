using Infrastructure.EntityFramework.Repository;

namespace Infrastructure.UnitOfWork
{
    public class UnitOfWork : BaseUnitOfWork
    {
        private UsuarioRepository _usuarioRepository;
        private MedicoRepository _medicoRepository;
        private PacienteRepository _pacienteRepository;
        private AgendamentoRepository _agendaRepository;

        public UnitOfWork(string connectionString)
            :base(connectionString)
        {
            SetRepositories();
        }

        private void SetRepositories()
        {
            _usuarioRepository = new UsuarioRepository(Context);
            _medicoRepository = new MedicoRepository(Context);
            _pacienteRepository = new PacienteRepository(Context);
            _agendaRepository = new AgendamentoRepository(Context);
        }

        public UsuarioRepository UsuarioRepository { get { return _usuarioRepository; } }

        public MedicoRepository MedicoRepository { get { return _medicoRepository; } }

        public PacienteRepository PacienteRepository { get { return _pacienteRepository; } }

        public AgendamentoRepository AgendamentoRepository { get { return _agendaRepository; } }
    }
}
