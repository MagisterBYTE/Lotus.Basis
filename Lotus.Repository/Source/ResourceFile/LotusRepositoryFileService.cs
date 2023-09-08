//=====================================================================================================================
// Проект: Модуль репозитория
// Раздел: Подсистема файловых ресурсов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusRepositoryFileService.cs
*		Cервис для работы с для работы с файлами.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Threading;
using System;
using System.Linq;
//=====================================================================================================================
namespace Lotus
{
    namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup RepositoryResourceFile
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Cервис для работы с файлами
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class ResourceFileService : ILotusResourceFileService
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			private static readonly Regex RegexReplace = new Regex(@"^[\w/\:.-]+;base64,");
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			private readonly ILotusDataStorage _dataStorage;
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="dataStorage">Интерфейс для работы с сущностями</param>
			//---------------------------------------------------------------------------------------------------------
			public ResourceFileService(ILotusDataStorage dataStorage)
            {
				_dataStorage = dataStorage;
            }
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Создать/загрузить файл по указанным данным
			/// </summary>
			/// <param name="fileCreate">Параметры для создания файла</param>
			/// <param name="token">Токен отмены</param>
			/// <returns>Файл</returns>
			//---------------------------------------------------------------------------------------------------------
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

						await _dataStorage.AddAsync(entity);
						await _dataStorage.FlushAsync(token);
					}

					var result = entity.ToFileDto();

					return XResponse.Succeed<FileDto>(result);
				}

				if (fileCreate is FileCreateRawRequest requestRaw)
				{
					var entity = new ResourceFile();
					entity.Name = requestRaw.Name;
					entity.AuthorId = requestRaw.AuthorId;
					entity.FileTypeId = requestRaw.FileTypeId;
					entity.GroupId = requestRaw.GroupId;

					switch (requestRaw.Target)
					{
						case TResourceFileStorage.Local:
							break;
						case TResourceFileStorage.Server:
							{
							}
							break;
						case TResourceFileStorage.Database:
							{
								entity.StorageType = TResourceFileStorage.Database;
								switch (requestRaw.SaveFormat)
								{
									case TResourceFileSaveFormat.Base64:
										{
											var data = Convert.ToBase64String(requestRaw.Data!);
											entity.SaveFormat = TResourceFileSaveFormat.Base64;
											entity.LoadPath = data;
											entity.SizeInBytes = requestRaw.Data!.Length;
										}
										break;
									case TResourceFileSaveFormat.Raw:
										{
											entity.SaveFormat = TResourceFileSaveFormat.Raw;
											entity.Data = requestRaw.Data!;
											entity.SizeInBytes = requestRaw.Data!.Length;
										}
										break;
									default:
										break;
								}
							}
							break;
						default:
							break;
					}

					await _dataStorage.AddAsync(entity);
					await _dataStorage.FlushAsync(token);

					var result = entity.ToFileDto();

					return XResponse.Succeed<FileDto>(result);
				}

				if (fileCreate is FileCreateStreamRequest requestStream)
				{
					var entity = new ResourceFile();
					entity.Name = requestStream.Name;
					entity.AuthorId = requestStream.AuthorId;
					entity.FileTypeId = requestStream.FileTypeId;
					entity.GroupId = requestStream.GroupId;

					switch (requestStream.Target)
					{
						case TResourceFileStorage.Local:
							break;
						case TResourceFileStorage.Server:
							{
							}
							break;
						case TResourceFileStorage.Database:
							{
								using BinaryReader binaryReader = new BinaryReader(requestStream.ReadStream);
								var bytes = binaryReader.ReadBytes((Int32)requestStream.ReadStream.Length);
								entity.StorageType = TResourceFileStorage.Database;

								switch (requestStream.SaveFormat)
								{
									case TResourceFileSaveFormat.Base64:
										{
											var data = Convert.ToBase64String(bytes);
											entity.SaveFormat = TResourceFileSaveFormat.Base64;
											entity.LoadPath = data;
											entity.SizeInBytes = bytes.Length;
										}
										break;
									case TResourceFileSaveFormat.Raw:
										{
											entity.SaveFormat = TResourceFileSaveFormat.Raw;
											entity.Data = bytes;
											entity.SizeInBytes = bytes.Length;
										}
										break;
									default:
										break;
								}

								binaryReader.Close();
							}
							break;
						default:
							break;
					}

					await _dataStorage.AddAsync(entity);
					await _dataStorage.FlushAsync(token);
					var result = entity.ToFileDto();

					return XResponse.Succeed<FileDto>(result);
				}

				if (fileCreate is FileCreateBase64Request requestBase64)
				{
					var entity = new ResourceFile();
					entity.Name = requestBase64.Name;
					entity.AuthorId = requestBase64.AuthorId;
					entity.FileTypeId = requestBase64.FileTypeId;
					entity.GroupId = requestBase64.GroupId;

					switch (requestBase64.Target)
					{
						case TResourceFileStorage.Local:
							break;
						case TResourceFileStorage.Server:
							{
							}
							break;
						case TResourceFileStorage.Database:
							{
								entity.StorageType = TResourceFileStorage.Database;
								switch (requestBase64.SaveFormat)
								{
									case TResourceFileSaveFormat.Base64:
										{
											entity.SaveFormat = TResourceFileSaveFormat.Base64;
											entity.LoadPath = requestBase64.Data;
										}
										break;
									case TResourceFileSaveFormat.Raw:
										{
											var fileData = RegexReplace.Replace(requestBase64.Data, String.Empty);
											var bytes = Convert.FromBase64String(fileData);
											entity.SaveFormat = TResourceFileSaveFormat.Raw;
											entity.Data = bytes;
											entity.SizeInBytes = bytes.Length;
										}
										break;
									default:
										break;
								}
							}
							break;
						default:
							break;
					}

					await _dataStorage.AddAsync(entity);
					await _dataStorage.FlushAsync(token);
					
					var result = entity.ToFileDto();

					return XResponse.Succeed<FileDto>(result);
				}

				return XResponse.Failed<FileDto>(2000, "Неизвестный запрос");
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение указанного файла в формате Base64
			/// </summary>
			/// <param name="id">Идентификатор файла</param>
			/// <param name="token">Токен отмены</param>
			/// <returns>Файл в формате Base64</returns>
			//---------------------------------------------------------------------------------------------------------
			public async Task<Response<FileBase64Dto>> GetBase64Async(Guid id, CancellationToken token)
			{
				var entity = await _dataStorage.GetByIdAsync<ResourceFile, Guid>(id, token);
				if (entity == null)
				{
					return XResponse.Failed<FileBase64Dto>(XResourceFileErrors.NotFound);
				}

				FileBase64Dto result = entity.ToFileBase64Dto();

				return XResponse.Succeed(result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение списка файлов в формате Base64
			/// </summary>
			/// <param name="filesRequest">Параметры получения списка</param>
			/// <param name="token">Токен отмены</param>
			/// <returns>Cписок файлов в формате Base64</returns>
			//---------------------------------------------------------------------------------------------------------
			public async Task<ResponsePage<FileBase64Dto>> GetAllBase64Async(FilesRequest filesRequest, 
				CancellationToken token)
			{
				var query = _dataStorage.Query <ResourceFile>();

				if(filesRequest.AuthorId.HasValue)
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

				ResponsePage<FileBase64Dto> response = new ResponsePage<FileBase64Dto>()
				{
					Result = result.Result,
					PageInfo = result.PageInfo,
					Payload = result.Payload.Select(x => x.ToFileBase64Dto()).ToArray(),
				};

				return response;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Удаление файла
			/// </summary>
			/// <param name="id">Идентификатор файла</param>
			/// <param name="token">Токен отмены</param>
			/// <returns>Статус успешности</returns>
			//---------------------------------------------------------------------------------------------------------
			public async Task<Response> DeleteAsync(Guid id, CancellationToken token)
            {
				var entity = await _dataStorage.GetByIdAsync<ResourceFile, Guid>(id, token);
				if (entity == null)
				{
					return XResponse.Failed(XResourceFileErrors.NotFound);
				}


				_dataStorage.Remove(entity!);
                await _dataStorage.FlushAsync(token);

                return XResponse.Succeed();
            }
            #endregion
        }
        //-------------------------------------------------------------------------------------------------------------
        /**@}*/
        //-------------------------------------------------------------------------------------------------------------
    }
}
//=====================================================================================================================