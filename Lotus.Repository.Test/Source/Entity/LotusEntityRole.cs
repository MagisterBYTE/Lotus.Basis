using Lotus.Core;
//---------------------------------------------------------------------------------------------------------------------

namespace Lotus.Repository
{
    /// <summary>
    /// Роль.
    /// </summary>
    public class Role : EntityDb<int>
    {
        public string? Name { get; set; }

        public List<Permission> Permissions { get; set; } = new List<Permission>();

        public Role(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Role(int id, string name, params Permission[] permissions)
        {
            Id = id;
            Name = name;
            Permissions.AddRange(permissions);
        }
    }
}