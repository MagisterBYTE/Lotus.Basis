//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема сериализации
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusSerializationDispatcher.cs
*		Диспетчер подсистемы сериализации данных для сохранения/загрузки объектов в различных форматах.
*		Диспетчер обеспечивает хранение и представление всех доступных сериализаторов, а также при сохранение/загрузки 
*	данных выбирает нужный тип сериализатора.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.IO;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreSerialization
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Диспетчер подсистемы сериализации данных для сохранения/загрузки объектов в различных форматах
		/// </summary>
		/// <remarks>
		/// Диспетчер обеспечивает хранение и представление всех доступных сериализаторов, а также 
		/// при сохранение/загрузки данных выбирает нужный тип сериализатора.
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XSerializationDispatcher
		{
			#region ======================================= СВОЙСТВА ==================================================
			/// <summary>
			/// Текущий сериализатор в формат Xml
			/// </summary>
			public static CSerializerXml SerializerXml { get; set; }

			/// <summary>
			/// Текущий сериализатор в формат Json
			/// </summary>
			public static CSerializerJson SerializerJson { get; set; }
			#endregion

			#region ======================================= МЕТОДЫ СОХРАНЕНИЯ =========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сохранения объекта в файл
			/// </summary>
			/// <remarks>
			/// Формат записи определяется исходя из расширения файла
			/// </remarks>
			/// <param name="file_name">Имя файла</param>
			/// <param name="instance">Экземпляр объекта</param>
			//---------------------------------------------------------------------------------------------------------
			public static void SaveTo(String file_name, System.Object instance)
			{
				String ext = Path.GetExtension(file_name).ToLower();
				switch (ext)
				{
					case XFileExtension.XML_D:
						{
							if(SerializerXml == null)
							{
								SerializerXml = new CSerializerXml();
							}

							SerializerXml.SaveTo(file_name, instance);
						}
						break;
					case XFileExtension.JSON_D:
						{
							if (SerializerJson == null)
							{
								SerializerJson = new CSerializerJson();
							}

							SerializerJson.SaveTo(file_name, instance);
						}
						break;
					case XFileExtension.BIN_D:
					case XFileExtension.BYTES_D:
						{
						}
						break;
					default:
						break;
				}
			}
			#endregion

			#region ======================================= МЕТОДЫ ЗАГРУЗКИ ===========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из файла
			/// </summary>
			/// <remarks>
			/// Формат чтения определяется исходя из расширения файла
			/// </remarks>
			/// <param name="file_name">Имя файла</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public static System.Object LoadFrom(String file_name)
			{
				String ext = Path.GetExtension(file_name).ToLower();
				System.Object result = null;
				switch (ext)
				{
					case XFileExtension.XML_D:
						{
							if (SerializerXml == null)
							{
								SerializerXml = new CSerializerXml();
							}

							result = SerializerXml.LoadFrom(file_name);
						}
						break;
					case XFileExtension.JSON_D:
						{
							if (SerializerJson == null)
							{
								SerializerJson = new CSerializerJson();
							}

							result = SerializerJson.LoadFrom(file_name);
						}
						break;
					case XFileExtension.BIN_D:
					case XFileExtension.BYTES_D:
						{
						}
						break;
					default:
						break;
				}

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Загрузка объекта из файла
			/// </summary>
			/// <remarks>
			/// Формат чтения определяется исходя из расширения файла
			/// </remarks>
			/// <typeparam name="TResultType">Тип объекта</typeparam>
			/// <param name="file_name">Имя файла</param>
			/// <returns>Объект</returns>
			//---------------------------------------------------------------------------------------------------------
			public static TResultType LoadFrom<TResultType>(String file_name)
			{
				String ext = Path.GetExtension(file_name).ToLower();
				TResultType result = default;
				switch (ext)
				{
					case XFileExtension.XML_D:
						{
							if (SerializerXml == null)
							{
								SerializerXml = new CSerializerXml();
							}

							result = SerializerXml.LoadFrom<TResultType>(file_name);
						}
						break;
					case XFileExtension.JSON_D:
						{
							if (SerializerJson == null)
							{
								SerializerJson = new CSerializerJson();
							}

							result = SerializerJson.LoadFrom<TResultType>(file_name);
						}
						break;
					case XFileExtension.BIN_D:
					case XFileExtension.BYTES_D:
						{
						}
						break;
					default:
						break;
				}

				return (result);
			}
			#endregion

			#region ======================================= МЕТОДЫ ОБНОВЛЕНИЯ =========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Обновление объекта из файла
			/// </summary>
			/// <remarks>
			/// Формат чтения определяется исходя из расширения файла
			/// </remarks>
			/// <param name="instance">Экземпляр объекта</param>
			/// <param name="file_name">Имя файла</param>
			//---------------------------------------------------------------------------------------------------------
			public static void UpdateFrom(System.Object instance, String file_name)
			{
				String ext = Path.GetExtension(file_name).ToLower();
				switch (ext)
				{
					case XFileExtension.XML_D:
						{
							if (SerializerXml == null)
							{
								SerializerXml = new CSerializerXml();
							}

							SerializerXml.UpdateFrom(instance, file_name);
						}
						break;
					case XFileExtension.JSON_D:
						{
							if (SerializerJson == null)
							{
								SerializerJson = new CSerializerJson();
							}

							SerializerJson.UpdateFrom(instance, file_name);
						}
						break;
					case XFileExtension.BIN_D:
					case XFileExtension.BYTES_D:
						{
						}
						break;
					default:
						break;
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================