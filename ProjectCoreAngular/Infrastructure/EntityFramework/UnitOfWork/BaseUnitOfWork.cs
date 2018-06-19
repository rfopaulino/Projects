using Microsoft.EntityFrameworkCore;

namespace Infrastructure.UnitOfWork
{
    public class BaseUnitOfWork
    {
        protected Model Context { get; set; }

        public BaseUnitOfWork(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Model>();
            optionsBuilder.UseSqlServer(connectionString);
            Context = new Model(optionsBuilder.Options);
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }
    }
}
