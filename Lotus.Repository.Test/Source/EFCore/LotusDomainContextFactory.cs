using Microsoft.EntityFrameworkCore.Design;

namespace Lotus.Repository
{
    /// <summary>
    /// Фабрика для создания контекста базы данных.
    /// </summary>
    public class DomainContextFactory : IDesignTimeDbContextFactory<DomainContext>
    {
        public DomainContext CreateDbContext(string[] args) => DomainContext.Create(XConnection.ConnectionString);
    }
}