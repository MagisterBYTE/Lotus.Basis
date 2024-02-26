using System;

namespace Lotus.Core
{
    /**
     * \defgroup CoreText Подсистема текстовых данных
     * \ingroup Core
     * \brief Подсистема текстовых данных реализуется базовый механизм для автоматической генерации текстовых данных, 
        их семантического и синтаксического редактирования, включая кодогенерацию и расширенное редактирование 
        текстовых файлов.
     * @{
     */
    /// <summary>
    /// Класс оболочка на стандартной строкой.
    /// </summary>
    public class CTextStr : IEquatable<CTextStr>, IEquatable<string>, IComparable<CTextStr>, IComparable<string>
    {
        #region Static fields
        #endregion

        #region Fields
        protected internal string _rawString;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Уровень вложенности строки.
        /// </summary>
        /// <remarks>
        /// Уровень вложенности строки определяет какой количество знаков табуляции находится в начале строки.
        /// </remarks>
        public int Indent
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
                    _rawString = _rawString.Insert(count, new string(XChar.Tab, value - count));
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
        /// Длина строки.
        /// </summary>
        /// <remarks>
        /// В случае установки длины строки больше существующий она дополняется последним символом.
        /// </remarks>
        public int Length
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
        /// Длина строки с учетом отступов.
        /// </summary>
        /// <remarks>
        /// В случае установки длины строки больше существующий она дополняется последним символом.
        /// </remarks>
        public int LengthText
        {
            get
            {
                var tabs = GetTabsStart();
                return _rawString.Length - tabs + (tabs * 4);
            }
        }

        /// <summary>
        /// Строка.
        /// </summary>
        public string RawString
        {
            get { return _rawString; }
            set
            {
                _rawString = value;
            }
        }

        /// <summary>
        /// Длина строки.
        /// </summary>
        public int RawLength
        {
            get { return _rawString.Length; }
        }

        //
        // ДОСТУП К СИМВОЛАМ
        //
        /// <summary>
        /// Первый символ строки.
        /// </summary>
        public char CharFirst
        {
            get { return _rawString[0]; }
            set
            {
                var massive = _rawString.ToCharArray();
                massive[0] = value;
                _rawString = new string(massive);
            }
        }

        /// <summary>
        /// Второй символ строки.
        /// </summary>
        public char CharSecond
        {
            get { return _rawString[1]; }
            set
            {
                var massive = _rawString.ToCharArray();
                massive[1] = value;
                _rawString = new string(massive);
            }
        }

        /// <summary>
        /// Предпоследний символ строки.
        /// </summary>
        public char CharPenultimate
        {
            get { return _rawString[_rawString.Length - 2]; }
            set
            {
                var massive = _rawString.ToCharArray();
                massive[_rawString.Length - 2] = value;
                _rawString = new string(massive);
            }
        }

        /// <summary>
        /// Последний символ строки.
        /// </summary>
        public char CharLast
        {
            get { return _rawString[_rawString.Length - 1]; }
            set
            {
                var massive = _rawString.ToCharArray();
                massive[_rawString.Length - 1] = value;
                _rawString = new string(massive);
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CTextStr()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="str">Строка.</param>
        public CTextStr(string str)
        {
            _rawString = str;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Проверяет равен ли текущий объект другому объекту того же типа.
        /// </summary>
        /// <param name="obj">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public override bool Equals(object? obj)
        {
            if (obj is CTextStr text_str)
            {
                return _rawString == text_str._rawString;
            }
            return base.Equals(obj);
        }

        /// <summary>
        /// Проверка равенства объектов по значению.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public bool Equals(CTextStr? other)
        {
            return string.Equals(_rawString, other?._rawString);
        }

        /// <summary>
        /// Проверка равенства объектов по значению.
        /// </summary>
        /// <param name="other">Сравниваемый объект.</param>
        /// <returns>Статус равенства объектов.</returns>
        public bool Equals(string? other)
        {
            if (other == null) return false;

            return string.Equals(_rawString, other);
        }

        /// <summary>
        /// Сравнение строк для упорядочивания.
        /// </summary>
        /// <param name="other">Строка.</param>
        /// <returns>Статус сравнения.</returns>
        public int CompareTo(CTextStr? other)
        {
            return string.CompareOrdinal(_rawString, other?.RawString);
        }

        /// <summary>
        /// Сравнение строк для упорядочивания.
        /// </summary>
        /// <param name="other">Строка.</param>
        /// <returns>Статус сравнения.</returns>
        public int CompareTo(string? other)
        {
            if (other == null) return 0;

            return string.CompareOrdinal(_rawString, other);
        }

        /// <summary>
        /// Получение хеш-кода строки.
        /// </summary>
        /// <returns>Хеш-код строки.</returns>
        public override int GetHashCode()
        {
            return _rawString.GetHashCode();
        }

        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Строка.</returns>
        public override string ToString()
        {
            return _rawString;
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сложение строк.
        /// </summary>
        /// <param name="left">Первая строка.</param>
        /// <param name="right">Вторая строка.</param>
        /// <returns>Объединённая строка.</returns>
        public static CTextStr operator +(CTextStr left, CTextStr right)
        {
            return new CTextStr(left._rawString + right._rawString);
        }

        /// <summary>
        /// Сравнение строк на равенство.
        /// </summary>
        /// <param name="left">Первая строка.</param>
        /// <param name="right">Вторая строка.</param>
        /// <returns>Статус равенства строк.</returns>
        public static bool operator ==(CTextStr left, CTextStr right)
        {
            return string.Equals(left._rawString, right._rawString);
        }

        /// <summary>
        /// Сравнение строк на неравенство.
        /// </summary>
        /// <param name="left">Первая строка.</param>
        /// <param name="right">Вторая строка.</param>
        /// <returns>Статус неравенства строк.</returns>
        public static bool operator !=(CTextStr left, CTextStr right)
        {
            return !string.Equals(left._rawString, right._rawString);
        }
        #endregion

        #region Operators conversion 
        /// <summary>
        /// Неявное преобразование в объект типа String.
        /// </summary>
        /// <param name="text_str">Строка.</param>
        /// <returns>Строка.</returns>
        public static implicit operator string(CTextStr text_str)
        {
            return text_str.RawString;
        }

        /// <summary>
        /// Неявное преобразование в объект типа <see cref="CTextStr"/>.
        /// </summary>
        /// <param name="str">Строка.</param>
        /// <returns>Строка.</returns>
        public static implicit operator CTextStr(string str)
        {
            return new CTextStr(str);
        }
        #endregion

        #region Indexer
        /// <summary>
        /// Индексация символов строки.
        /// </summary>
        /// <param name="index">Индекс символа.</param>
        /// <returns>Символ строки.</returns>
        public char this[int index]
        {
            get { return _rawString[index]; }
            set
            {
                var massive = _rawString.ToCharArray();
                massive[index] = value;
                _rawString = new string(massive);
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение количества символов табуляции с сначала строки.
        /// </summary>
        /// <returns>Количества символов табуляции.</returns>
        public int GetTabsStart()
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

        /// <summary>
        /// Установить длину строки.
        /// </summary>
        /// <remarks>
        /// Если длина больше требуемой то строка заполняется последним символом.
        /// </remarks>
        /// <param name="length">Длина строки.</param>
        public void SetLength(int length)
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
                    _rawString += new string(CharLast, count);
                }
            }
        }

        /// <summary>
        /// Установить длину строки.
        /// </summary>
        /// <remarks>
        /// Если длина больше требуемой то строка заполняется указанным символом.
        /// </remarks>
        /// <param name="length">Длина строки.</param>
        /// <param name="symbol">Символ.</param>
        public void SetLength(int length, char symbol)
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
                    _rawString += new string(symbol, count);
                }
            }
        }

        /// <summary>
        /// Установить длину строки с указанным последним символом.
        /// </summary>
        /// <remarks>
        /// Если длина больше требуемой то строка заполняется последним символом, но последний 
        /// символ всегда указанный
        /// </remarks>
        /// <param name="length">Длина строки.</param>
        /// <param name="symbol">Символ.</param>
        public void SetLengthAndLastChar(int length, char symbol)
        {
            SetLength(length);
            CharLast = symbol;
        }

        /// <summary>
        /// Установить длину строки с учетом начальных символов табуляции.
        /// </summary>
        /// <remarks>
        /// Если длина больше требуемой то строка заполняется последним символом.
        /// </remarks>
        /// <param name="length">Длина строки.</param>
        /// <param name="tabsEquiv">Размер одного символа табуляции.</param>
        public void SetLengthWithTabs(int length, int tabsEquiv = 4)
        {
            var count_tabs = GetTabsStart();
            if (count_tabs > 0)
            {
                // Меняем табы на пробелы
                _rawString = _rawString.Remove(0, count_tabs);
                _rawString = _rawString.Insert(0, new string(XChar.Space, count_tabs * tabsEquiv));

                SetLength(length);

                // Меняем пробелы на табы
                _rawString = _rawString.Remove(0, count_tabs * tabsEquiv);
                _rawString = _rawString.Insert(0, new string(XChar.Tab, count_tabs));
            }
            else
            {
                SetLength(length);
            }
        }

        /// <summary>
        /// Установить длину строки с учетом начальных символов табуляции.
        /// </summary>
        /// <remarks>
        /// Если длина больше требуемой то строка заполняется указанным символом.
        /// </remarks>
        /// <param name="length">Длина строки.</param>
        /// <param name="symbol">Символ.</param>
        /// <param name="tabsEquiv">Размер одного символа табуляции.</param>
        public void SetLengthWithTabs(int length, char symbol, int tabsEquiv = 4)
        {
            var count_tabs = GetTabsStart();
            if (count_tabs > 0)
            {
                // Меняем табы на пробелы
                _rawString = _rawString.Remove(0, count_tabs);
                _rawString = _rawString.Insert(0, new string(XChar.Space, count_tabs * tabsEquiv));

                SetLength(length, symbol);

                // Меняем пробелы на табы
                _rawString = _rawString.Remove(0, count_tabs * tabsEquiv);
                _rawString = _rawString.Insert(0, new string(XChar.Tab, count_tabs));
            }
            else
            {
                SetLength(length, symbol);
            }
        }
        #endregion
    }
    /**@}*/
}