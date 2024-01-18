//=====================================================================================================================
// Проект: LotusPlatform
// Проект: Модуль репозитория
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusResourceFileTesting.cs
*		Тестирование методов работы с файлами.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Repository;
//=====================================================================================================================
namespace Lotus
{
	namespace Repository
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Тестирование методов работы с файлами
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class ResourceFileTesting : IClassFixture<DomainContextFixture>
		{
			public DomainContextFixture Fixture { get; }

			public ResourceFileTesting(DomainContextFixture fixture)
			{
				Fixture = fixture;
			}

			[Fact]
			public async Task TestResourceFile()
			{
				var dataStorage = Fixture.CreateDataStorage();
				ResourceFileService service = new ResourceFileService(dataStorage);

				const string base64 = "iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==";

				//
				// Локальные файлы
				//
				var localRequest = new FileCreateLocalRequest()
				{
					Id = Guid.NewGuid(),
					Name = "file"
				};
				await service.CreateAsync(localRequest, CancellationToken.None);

				var response = await service.GetBase64Async(localRequest.Id, CancellationToken.None);
				
				Assert.Equal(response.Payload.Id, localRequest.Id);

				await service.CreateAsync(localRequest, CancellationToken.None);

				//
				// Base64 - Base64
				//
				var localBase64 = new FileCreateBase64Request()
				{
					Name = "file",
					Data = base64,
				};

				var responseCreate = await service.CreateAsync(localBase64, CancellationToken.None);
				Assert.NotNull(response.Payload);

				var responseGet = await service.GetBase64Async(responseCreate.Payload.Id, CancellationToken.None);
				Assert.Equal(base64, responseGet.Payload.Data);

				//
				// Base64 - Raw
				//
				localBase64 = new FileCreateBase64Request()
				{
					Name = "file",
					Data = base64,
					SaveFormat = TResourceFileSaveFormat.Raw
				};

				responseCreate = await service.CreateAsync(localBase64, CancellationToken.None);
				Assert.NotNull(response.Payload);

				responseGet = await service.GetBase64Async(responseCreate.Payload.Id, CancellationToken.None);
				Assert.Equal(base64, responseGet.Payload.Data);

				//
				// Raw - Raw
				//
				var localRaw = new FileCreateRawRequest()
				{
					Name = "file",
					Data = Convert.FromBase64String(base64)
				};

				responseCreate = await service.CreateAsync(localRaw, CancellationToken.None);
				Assert.NotNull(response.Payload);

				responseGet = await service.GetBase64Async(responseCreate.Payload.Id, CancellationToken.None);
				Assert.Equal(base64, responseGet.Payload.Data);

				//
				// Raw - Base64
				//
				localRaw = new FileCreateRawRequest()
				{
					Name = "file",
					Data = Convert.FromBase64String(base64),
					SaveFormat = TResourceFileSaveFormat.Base64
				};

				responseCreate = await service.CreateAsync(localRaw, CancellationToken.None);
				Assert.NotNull(response.Payload);

				responseGet = await service.GetBase64Async(responseCreate.Payload.Id, CancellationToken.None);
				Assert.Equal(base64, responseGet.Payload.Data);

				//
				// Stream - Base64
				//
				var memoryStream = new MemoryStream(Convert.FromBase64String(base64));
				var localStream = new FileCreateStreamRequest()
				{
					Name = "file",
					ReadStream = memoryStream,
					SaveFormat = TResourceFileSaveFormat.Base64
				};

				responseCreate = await service.CreateAsync(localStream, CancellationToken.None);
				Assert.NotNull(response.Payload);

				responseGet = await service.GetBase64Async(responseCreate.Payload.Id, CancellationToken.None);
				Assert.Equal(base64, responseGet.Payload.Data);
				memoryStream.Close();

				//
				// Stream - Raw
				//
				memoryStream = new MemoryStream(Convert.FromBase64String(base64));
				localStream = new FileCreateStreamRequest()
				{
					Name = "file",
					ReadStream = memoryStream,
					SaveFormat = TResourceFileSaveFormat.Raw
				};

				responseCreate = await service.CreateAsync(localStream, CancellationToken.None);
				Assert.NotNull(response.Payload);

				responseGet = await service.GetBase64Async(responseCreate.Payload.Id, CancellationToken.None);
				Assert.Equal(base64, responseGet.Payload.Data);
				memoryStream.Close();
			}
		}
	}
}
//=====================================================================================================================