using Lotus.Core;

namespace Lotus.Repository
{
    /// <summary>
    /// Разрешение.
    /// </summary>
    public class Permission : EntityDb<int>
    {
        public string? Name { get; set; }

        public List<Role> Roles { get; set; } = new List<Role>();

        public Permission(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}