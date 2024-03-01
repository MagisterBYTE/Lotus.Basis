namespace Lotus.Repository
{
    /// <summary>
    /// Контекст базы данных для тестирования.
    /// </summary>
    public class DomainContextFixture
    {
        private static readonly object _lock = new();
        private static bool _databaseInitialized;

        public DomainContextFixture()
        {
            lock (_lock)
            {
                if (!_databaseInitialized)
                {
                    using (var context = CreateContext())
                    {
                        context.Database.EnsureDeleted();
                        context.Database.EnsureCreated();

                        context.Initialize();
                    }

                    _databaseInitialized = true;
                }
            }
        }

        public DomainDataStorage CreateDataStorage()
        {
            return new DomainDataStorage(CreateContext());
        }

        public DomainContext CreateContext()
        {
            return DomainContext.Create(XConnection.ConnectionString);
        }
    }
}