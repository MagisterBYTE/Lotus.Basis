namespace Lotus.Repository
{
    /// <summary>
    /// Класс для формирования подключения к тестовой базе данных.
    /// </summary>
    public static class XConnection
    {
        /// <summary>
        /// Строка для подключения к тестовой базе данных.
        /// </summary>
        public const string ConnectionString = "Host=localhost;Database=test_date;Username=postgres;Password=1234";
    }
}