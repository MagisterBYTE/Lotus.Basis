using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryResourceFile
	*@{*/
    /// <summary>
    /// Интерфейс сервиса для работы с файлами.
    /// </summary>
    public interface ILotusResourceFileService
    {
        /// <summary>
        /// Создание/загрузка файла по указанным данным.
        /// </summary>
        /// <param name="fileCreate">Параметры для создания файла.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Файл.</returns>
        Task<Response<FileDto>> CreateAsync(FileCreateRequest fileCreate, CancellationToken token);

        /// <summary>
        /// Получение указанного файла в формате Base64.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Файл в формате Base64.</returns>
        Task<Response<FileBase64Dto>> GetBase64Async(Guid id, CancellationToken token);

        /// <summary>
        /// Получение списка файлов в формате Base64.
        /// </summary>
        /// <param name="filesRequest">Параметры получения списка.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Cписок файлов в формате Base64.</returns>
        Task<ResponsePage<FileBase64Dto>> GetAllBase64Async(FilesRequest filesRequest, CancellationToken token);

        /// <summary>
        /// Удаление файла.
        /// </summary>
        /// <param name="id">Идентификатор файла.</param>
        /// <param name="token">Токен отмены.</param>
        /// <returns>Статус успешности.</returns>
        Task<Response> DeleteAsync(Guid id, CancellationToken token);
    }
    /**@}*/
}