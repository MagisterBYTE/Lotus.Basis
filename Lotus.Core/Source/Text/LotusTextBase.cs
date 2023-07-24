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
			protected internal String mRawString;
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
						mRawString = mRawString.Insert(count, new String(XChar.Tab, value - count));
					}
					else
					{
						if (value < count)
						{
							mRawString = mRawString.Remove(0, count - value);
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
					return mRawString.Length;
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
					return mRawString.Length - tabs + (tabs * 4);
				}
			}

			/// <summary>
			/// Строка
			/// </summary>
			public String RawString
			{
				get { return mRawString; }
				set
				{
					mRawString = value;
				}
			}

			/// <summary>
			/// Длина строки
			/// </summary>
			public Int32 RawLength
			{
				get { return mRawString.Length; }
			}

			//
			// ДОСТУП К СИМВОЛАМ
			//
			/// <summary>
			/// Первый символ строки
			/// </summary>
			public Char CharFirst
			{
				get { return mRawString[0]; }
				set
				{
					var massive = mRawString.ToCharArray();
					massive[0] = value;
					mRawString = new String(massive);
				}
			}

			/// <summary>
			/// Второй символ строки
			/// </summary>
			public Char CharSecond
			{
				get { return mRawString[1]; }
				set
				{
					var massive = mRawString.ToCharArray();
					massive[1] = value;
					mRawString = new String(massive);
				}
			}

			/// <summary>
			/// Предпоследний символ строки
			/// </summary>
			public Char CharPenultimate
			{
				get { return mRawString[mRawString.Length - 2]; }
				set
				{
					var massive = mRawString.ToCharArray();
					massive[mRawString.Length - 2] = value;
					mRawString = new String(massive);
				}
			}

			/// <summary>
			/// Последний символ строки
			/// </summary>
			public Char CharLast
			{
				get { return mRawString[mRawString.Length - 1]; }
				set
				{
					var massive = mRawString.ToCharArray();
					massive[mRawString.Length - 1] = value;
					mRawString = new String(massive);
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
				mRawString = str;
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
			public override Boolean Equals(Object obj)
			{
				if (obj != null)
				{
					if (obj is CTextStr text_str)
					{
						return mRawString == text_str.mRawString;
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
			public Boolean Equals(CTextStr other)
			{
				return String.Equals(mRawString, other.mRawString);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Проверка равенства объектов по значению
			/// </summary>
			/// <param name="other">Сравниваемый объект</param>
			/// <returns>Статус равенства объектов</returns>
			//---------------------------------------------------------------------------------------------------------
			public Boolean Equals(String other)
			{
				return String.Equals(mRawString, other);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение строк для упорядочивания
			/// </summary>
			/// <param name="other">Строка</param>
			/// <returns>Статус сравнения</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(CTextStr other)
			{
				return String.CompareOrdinal(mRawString, other.RawString);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Сравнение строк для упорядочивания
			/// </summary>
			/// <param name="other">Строка</param>
			/// <returns>Статус сравнения</returns>
			//---------------------------------------------------------------------------------------------------------
			public Int32 CompareTo(String other)
			{
				return String.CompareOrdinal(mRawString, other);
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Получение хеш-кода строки
			/// </summary>
			/// <returns>Хеш-код строки</returns>
			//---------------------------------------------------------------------------------------------------------
			public override Int32 GetHashCode()
			{
				return mRawString.GetHashCode();
			}

			//---------------------------------------------------------------------------------------------------------
			/// <summary>
			/// Преобразование к текстовому представлению
			/// </summary>
			/// <returns>Строка</returns>
			//---------------------------------------------------------------------------------------------------------
			public override String ToString()
			{
				return mRawString;
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
				return new CTextStr(left.mRawString + right.mRawString);
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
				return String.Equals(left.mRawString, right.mRawString);
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
				return !String.Equals(left.mRawString, right.mRawString);
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
				get { return mRawString[index]; }
				set
				{
					var massive = mRawString.ToCharArray();
					massive[index] = value;
					mRawString = new String(massive);
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
				for (var i = 0; i < mRawString.Length; i++)
				{
					if (mRawString[i] != XChar.Tab)
					{
						if (i == 0) break;
						if (find) break;
					}

					if (mRawString[i] == XChar.Tab)
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
				if (mRawString.Length > length)
				{
					mRawString = mRawString.Remove(length);
				}
				else
				{
					if (mRawString.Length < length)
					{
						var count = length - mRawString.Length;
						mRawString += new String(CharLast, count);
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
				if (mRawString.Length > length)
				{
					mRawString = mRawString.Remove(length);
				}
				else
				{
					if (mRawString.Length < length)
					{
						var count = length - mRawString.Length;
						mRawString += new String(symbol, count);
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
				if(count_tabs > 0)
				{
					// Меняем табы на пробелы
					mRawString = mRawString.Remove(0, count_tabs);
					mRawString = mRawString.Insert(0, new String(XChar.Space, count_tabs * tabsEquiv));

					SetLength(length);

					// Меняем пробелы на табы
					mRawString = mRawString.Remove(0, count_tabs * tabsEquiv);
					mRawString = mRawString.Insert(0, new String(XChar.Tab, count_tabs));
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
					mRawString = mRawString.Remove(0, count_tabs);
					mRawString = mRawString.Insert(0, new String(XChar.Space, count_tabs * tabsEquiv));

					SetLength(length, symbol);

					// Меняем пробелы на табы
					mRawString = mRawString.Remove(0, count_tabs * tabsEquiv);
					mRawString = mRawString.Insert(0, new String(XChar.Tab, count_tabs));
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