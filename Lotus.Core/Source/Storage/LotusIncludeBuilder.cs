using System;
using System.Linq.Expressions;

namespace Lotus.Core
{
    /** \addtogroup CoreStorage
    *@{*/
    /// <summary>
    /// Построитель включений навигационных свойств (аналог EF Include) без зависимости от EF Core.
    /// </summary>
    /// <typeparam name="TEntity">Тип корневой сущности.</typeparam>
    public interface ILotusIncludeBuilder<TEntity> where TEntity : class
    {
        /// <summary>
        /// Включить навигационное свойство в выборку / гидратацию.
        /// </summary>
        /// <typeparam name="TProperty">Тип навигации (ссылка или коллекция).</typeparam>
        /// <param name="navigation">Выражение навигации, например <c>u => u.Role</c>.</param>
        /// <returns>Текущий построитель.</returns>
        ILotusIncludeBuilder<TEntity> Include<TProperty>(Expression<Func<TEntity, TProperty>> navigation);
    }
    /**@}*/
}
