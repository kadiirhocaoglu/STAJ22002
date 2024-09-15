using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace HST.DataAccess.Concrete
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=HSTDB;User Id=SA;Password=reallyStrongPwd123;TrustServerCertificate=True;");

            return new Context(optionsBuilder.Options);
        }
    }
}
