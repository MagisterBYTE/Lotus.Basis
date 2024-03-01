using System.Linq;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryEFCore
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы расширения для работы с ModelBuilder.
    /// </summary>
    public static class XModelBuilderExtensions
    {
        /// <summary>
        /// Изменить поведение при удалении на <see cref="DeleteBehavior.Restrict"/>.
        /// </summary>
        /// <param name="modelBuilder">Построитель модели.</param>
        /// <returns>Построитель модели.</returns>
        public static ModelBuilder ReplaceCascadeDeleteBehaviorToRestrict(this ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
                .SelectMany(x => x.GetForeignKeys())
                .Where(x => !x.IsOwnership
                            && !HasIgnoreAnnotation(x)
                            && x.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
            {
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            }

            return modelBuilder;

            bool HasIgnoreAnnotation(IMutableForeignKey key)
            {
                return (bool?)key.FindAnnotation(XEntityTypeBuilderExtensions.IgnoreOverrideDeleteBehavior)?.Value == true;
            }
        }
    }
    /**@}*/
}