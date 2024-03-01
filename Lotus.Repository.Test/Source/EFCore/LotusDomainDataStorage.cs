namespace Lotus.Repository
{
    /// <summary>
    /// Контекст базы данных для тестирования.
    /// </summary>
    public class DomainDataStorage : DataStorageContextDb<DomainContext>
    {
        public DomainDataStorage(DomainContext context)
            : base(context)
        {
        }
    }
}