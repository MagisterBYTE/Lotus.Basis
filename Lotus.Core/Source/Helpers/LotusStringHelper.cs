//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Вспомогательная подсистема
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusStringHelper.cs
*		Работа со строками.
*		Реализация дополнительных методов и константных данных при работе со строками и символами.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 27.03.2022
//=====================================================================================================================
using System;
using System.ComponentModel;
using System.Collections.Generic;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		//! \addtogroup CoreHelpers
		/*@{*/
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Опции поиска в строке другой подстроки
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		[TypeConverter(typeof(EnumToStringConverter<TStringSearchOption>))]
		public enum TStringSearchOption
		{
			/// <summary>
			/// Подстрока должна находится в начала
			/// </summary>
			[Description("В начале")]
			Start,

			/// <summary>
			/// Подстрока должна находится в конце
			/// </summary>
			[Description("В конце")]
			End,

			/// <summary>
			/// Подстрока может находиться в любом месте
			/// </summary>
			[Description("Содержит")]
			Contains,

			/// <summary>
			/// Подстрока должна точно совпадать
			/// </summary>
			[Description("Равно")]
			Equal
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий дополнительные методы и константные данные при работе со строками
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XString
		{
			#region ======================================= ДАННЫЕ ====================================================
			//
			// КОНСТАНТНЫЕ ДАННЫЕ
			//
			/// <summary>
			/// Символ разделитель новой строки
			/// </summary>
			public const String NewLine = "\n";

			/// <summary>
			/// Символ разделитель возврата каретки
			/// </summary>
			public const String CarriageReturn = "\r";

			/// <summary>
			/// Символ смайлика
			/// </summary>
			public const String Smiley = "☺";

			/// <summary>
			/// Символ света
			/// </summary>
			public const String Light = "☼";

			/// <summary>
			/// Символ стрелки вверх
			/// </summary>
			public const String ArrowUp = "↑";

			/// <summary>
			/// Символ стрелки вниз
			/// </summary>
			public const String ArrowDown = "↓";

			/// <summary>
			/// Символ стрелки вправо
			/// </summary>
			public const String ArrowRight = "→";

			/// <summary>
			/// Символ стрелки влево
			/// </summary>
			public const String ArrowLeft = "←";

			/// <summary>
			/// Символ треугольника вверх
			/// </summary>
			public const String TriangleUp = "▲";

			/// <summary>
			/// Символ треугольника вниз
			/// </summary>
			public const String TriangleDown = "▼";

			/// <summary>
			/// Символ треугольника вправо
			/// </summary>
			public const String TriangleRight = "►";

			/// <summary>
			/// Символ треугольника влево
			/// </summary>
			public const String TriangleLeft = "◄";

			/// <summary>
			/// Символ дома
			/// </summary>
			public const String Home = "⌂";

			/// <summary>
			/// Символ кавычки слева (французские)
			/// </summary>
			public const String QuoteLeft = "«";

			/// <summary>
			/// Символ кавычки справа (французские)
			/// </summary>
			public const String QuoteRight = "»";

			/// <summary>
			/// Символ меню
			/// </summary>
			public const String Menu = "≡";

			/// <summary>
			/// Символ квадрата
			/// </summary>
			public const String Square = "■";

			/// <summary>
			/// Символ плюс
			/// </summary>
			public const String Plus = "+";

			/// <summary>
			/// Символ минус
			/// </summary>
			public const String Minus = "-";

			//
			// ДАННЫЕ РАЗДЕЛИТЕЛЕЙ
			//
			/// <summary>
			/// Разделителей для данных текстовых файлов
			/// </summary>
			/// <remarks>
			/// Данный разделитель используется чтобы отделить некий заголовок(ключ) от следующих за ним по порядку данных 
			/// </remarks>
			public static readonly String SeparatorFileData = "##";

			/// <summary>
			/// Массив символов разделителей (новая строка)
			/// </summary>
			public static readonly String[] SeparatorNewLine = new String[] { "\n" };

			/// <summary>
			/// Массив символов разделителей (новая строка и возврат каретки)
			/// </summary>
			public static readonly String[] SeparatorNewCarriageLine = new String[] { "\n", "\r" };

			/// <summary>
			/// Массив символов разделителей (запятая и новая строка)
			/// </summary>
			public static readonly String[] SeparatorComma = new String[] { ",", "\n" };

			/// <summary>
			/// Массив символов разделителей (точка запятая)
			/// </summary>
			public static readonly String[] SeparatorDotComma = new String[] { ";" };

			/// <summary>
			/// Массив символов разделителей (квадратные скобки)
			/// </summary>
			public static readonly String[] SeparatorSquareBracket = new String[] { "[", "]" };

			/// <summary>
			/// Массив символов разделителей (нижние подчеркивание)
			/// </summary>
			public static readonly String[] SeparatorLowLine = new String[] { "_" };

			/// <summary>
			/// Массив символов разделителей (пробел)
			/// </summary>
			public static readonly String[] SeparatorSpaces = new String[] { " " };

			/// <summary>
			/// Массив символов разделителей (символ табуляции)
			/// </summary>
			public static readonly String[] SeparatorTab = new String[] { "\t" };

			/// <summary>
			/// Массив символов для разделение на предложения
			/// </summary>
			public static readonly String[] SeparatorSentences = new String[] { ".", "!", "?" };

			/// <summary>
			/// Массив символов табуляции
			/// </summary>
			public static readonly String[] Depths = new String[] 
			{
				"",
				"\t",
				"\t\t",
				"\t\t\t",
				"\t\t\t\t",
				"\t\t\t\t\t",
				"\t\t\t\t\t\t",
				"\t\t\t\t\t\t\t",
				"\t\t\t\t\t\t\t\t",
				"\t\t\t\t\t\t\t\t\t",
				"\t\t\t\t\t\t\t\t\t\t",
				"\t\t\t\t\t\t\t\t\t\t\t",
				"\t\t\t\t\t\t\t\t\t\t\t\t",
				"\t\t\t\t\t\t\t\t\t\t\t\t\t",
				"\t\t\t\t\t\t\t\t\t\t\t\t\t\t",
			};

			/// <summary>
			/// Массив символов пробела
			/// </summary>
			public static readonly String[] Spaces = new String[] 
			{
				"",
				new String(XChar.Space, 1),
				new String(XChar.Space, 2),
				new String(XChar.Space, 3),
				new String(XChar.Space, 4),
				new String(XChar.Space, 5),
				new String(XChar.Space, 6),
				new String(XChar.Space, 7),
				new String(XChar.Space, 8),
				new String(XChar.Space, 9),
				new String(XChar.Space, 10),
				new String(XChar.Space, 11),
				new String(XChar.Space, 12),
				new String(XChar.Space, 13),
				new String(XChar.Space, 14),
				new String(XChar.Space, 15)
			};

			/// <summary>
			/// Массив символов пробела c начальным символом новой строки
			/// </summary>
			/// <remarks>
			/// При последовательном присвоение позволяет получить иерархически выглядящую строку
			/// </remarks>
			public static readonly String[] HierarchySpaces = new String[]
			{
				"",
				new String(XChar.Space, 1).Insert(0, XString.NewLine),
				new String(XChar.Space, 2).Insert(0, XString.NewLine),
				new String(XChar.Space, 3).Insert(0, XString.NewLine),
				new String(XChar.Space, 4).Insert(0, XString.NewLine),
				new String(XChar.Space, 5).Insert(0, XString.NewLine),
				new String(XChar.Space, 6).Insert(0, XString.NewLine),
				new String(XChar.Space, 7).Insert(0, XString.NewLine),
				new String(XChar.Space, 8).Insert(0, XString.NewLine),
				new String(XChar.Space, 9).Insert(0, XString.NewLine),
				new String(XChar.Space, 10).Insert(0, XString.NewLine),
				new String(XChar.Space, 11).Insert(0, XString.NewLine),
				new String(XChar.Space, 12).Insert(0, XString.NewLine),
				new String(XChar.Space, 13).Insert(0, XString.NewLine),
				new String(XChar.Space, 14).Insert(0, XString.NewLine),
				new String(XChar.Space, 15).Insert(0, XString.NewLine)
			};
			#endregion

			#region ======================================= МЕТОДЫ ====================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конвертация массива строк в группы строк разделяемых по указанному разделителю
			/// </summary>
			/// <remarks>
			/// Метод ищет по строкам вхождение разделителя (должен быть в начале строки) и присваивает его ключу.
			/// Дальнейшие строки, до другого вхождения разделителя, присваиваются значению
			/// </remarks>
			/// <param name="lines">Массив строк</param>
			/// <param name="delimiters">Строка для разделения данных</param>
			/// <returns>Список данных ключ-значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static List<KeyValuePair<String, String>> ConvertLinesToGroupLines(String[] lines, String delimiters)
			{
				List<KeyValuePair<String, String>> result = new List<KeyValuePair<String, String>>(30);

				if (lines != null && lines.Length > 1)
				{
					// Проходим все строки в файле
					for (Int32 i = 0; i < lines.Length; i++)
					{
						// Подготавливаем для анализа токен
						String token = lines[i].Trim(XChar.NewLine, XChar.CarriageReturn, XChar.Space);

						// Если нашли разделитель (должен быть в начале)
						if (token.IndexOf(delimiters) == 0)
						{
							// В ключ записываем данные разделения
							String key = token;

							String current_value = "";

							// Читаем данные (со следующей строк)
							for (Int32 j = i + 1; j < lines.Length; j++)
							{
								// Пустые пропускаем
								if (String.IsNullOrEmpty(lines[j]))
								{
									continue;
								}

								// Если дошли до следующего разделителя то выходим
								if (lines[j].IndexOf(delimiters) == 0)
								{
									// Скорректируем позицию чтения
									i = j - 1;

									break;
								}

								// Добавляем в значение
								if (String.IsNullOrEmpty(current_value))
								{
									current_value = lines[j].Trim();
								}
								else
								{
									if (lines[j].Length == 1 && (lines[j][0] == XChar.NewLine || lines[j][0] == XChar.CarriageReturn))
									{

									}
									else
									{
										current_value += XChar.NewLine + lines[j].Trim();
									}
								}
							}

							// Конец
							result.Add(new KeyValuePair<String, String>(key, current_value));
						}
					}
				}

				return result;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конвертация массива строк в группы строк разделяемых по указанному разделителю игнорируя комментарии
			/// </summary>
			/// <remarks>
			/// Метод ищет по строкам вхождение разделителя (должен быть в начале строки) и присваивает его ключу.
			/// Дальнейшие строки, до другого вхождения разделителя, присваиваются значению.
			/// Также данный метод игнорирует комментарии в стиле С# начинающиеся с символов //
			/// </remarks>
			/// <param name="lines">Массив строк</param>
			/// <param name="delimiters">Строка для разделения данных</param>
			/// <returns>Список данных ключ-значение</returns>
			//---------------------------------------------------------------------------------------------------------
			public static List<KeyValuePair<String, String>> ConvertLinesToGroupLinesIgnoringComments(String[] lines, String delimiters)
			{
				List<KeyValuePair<String, String>> result = new List<KeyValuePair<String, String>>(30);

				if (lines != null && lines.Length > 1)
				{
					// Проходим все строки
					for (Int32 i = 0; i < lines.Length; i++)
					{
						// Подготавливаем для анализа токен
						String token = lines[i].Trim(XChar.NewLine, XChar.CarriageReturn, XChar.Space);

						// Игнорируем комментарий 
						if (token.IndexOf("//") == 0) continue;

						// Если нашли разделитель (должен быть в начале)
						if (token.IndexOf(delimiters) == 0)
						{
							// В ключ записываем данные разделения
							String key = token;

							String current_value = "";

							// Читаем данные (со следующей строк)
							for (Int32 j = i + 1; j < lines.Length; j++)
							{
								// Игнорируем комментарий 
								if (lines[j].IndexOf("//") == 0) continue;

								// Пустые пропускаем
								if (String.IsNullOrEmpty(lines[j]))
								{
									continue;
								}

								// Если дошли до следующего разделителя то выходим
								if (lines[j].IndexOf(delimiters) > -1)
								{
									// Скорректируем позицию чтения
									i = j - 1;

									break;
								}

								// Добавляем в значение
								if (String.IsNullOrEmpty(current_value))
								{
									current_value = lines[j].Trim();
								}
								else
								{
									if (lines[j].Length == 1 && (lines[j][0] == XChar.NewLine || lines[j][0] == XChar.CarriageReturn))
									{

									}
									else
									{
										current_value += XChar.NewLine + lines[j].Trim();
									}
								}
							}

							// Конец
							result.Add(new KeyValuePair<String, String>(key, current_value));
						}
					}
				}

				return result;
			}
			#endregion
		}

		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Статический класс реализующий дополнительные методы и константные данные при работе с символами
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public static class XChar
		{
			//
			// КОНСТАНТНЫЕ ДАННЫЕ
			//
			/// <summary>
			/// Символ разделитель новой строки
			/// </summary>
			public const Char NewLine = '\n';

			/// <summary>
			/// Символ разделитель возврата каретки
			/// </summary>
			public const Char CarriageReturn = '\r';

			/// <summary>
			/// Символ пробела
			/// </summary>
			public const Char Space = ' ';

			/// <summary>
			/// Символ табуляции
			/// </summary>
			public const Char Tab = '\t';

			/// <summary>
			/// Символ смайлика
			/// </summary>
			public const Char Smiley = '☺';

			/// <summary>
			/// Символ света
			/// </summary>
			public const Char Light = '☼';

			/// <summary>
			/// Символ стрелки вверх
			/// </summary>
			public const Char ArrowUp = '↑';

			/// <summary>
			/// Символ стрелки вниз
			/// </summary>
			public const Char ArrowDown = '↓';

			/// <summary>
			/// Символ стрелки вправо
			/// </summary>
			public const Char ArrowRight = '→';

			/// <summary>
			/// Символ стрелки влево
			/// </summary>
			public const Char ArrowLeft = '←';

			/// <summary>
			/// Символ треугольника вверх
			/// </summary>
			public const Char TriangleUp = '▲';

			/// <summary>
			/// Символ треугольника вниз
			/// </summary>
			public const Char TriangleDown = '▼';

			/// <summary>
			/// Символ треугольника вправо
			/// </summary>
			public const Char TriangleRight = '►';

			/// <summary>
			/// Символ треугольника влево
			/// </summary>
			public const Char TriangleLeft = '◄';

			/// <summary>
			/// Символ дома
			/// </summary>
			public const Char Home = '⌂';

			/// <summary>
			/// Символ кавычки слева (французские)
			/// </summary>
			public const Char QuoteLeft = '«';

			/// <summary>
			/// Символ кавычки справа (французские)
			/// </summary>
			public const Char QuoteRight = '»';

			/// <summary>
			/// Символ двойных кавычек
			/// </summary>
			public const Char DoubleQuotes = '"';

			/// <summary>
			/// Символ меню
			/// </summary>
			public const Char Menu = '≡';

			/// <summary>
			/// Символ квадрата
			/// </summary>
			public const Char Square = '■';

			/// <summary>
			/// Символ плюс
			/// </summary>
			public const Char Plus = '+';

			/// <summary>
			/// Символ минус
			/// </summary>
			public const Char Minus = '-';

			/// <summary>
			/// Символ запятая
			/// </summary>
			public const Char Comma = ',';

			/// <summary>
			/// Символ точки
			/// </summary>
			public const Char Dot = '.';

			//
			// ДАННЫЕ РАЗДЕЛИТЕЛЕЙ
			//
			/// <summary>
			/// Массив символов разделителей (новая строка)
			/// </summary>
			public static readonly Char[] SeparatorNewLine = new Char[] { '\n' };

			/// <summary>
			/// Массив символов разделителей (новая строка и возврат каретки)
			/// </summary>
			public static readonly Char[] SeparatorNewCarriageLine = new Char[] { '\n', '\r' };

			/// <summary>
			/// Массив символов разделителей (запятая и новая строка)
			/// </summary>
			public static readonly Char[] SeparatorComma = new Char[] { ',', '\n' };

			/// <summary>
			/// Массив символов разделителей (точка запятая)
			/// </summary>
			public static readonly Char[] SeparatorDotComma = new Char[] { ';' };

			/// <summary>
			/// Массив символов разделителей (квадратные скобки)
			/// </summary>
			public static readonly Char[] SeparatorSquareBracket = new Char[] { '[', ']' };

			/// <summary>
			/// Массив символов разделителей (нижние подчеркивание)
			/// </summary>
			public static readonly Char[] SeparatorLowLine = new Char[] { '_' };

			/// <summary>
			/// Массив символов разделителей (пробел)
			/// </summary>
			public static readonly Char[] SeparatorSpaces = new Char[] { ' ' };

			/// <summary>
			/// Массив символов разделителей (символ табуляции)
			/// </summary>
			public static readonly Char[] SeparatorTab = new Char[] { '\t' };

			/// <summary>
			/// Массив символов для разделение на предложения
			/// </summary>
			public static readonly Char[] SeparatorSentences = new Char[] { '.', '!', '?' };
		}
		//-------------------------------------------------------------------------------------------------------------
		/*@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================