using System.Collections.Generic;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryPageInfo
	*@{*/
    /// <summary>
    /// Статический класс, реализующий вспомогательные методы для постраничного запроса данных.
    /// </summary>
    public static class XPageInfoHelpers
    {
        /// <summary>
        /// Получение массива постраничного запроса данных.
        /// </summary>
        /// <param name="totalSize">Общее количество данных.</param>
        /// <param name="pageSize">Размер страницы.</param>
        /// <returns>Массив для постраничного запроса данных.</returns>
        public static PageInfoRequest[] ToPageInfo(int totalSize, int pageSize)
        {
            if (totalSize <= pageSize)
            {
                return new[] { new PageInfoRequest { PageNumber = 0, PageSize = totalSize, } };
            }

            var countPage = totalSize / pageSize;
            var residue = totalSize % pageSize;

            var pageInfoRequests = new List<PageInfoRequest>();

            for (var i = 0; i < countPage; i++)
            {
                var pageInfoRequest = new PageInfoRequest()
                {
                    PageNumber = i * pageSize,
                    PageSize = pageSize,
                };
                pageInfoRequests.Add(pageInfoRequest);
            }

            if (residue > 0)
            {
                var pageInfoRequest = new PageInfoRequest()
                {
                    PageNumber = countPage * pageSize,
                    PageSize = residue,
                };

                pageInfoRequests.Add(pageInfoRequest);
            }

            return pageInfoRequests.ToArray();
        }
    }
    /**@}*/
}