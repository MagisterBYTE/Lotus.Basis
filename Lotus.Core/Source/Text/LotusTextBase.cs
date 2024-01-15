//=====================================================================================================================
// Проект: Модуль базового ядра
// Раздел: Подсистема текстовых данных
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
//---------------------------------------------------------------------------------------------------------------------
/** \file LotusTextBase.cs
*		Определение общих типов и структур данных для подсистемы текстовых данных.
*/
//---------------------------------------------------------------------------------------------------------------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023
//=====================================================================================================================
using System;
//=====================================================================================================================
namespace Lotus
{
	namespace Core
	{
		//-------------------------------------------------------------------------------------------------------------
		/**
         * \defgroup CoreText Подсистема текстовых данных
         * \ingroup Core
         * \brief Подсистема текстовых данных реализуется базовый механизм для автоматической генерации текстовых данных, 
			их семантического и синтаксического редактирования, включая кодогенерацию и расширенное редактирование 
			текстовых файлов.
         * @{
         */
		//-------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Класс оболочка на стандартной строкой
		/// </summary>
		//-------------------------------------------------------------------------------------------------------------
		public class CTextStr : IEquatable<CTextStr>, IEquatable<String>, IComparable<CTextStr>, IComparable<String>
		{
			#region ======================================= СТАТИЧЕСКИЕ ДАННЫЕ ========================================
			#endregion

			#region ======================================= ДАННЫЕ ====================================================
			protected internal String _rawString;
			#endregion

			#region ======================================= СВОЙСТВА ==================================================
			//
			// ОСНОВНЫЕ ПАРАМЕТРЫ
			//
			/// <summary>
			/// Уровень вложенности строки
			/// </summary>
			/// <remarks>
			/// Уровень вложенности строки определяет какой количество знаков табуляции находится в начале строки
			/// </remarks>
			public Int32 Indent
			{
				get
				{
					return GetTabsStart();
				}
				set
				{
					var count = GetTabsStart();
					if (value > count)
					{
						_rawString = _rawString.Insert(count, new String(XChar.Tab, value - count));
					}
					else
					{
						if (value < count)
						{
							_rawString = _rawString.Remove(0, count - value);
						}
					}
				}
			}

			/// <summary>
			/// Длина строки
			/// </summary>
			/// <remarks>
			/// В случае установки длины строки больше существующий она дополняется последним символом
			/// </remarks>
			public Int32 Length
			{
				get
				{
					return _rawString.Length;
				}
				set
				{
					SetLength(value);
				}
			}

			/// <summary>
			/// Длина строки с учетом отступов
			/// </summary>
			/// <remarks>
			/// В случае установки длины строки больше существующий она дополняется последним символом
			/// </remarks>
			public Int32 LengthText
			{
				get
				{
					var tabs = GetTabsStart();
					return _rawString.Length - tabs + (tabs * 4);
				}
			}

			/// <summary>
			/// Строка
			/// </summary>
			public String RawString
			{
				get { return _rawString; }
				set
				{
					_rawString = value;
				}
			}

			/// <summary>
			/// Длина строки
			/// </summary>
			public Int32 RawLength
			{
				get { return _rawString.Length; }
			}

			//
			// ДОСТУП К СИМВОЛАМ
			//
			/// <summary>
			/// Первый символ строки
			/// </summary>
			public Char CharFirst
			{
				get { return _rawString[0]; }
				set
				{
					var massive = _rawString.ToCharArray();
					massive[0] = value;
					_rawString = new String(massive);
				}
			}

			/// <summary>
			/// Второй символ строки
			/// </summary>
			public Char CharSecond
			{
				get { return _rawString[1]; }
				set
				{
					var massive = _rawString.ToCharArray();
					massive[1] = value;
					_rawString = new String(massive);
				}
			}

			/// <summary>
			/// Предпоследний символ строки
			/// </summary>
			public Char CharPenultimate
			{
				get { return _rawString[_rawString.Length - 2]; }
				set
				{
					var massive = _rawString.ToCharArray();
					massive[_rawString.Length - 2] = value;
					_rawString = new String(massive);
				}
			}

			/// <summary>
			/// Последний символ строки
			/// </summary>
			public Char CharLast
			{
				get { return _rawString[_rawString.Length - 1]; }
				set
				{
					var massive = _rawString.ToCharArray();
					massive[_rawString.Length - 1] = value;
					_rawString = new String(massive);
				}
			}
			#endregion

			#region ======================================= КОНСТРУКТОРЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
			/// </summary>
			//---------------------------------------------------------------------------------------------------------
			public CTextStr()
			{
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="str">Строка</param>
			//---------------------------------------------------------------------------------------------------------
			public CTextStr(String str)
			{
				_rawString = str;
			}
			#endregion

			#region ======================================= СИСТЕМНЫЕ МЕТОДЫ ==========================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверяет равен ли текущий объект другому объекту того же типа
			/// </summary>
			/// <param name="obj">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Boolean Equals(System.Object? obj)
			{
				if (obj != null)
				{
					if (obj is CTextStr text_str)
					{
						return _rawString == text_str._rawString;
					}
				}
				return base.Equals(obj);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства объектов по значению
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(CTextStr? other)
			{
				return String.Equals(_rawString, other?._rawString);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства объектов по значению
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(String? other)
			{
                if (other == null) return false;

                return String.Equals(_rawString, other);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение строк для упорядочивания
			/// </summary>
			/// <param name="other">Строка</param>
			/// <returns>Статус сравнения</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CTextStr? other)
			{
                return String.CompareOrdinal(_rawString, other?.RawString);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение строк для упорядочивания
			/// </summary>
			/// <param name="other">Строка</param>
			/// <returns>Статус сравнения</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(String? other)
			{
                if (other == null) return 0;

                return String.CompareOrdinal(_rawString, other);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода строки
			/// </summary>
			/// <returns>Хеш-код строки</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				return _rawString.GetHashCode();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Строка</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return _rawString;
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ =================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сложение строк
			/// </summary>
			/// <param name="left">Первая строка</param>
			/// <param name="right">Вторая строка</param>
			/// <returns>Объединённая строка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static CTextStr operator +(CTextStr left, CTextStr right)
			{
				return new CTextStr(left._rawString + right._rawString);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение строк на равенство
			/// </summary>
			/// <param name="left">Первая строка</param>
			/// <param name="right">Вторая строка</param>
			/// <returns>Статус равенства строк</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator ==(CTextStr left, CTextStr right)
			{
				return String.Equals(left._rawString, right._rawString);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение строк на неравенство
			/// </summary>
			/// <param name="left">Первая строка</param>
			/// <param name="right">Вторая строка</param>
			/// <returns>Статус неравенства строк</returns>
			//---------------------------------------------------------------------------------------------------------
			public static Boolean operator !=(CTextStr left, CTextStr right)
			{
				return !String.Equals(left._rawString, right._rawString);
			}
			#endregion

			#region ======================================= ОПЕРАТОРЫ ПРЕОБРАЗОВАНИЯ ==================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа String
			/// </summary>
			/// <param name="text_str">Строка</param>
			/// <returns>Строка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator String(CTextStr text_str)
			{
				return text_str.RawString;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Неявное преобразование в объект типа <see cref="CTextStr"/>
			/// </summary>
			/// <param name="str">Строка</param>
			/// <returns>Строка</returns>
			//---------------------------------------------------------------------------------------------------------
			public static implicit operator CTextStr(String str)
			{
				return new CTextStr(str);
			}
			#endregion

			#region ======================================= ИНДЕКСАТОР ================================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Индексация символов строки
			/// </summary>
			/// <param name="index">Индекс символа</param>
			/// <returns>Символ строки</returns>
			//---------------------------------------------------------------------------------------------------------
			public Char this[Int32 index]
			{
				get { return _rawString[index]; }
				set
				{
					var massive = _rawString.ToCharArray();
					massive[index] = value;
					_rawString = new String(massive);
				}
			}
			#endregion

			#region ======================================= ОБЩИЕ МЕТОДЫ ==============================================
			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение количества символов табуляции с сначала строки
			/// </summary>
			/// <returns>Количества символов табуляции</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 GetTabsStart()
			{
				var count = 0;
				var find = false;
				for (var i = 0; i < _rawString.Length; i++)
				{
					if (_rawString[i] != XChar.Tab)
					{
						if (i == 0) break;
						if (find) break;
					}

					if (_rawString[i] == XChar.Tab)
					{
						count++;
						find = true;
					}
				}
				return count;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установить длину строки
			/// </summary>
			/// <remarks>
			/// Если длина больше требуемой то строка заполняется последним символом
			/// </remarks>
			/// <param name="length">Длина строки</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLength(Int32 length)
			{
				if (_rawString.Length > length)
				{
					_rawString = _rawString.Remove(length);
				}
				else
				{
					if (_rawString.Length < length)
					{
						var count = length - _rawString.Length;
						_rawString += new String(CharLast, count);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установить длину строки
			/// </summary>
			/// <remarks>
			/// Если длина больше требуемой то строка заполняется указанным символом
			/// </remarks>
			/// <param name="length">Длина строки</param>
			/// <param name="symbol">Символ</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLength(Int32 length, Char symbol)
			{
				if (_rawString.Length > length)
				{
					_rawString = _rawString.Remove(length);
				}
				else
				{
					if (_rawString.Length < length)
					{
						var count = length - _rawString.Length;
						_rawString += new String(symbol, count);
					}
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установить длину строки с указанным последним символом
			/// </summary>
			/// <remarks>
			/// Если длина больше требуемой то строка заполняется последним символом, но последний 
			/// символ всегда указанный
			/// </remarks>
			/// <param name="length">Длина строки</param>
			/// <param name="symbol">Символ</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLengthAndLastChar(Int32 length, Char symbol)
			{
				SetLength(length);
				CharLast = symbol;
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установить длину строки с учетом начальных символов табуляции
			/// </summary>
			/// <remarks>
			/// Если длина больше требуемой то строка заполняется последним символом
			/// </remarks>
			/// <param name="length">Длина строки</param>
			/// <param name="tabsEquiv">Размер одного символа табуляции</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLengthWithTabs(Int32 length, Int32 tabsEquiv = 4)
			{
				var count_tabs = GetTabsStart();
				if (count_tabs > 0)
				{
					// Меняем табы на пробелы
					_rawString = _rawString.Remove(0, count_tabs);
					_rawString = _rawString.Insert(0, new String(XChar.Space, count_tabs * tabsEquiv));

					SetLength(length);

					// Меняем пробелы на табы
					_rawString = _rawString.Remove(0, count_tabs * tabsEquiv);
					_rawString = _rawString.Insert(0, new String(XChar.Tab, count_tabs));
				}
				else
				{
					SetLength(length);
				}
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Установить длину строки с учетом начальных символов табуляции
			/// </summary>
			/// <remarks>
			/// Если длина больше требуемой то строка заполняется указанным символом
			/// </remarks>
			/// <param name="length">Длина строки</param>
			/// <param name="symbol">Символ</param>
			/// <param name="tabsEquiv">Размер одного символа табуляции</param>
			//---------------------------------------------------------------------------------------------------------
			public void SetLengthWithTabs(Int32 length, Char symbol, Int32 tabsEquiv = 4)
			{
				var count_tabs = GetTabsStart();
				if (count_tabs > 0)
				{
					// Меняем табы на пробелы
					_rawString = _rawString.Remove(0, count_tabs);
					_rawString = _rawString.Insert(0, new String(XChar.Space, count_tabs * tabsEquiv));

					SetLength(length, symbol);

					// Меняем пробелы на табы
					_rawString = _rawString.Remove(0, count_tabs * tabsEquiv);
					_rawString = _rawString.Insert(0, new String(XChar.Tab, count_tabs));
				}
				else
				{
					SetLength(length, symbol);
				}
			}
			#endregion
		}
		//-------------------------------------------------------------------------------------------------------------
		/**@}*/
		//-------------------------------------------------------------------------------------------------------------
	}
}
//=====================================================================================================================