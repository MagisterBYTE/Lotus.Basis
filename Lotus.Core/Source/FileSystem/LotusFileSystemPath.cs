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
			public const String TXTD = ".txt";

			/// <summary>
			/// Расширение XML файла
			/// </summary>
			public const String XML = "xml";

			/// <summary>
			/// Расширение XML файла
			/// </summary>
			public const String XMLD = ".xml";

			/// <summary>
			/// Расширение JSON файла
			/// </summary>
			public const String JSON = "json";

			/// <summary>
			/// Расширение JSON файла
			/// </summary>
			public const String JSOND = ".json";

			/// <summary>
			/// Расширение файла Lua скрипта
			/// </summary>
			public const String LUA = "lua";

			/// <summary>
			/// Расширение файла Lua скрипта
			/// </summary>
			public const String LUAD = ".lua";

			/// <summary>
			/// Стандартное расширение файла с бинарными данными
			/// </summary>
			public const String BIN = "bin";

			/// <summary>
			/// Стандартное расширение файла с бинарными данными
			/// </summary>
			public const String BIND = ".bin";

			/// <summary>
			/// Расширение файла с бинарными данными для TextAsset
			/// </summary>
			public const String BYTES = "bytes";

			/// <summary>
			/// Расширение файла с бинарными данными для TextAsset
			/// </summary>
			public const String BYTESD = ".bytes";
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка расширения имени файла на принадлежность к текстовым данным
			/// </summary>
			/// <param name="fileName">Имя файла</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsTextFileName(String fileName)
			{
				var exe = Path.GetExtension(fileName).ToLower();
				if(exe == TXTD)
				{
					return true;
				}

				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка расширения имени файла на принадлежность к бинарным данным
			/// </summary>
			/// <param name="fileName">Имя файла</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsBinaryFileName(String fileName)
			{
				var exe = Path.GetExtension(fileName).ToLower();
				if (exe == BIND || exe == BYTESD)
				{
					return true;
				}

				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка расширения имени файла на принадлежность к формату XML
			/// </summary>
			/// <param name="fileName">Имя файла</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsXmlFileName(String fileName)
			{
				var exe = Path.GetExtension(fileName).ToLower();
				if (exe == XMLD)
				{
					return true;
				}

				return false;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка расширения имени файла на принадлежность к формату JSON
			/// </summary>
			/// <param name="fileName">Имя файла</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean IsJSONFileName(String fileName)
			{
				var exe = Path.GetExtension(fileName).ToLower();
				if (exe == JSOND)
				{
					return true;
				}

				return false;
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
			/// <param name="fileName">Имя файла</param>
			/// <param name="ext">Расширение файла</param>
			/// <returns>Имя файла доступного для загрузки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetFileName(String path, String fileName, String ext)
			{
#if UNITY_2017_1_OR_NEWER
				file_name = file_name.Replace(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
#else
				fileName = fileName.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
#endif

				var result = "";
				if(fileName.IndexOf(Path.DirectorySeparatorChar) > -1 || fileName.IndexOf(Path.AltDirectorySeparatorChar) > -1)
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
					result = fileName;
#endif
				}
				else
				{
					result = Path.Combine(path, fileName);
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
			/// <param name="newFileName">Новое имя файла</param>
			/// <returns>Путь с новым именем файла</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetPathForRenameFile(String path, String newFileName)
			{
				var dir = Path.GetDirectoryName(path);
				var exe = Path.GetExtension(path);

				var result = "";
				if (Path.HasExtension(newFileName))
				{
					result = Path.Combine(dir, newFileName);
				}
				else
				{
					result = Path.Combine(dir, newFileName + exe);
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
			/// Проверка на корректность имени файла для сохранения
			/// </summary>
			/// <remarks>
			/// Имя файла корректно, если оно не пустое, длиной меньше 240 символов и не содержит запрещающих символов
			/// </remarks>
			/// <param name="fileName">Имя файла</param>
			/// <returns>Статус корректности имени файла</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean CheckCorrectFileName(String fileName)
			{
				if(String.IsNullOrEmpty(fileName))
				{
					return false;
				}

				if (fileName.Length > 240)
				{
					return false;
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