using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Lotus.Repository
{
    /** \addtogroup RepositoryResourceFile
    *@{*/
    /// <summary>
    /// Сервис для работы с файлами.
    /// </summary>
    public class ResourceFileService : ILotusResourceFileService
    {
        #region Const
        private static readonly Regex _regexReplace = new(@"^[\w/\:.-]+;base64,");
        #endregion

        #region Fields
        private readonly ILotusDataStorage _dataStorage;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="dataStorage">Интерфейс для работы с сущностями.</param>
        public ResourceFileService(ILotusDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }
        #endregion

        #region ILotusResourceFileService methods
        /// <inheritdoc/>
        public async Task<Response<FileDto>> CreateAsync(FileCreateRequest fileCreate, CancellationToken token)
        {
            if (fileCreate is FileCreateLocalRequest requestLocal)
            {
                var entity = await _dataStorage.GetByIdAsync<ResourceFile, Guid>(requestLocal.Id, token);
                if (entity == null)
                {
                    entity = new ResourceFile()
                    {
                        Name = requestLocal.Name,
                        Id = requestLocal.Id,
                        StorageType = TResourceFileStorage.Local
                    };

                    await _dataStorage.AddAsync(entity, token);
                    await _dataStorage.SaveChangesAsync(token);
                }

                var result = entity.ToFileDto();

                return Response<FileDto>.Succeed(result);
            }

            if (fileCreate is FileCreateRawRequest requestRaw)
            {
                var entity = CreateResourceFile(requestRaw.Name, requestRaw.AuthorId, requestRaw.FileTypeId, requestRaw.GroupId);
                if (requestRaw.Target == TResourceFileStorage.Database)
                {
                    ApplyDatabaseStorage(entity, requestRaw.SaveFormat, requestRaw.Data, null);
                }

                await _dataStorage.AddAsync(entity, token);
                await _dataStorage.SaveChangesAsync(token);
                return Response<FileDto>.Succeed(entity.ToFileDto());
            }

            if (fileCreate is FileCreateStreamRequest requestStream)
            {
                var entity = CreateResourceFile(requestStream.Name, requestStream.AuthorId, requestStream.FileTypeId, requestStream.GroupId);
                if (requestStream.Target == TResourceFileStorage.Database)
                {
                    using var binaryReader = new BinaryReader(requestStream.ReadStream);
                    var bytes = binaryReader.ReadBytes((int)requestStream.ReadStream.Length);
                    ApplyDatabaseStorage(entity, requestStream.SaveFormat, bytes, null);
                }

                await _dataStorage.AddAsync(entity, token);
                await _dataStorage.SaveChangesAsync(token);
                return Response<FileDto>.Succeed(entity.ToFileDto());
            }

            if (fileCreate is FileCreateBase64Request requestBase64)
            {
                var entity = CreateResourceFile(requestBase64.Name, requestBase64.AuthorId, requestBase64.FileTypeId, requestBase64.GroupId);
                if (requestBase64.Target == TResourceFileStorage.Database)
                {
                    ApplyDatabaseStorage(entity, requestBase64.SaveFormat, null, requestBase64.Data);
                }

                await _dataStorage.AddAsync(entity, token);
                await _dataStorage.SaveChangesAsync(token);
                return Response<FileDto>.Succeed(entity.ToFileDto());
            }

            return Response<FileDto>.Failed(2000, "Неизвестный запрос");
        }

        /// <inheritdoc/>
		public async Task<Response<FileBase64Dto>> GetBase64Async(Guid id, CancellationToken token)
        {
            var entity = await _dataStorage.GetByIdAsync<ResourceFile, Guid>(id, token);
            if (entity == null)
            {
                return Response<FileBase64Dto>.Failed(XResourceFileErrors.NotFound);
            }

            var result = entity.ToFileBase64Dto();

            return Response<FileBase64Dto>.Succeed(result);
        }

        /// <inheritdoc/>
		public async Task<ResponsePage<FileBase64Dto>> GetAllBase64Async(FilesRequest filesRequest,
            CancellationToken token)
        {
            var query = _dataStorage.Query<ResourceFile>();

            if (filesRequest.AuthorId.HasValue)
            {
                query = query.Where(f => f.AuthorId == filesRequest.AuthorId);
            }
            if (filesRequest.FileTypeId.HasValue)
            {
                query = query.Where(f => f.FileTypeId == filesRequest.FileTypeId);
            }
            if (filesRequest.GroupId.HasValue)
            {
                query = query.Where(f => f.GroupId == filesRequest.GroupId);
            }

            query = query.Filter(filesRequest.Filtering);

            var queryOrder = query.Sort(filesRequest.Sorting, x => x.Id);

            var result = await queryOrder.ToResponsePageAsync<ResourceFile>(filesRequest, token);

            var response = new ResponsePage<FileBase64Dto>()
            {
                Result = result.Result,
                PageInfo = result.PageInfo,
                Payload = result.Payload.Select(x => x.ToFileBase64Dto()).ToArray(),
            };

            return response;
        }

        /// <inheritdoc/>
		public async Task<Response> DeleteAsync(Guid id, CancellationToken token)
        {
            var entity = await _dataStorage.GetByIdAsync<ResourceFile, Guid>(id, token);
            if (entity == null)
            {
                return Response.Failed(XResourceFileErrors.NotFound);
            }

            _dataStorage.Remove(entity!);
            await _dataStorage.SaveChangesAsync(token);

            return Response.Succeed();
        }
        #endregion

        #region Private helpers
        private static ResourceFile CreateResourceFile(string? name, Guid? authorId, int? fileTypeId, int? groupId)
        {
            return new ResourceFile
            {
                Name = name,
                AuthorId = authorId,
                FileTypeId = fileTypeId,
                GroupId = groupId
            };
        }

        private void ApplyDatabaseStorage(ResourceFile entity, TResourceFileSaveFormat saveFormat, byte[]? rawBytes, string? base64String)
        {
            entity.StorageType = TResourceFileStorage.Database;
            entity.SaveFormat = saveFormat;

            switch (saveFormat)
            {
                case TResourceFileSaveFormat.Base64:
                    if (rawBytes != null)
                    {
                        entity.LoadPath = Convert.ToBase64String(rawBytes);
                        entity.SizeInBytes = rawBytes.Length;
                    }
                    else if (base64String != null)
                    {
                        entity.LoadPath = base64String;
                    }
                    break;
                case TResourceFileSaveFormat.Raw:
                    if (rawBytes != null)
                    {
                        entity.Data = rawBytes;
                        entity.SizeInBytes = rawBytes.Length;
                    }
                    else if (base64String != null)
                    {
                        var fileData = _regexReplace.Replace(base64String, string.Empty);
                        var bytes = Convert.FromBase64String(fileData);
                        entity.Data = bytes;
                        entity.SizeInBytes = bytes.Length;
                    }
                    break;
            }
        }
        #endregion
    }
    /**@}*/
}