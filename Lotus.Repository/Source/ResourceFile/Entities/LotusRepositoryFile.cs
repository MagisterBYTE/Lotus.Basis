﻿//=====================================================================================================================
// Проект: Модуль репозитория
// Раздел: Подсистема файловых ресурсов
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusRepositoryFile.cs
*		Класс для описания и представления отдельного файла.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.ComponentModel.DataAnnotations;
//---------------------------------------------------------------------------------------------------------------------
using Lotus.Core;
using Microsoft.EntityFrameworkCore;
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
		/// Класс для описания и предстваления отдельного файла
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class ResourceFile : RepositoryEntityBase<Guid>, IComparable<ResourceFile>
		{
			#region ======================================= КОНСТАНТНЫЕ ДАННЫЕ ========================================
			/// <summary>
			/// Имя таблицы
			/// </summary>
			public const String TABLE_NAME = "ResourceFile";
			#endregion

			#region ======================================= МЕТОДЫ ОПРЕДЕЛЕНИЯ МОДЕЛЕЙ ================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конфигурирование модели для типа <see cref="ResourceFile"/>
			/// </summary>
			/// <param name="modelBuilder">Интерфейс для построения моделей</param>
			/// <param name="schemeName">Схема куда будет помещена таблица</param>
			//---------------------------------------------------------------------------------------------------------
			public static void ModelCreating(ModelBuilder modelBuilder, String schemeName)
			{
				// Определение для таблицы
				var model = modelBuilder.Entity<ResourceFile>();
				model.ToTable(TABLE_NAME, schemeName);
			}
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Наименование файла
			/// </summary>
			[MaxLength(20)]
			public String? Name { get; set; }

			/// <summary>
			/// Место хранение файла
			/// </summary>
			public TResourceFileStorage StorageType { get; set; }

			/// <summary>
			/// Формат хранения файла в базе данных
			/// </summary>
			public TResourceFileSaveFormat SaveFormat { get; set; }

			/// <summary>
			/// Uri для загрузки файла если он храниться локально или на сервере
			/// </summary>
			[MaxLength(256)]
			public String? LoadPath { get; set; }

			/// <summary>
			/// Размер изображения в байтах
			/// </summary>
			public Int32? SizeInBytes { get; set; }

			/// <summary>
			/// Данные файла
			/// </summary>
			public Byte[]? Data { get; set; }

			/// <summary>
			/// Идентификатор автора файла
			/// </summary>
			public Guid? AuthorId { get; set; }

			/// <summary>
			/// Идентификатор типа файла
			/// </summary>
			public Int32? FileTypeId { get; set; }

			/// <summary>
			/// Идентификатор группы файла
			/// </summary>
			public Int32? GroupId { get; set; }
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение объектов для упорядочивания
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус сравнения объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(ResourceFile? other)
			{
				if (other == null) return 0;
				if (Name != null)
				{
					return (Name.CompareTo(other.Name));
				}
				else
				{
					return 0;
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Имя объекта</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return (Name ?? String.Empty);
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в объект типа <see cref="FileDto"/> 
			/// </summary>
			/// <returns>Объект <see cref="FileDto"/></returns>
			//---------------------------------------------------------------------------------------------------------
			public FileDto ToFileDto()
			{
				FileDto dto = new FileDto();
				dto.Name = Name;
				dto.Id = Id;
				dto.SizeInBytes = SizeInBytes;
				return dto;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование в объект типа <see cref="FileBase64Dto"/> 
			/// </summary>
			/// <returns>Объект <see cref="FileBase64Dto"/></returns>
			//---------------------------------------------------------------------------------------------------------
			public FileBase64Dto ToFileBase64Dto()
			{
				FileBase64Dto dto = new FileBase64Dto();
				dto.Name = Name;
				dto.Id = Id;
				dto.SizeInBytes = SizeInBytes;

				if (StorageType == TResourceFileStorage.Database)
				{
					if (SaveFormat == TResourceFileSaveFormat.Base64)
					{
						dto.Data = LoadPath!;
					}
					else
					{
						dto.Data = Convert.ToBase64String(Data!);
					}
				}

				return dto;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================
