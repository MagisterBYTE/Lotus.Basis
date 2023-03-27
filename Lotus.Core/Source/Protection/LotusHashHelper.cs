//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема защиты
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusHashHelper.cs
*		Работа с хешами по различным алгоритмам.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.Security.Cryptography;
using System.Text;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс для работы с хешами по различным алгоритмам
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XHashHelper
		{
			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеша строки
			/// </summary>
			/// <remarks>
			/// Используется алгоритм SHA1
			/// </remarks>
			/// <param name="input">Входная строка</param>
			/// <returns>Хеш строки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static String GetHash(String input)
			{
				using SHA1 sha1Hash = SHA1.Create();

				// Convert the input string to a byte array and compute the hash.
				byte[] data = sha1Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

				// Create a new Stringbuilder to collect the bytes
				// and create a string.
				var sBuilder = new StringBuilder();

				// Loop through each byte of the hashed data
				// and format each one as a hexadecimal string.
				for (int i = 0; i < data.Length; i++)
				{
					sBuilder.Append(data[i].ToString("x2"));
				}

				// Return the hexadecimal string.
				return sBuilder.ToString();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка хеша
			/// </summary>
			/// <remarks>
			/// Используется алгоритм SHA1
			/// </remarks>
			/// <param name="input">Входная строка</param>
			/// <param name="hash">Хеш строки</param>
			/// <returns>Статус проверки</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean VerifyHash(String input, String hash)
			{
				// Hash the input.
				var hashOfInput = GetHash(input);

				// Create a StringComparer an compare the hashes.
				StringComparer comparer = StringComparer.OrdinalIgnoreCase;

				return comparer.Compare(hashOfInput, hash) == 0;
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================