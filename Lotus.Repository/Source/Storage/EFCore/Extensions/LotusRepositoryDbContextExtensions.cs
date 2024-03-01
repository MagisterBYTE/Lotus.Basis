using System;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryEFCore
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы расширения для работы с ModelBuilder.
    /// </summary>
    public static class XDbContextExtensions
    {
        /// <summary>
        /// Сохранение в базу данных.
        /// </summary>
        /// <param name="dbContext">Контекст БД.</param>
        /// <param name="token">Токен отмены.</param>
        /// <param name="desc">Описание ошибки.</param>
        /// <returns>Задача.</returns>
        public static async Task TrySaveChangesAsync(this DbContext dbContext, string desc = "",
            CancellationToken token = default)
        {
            var status = await dbContext.SaveChangesAsync(token);

            if (status == default)
            {
                if (string.IsNullOrEmpty(desc))
                {
                    throw new Exception("При сохранении БД произошла ошибка");
                }
                else
                {
                    throw new Exception(desc);
                }
            }
        }
    }
    /**@}*/
}