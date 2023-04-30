//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема файловой системы
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusFileSystemPath.cs
*		Работа с сущностями файловой системы.
*		Реализация дополнительных методов для работы с путями, имена файлов и директорий в файловой системе, также
*	работа с расширениями файлов, их определение и классификация
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
using System.Collections.Generic;
using System.IO;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/** \addtogroup CoreFileSystem
		*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы для работы с расширениями файлов
		/// </summary>
		/// <remarks>
		/// Типовые расширения файлов приведены без точек и с точками в начале
		/// </remarks>
		//-------------------------------------------------------------------------------------------------------------
		public static class XFileExtension
		{
			#region ======================================= ТИПОВЫЕ РАСШИРЕНИЯ ФАЙЛОВ =================================
			/// <summary>
			/// Расширение текстового файла
			/// </summary>
			public const String TXT = "txt";

			/// <summary>
			/// Расширение текстового файла
			/// </summary>
			public const String TXT_D = ".txt";

			/// <summary>
			/// Расширение XML файла
			/// </summary>
			public const String XML = "xml";

			/// <summary>
			/// Расширение XML файла
			/// </summary>
			public const String XML_D = ".xml";

			/// <summary>
			/// Расширение JSON файла
			/// </summary>
			public const String JSON = "json";

			/// <summary>
			/// Расширение JSON файла
			/// </summary>
			public const String JSON_D = ".json";

			/// <summary>
			/// Расширение файла Lua скрипта
			/// </summary>
			public const String LUA = "lua";

			/// <summary>
			/// Расширение файла Lua скрипта
			/// </summary>
			public const String LUA_D = ".lua";

			/// <summary>
			/// Стандартное расширение файла с бинарными данными
			/// </summary>
			public const String BIN = "bin";

			/// <summary>
			/// Стандартное расширение файла с бинарными данными
			/// </summary>
			public const String BIN_D = ".bin";

			/// <summary>
			/// Расширение файла с бинарными данными для TextAsset
			/// </summary>
			public const String BYTES = "bytes";

			/// <summary>
			/// Расширение файла с бинарными данными для TextAsset
			/// </summary>
			public const String BYTES_D = ".bytes";
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка расширения имени файла на принадлежность к текстовым данным
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsTextFileName(String file_name)
			{
				var exe = Path.GetExtension(file_name).ToLower();
				if(exe == TXT_D)
				{
					return (true);
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка расширения имени файла на принадлежность к бинарным данным
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsBinaryFileName(String file_name)
			{
				var exe = Path.GetExtension(file_name).ToLower();
				if (exe == BIN_D || exe == BYTES_D)
				{
					return (true);
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка расширения имени файла на принадлежность к формату XML
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsXmlFileName(String file_name)
			{
				var exe = Path.GetExtension(file_name).ToLower();
				if (exe == XML_D)
				{
					return (true);
				}

				return (false);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка расширения имени файла на принадлежность к формату JSON
			/// </summary>
			/// <param name="file_name">Имя файла</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsJSONFileName(String file_name)
			{
				var exe = Path.GetExtension(file_name).ToLower();
				if (exe == JSON_D)
				{
					return (true);
				}

				return (false);
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий методы для работы с путями, имена файлов и директорий в файловой системе
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XFilePath
		{
			#region ======================================= РАБОТА С ФАЙЛАМИ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение имени файла доступного для загрузки
			/// </summary>
			/// <remarks>
			/// Метод анализирует имя файла и при необходимости добавляет путь и расширение
			/// </remarks>
			/// <param name="path">Путь</param>
			/// <param name="file_name">Имя файла</param>
			/// <param name="ext">Расширение файла</param>
			/// <returns>Имя файла доступного для загрузки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetFileName(String path, String file_name, String ext)
			{
#if UNITY_2017_1_OR_NEWER
				file_name = file_name.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
#else
				file_name = file_name.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
#endif

				var result = "";
				if(file_name.IndexOf(Path.DirectorySeparatorChar) > -1 || file_name.IndexOf(Path.AltDirectorySeparatorChar) > -1)
				{
#if UNITY_2017_1_OR_NEWER
					if(file_name.Contains(XCoreSettings.ASSETS_PATH))
					{
						result = file_name;
					}
					else
					{
						result = Path.Combine(path, file_name);
					}
#else
					result = file_name;
#endif
				}
				else
				{
					result = Path.Combine(path, file_name);
				}

				if(Path.HasExtension(result) == false)
				{
					if (ext[0] != XChar.Dot)
					{
						result = result + XChar.Dot + ext;
					}
					else
					{
						result = result + ext;
					}
				}

#if UNITY_2017_1_OR_NEWER
				result = result.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
#else
				result = result.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
#endif

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение полного пути файла с новым именем
			/// </summary>
			/// <param name="path">Путь</param>
			/// <param name="new_file_name">Новое имя файла</param>
			/// <returns>Путь с новым именем файла</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetPathForRenameFile(String path, String new_file_name)
			{
				var dir = Path.GetDirectoryName(path);
				var exe = Path.GetExtension(path);

				var result = "";
				if (Path.HasExtension(new_file_name))
				{
					result = Path.Combine(dir, new_file_name);
				}
				else
				{
					result = Path.Combine(dir, new_file_name + exe);
				}

#if UNITY_2017_1_OR_NEWER
				result = result.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
#else
				result = result.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
#endif

				return (result);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка на корректность имени файла для сохранения
			/// </summary>
			/// <remarks>
			/// Имя файла корректно, если оно не пустое, длиной меньше 240 символов и не содержит запрещающих символов
			/// </remarks>
			/// <param name="file_name">Имя файла</param>
			/// <returns>Статус корректности имени файла</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean CheckCorrectFileName(String file_name)
			{
				if(String.IsNullOrEmpty(file_name))
				{
					return (false);
				}

				if (file_name.Length > 240)
				{
					return (false);
				}

				return true;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================