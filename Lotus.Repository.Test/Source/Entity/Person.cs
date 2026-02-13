using Lotus.Core;

namespace Lotus.Repository
{
    /// <summary>
    /// Пользователь.
    /// </summary>
    public class Person : EntityDb<int>
    {
        public string? Name { get; set; }
    }
}