using System;

namespace Lotus.Core
{
    /** \addtogroup CoreText
	*@{*/
    /// <summary>
    /// Строка текстовых данных.
    /// </summary>
    public class CTextLine : CTextStr, IComparable<CTextLine>, ILotusIndexable, ILotusDuplicate<CTextLine>
    {
        #region Static fields
        #endregion

        #region Fields
        protected internal string _label;
        protected internal int _index;
        protected internal CTextList _owned;
        #endregion

        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Метка строки.
        /// </summary>
        /// <remarks>
        /// Метка служит для дополнительной идентификации строки.
        /// </remarks>
        public string Label
        {
            get { return _label; }
            set
            {
                _label = value;
            }
        }

        /// <summary>
        /// Индекс строки в списке строк.
        /// </summary>
        public int Index
        {
            get { return _index; }
            set
            {
                _index = value;
            }
        }

        /// <summary>
        /// Список строк текстовых данных - Владелец данной строки.
        /// </summary>
        public CTextList Owned
        {
            get { return _owned; }
            set
            {
                _owned = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CTextLine()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="str">Строка.</param>
        public CTextLine(string str)
        {
            _rawString = str;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="prefix">Начальный префикс строки.</param>
        /// <param name="symbol">Символ для заполнения.</param>
        /// <param name="totalLength">Общая требуемая длина строки.</param>
        public CTextLine(string prefix, char symbol, int totalLength)
        {
            _rawString = prefix + new string(symbol, totalLength - prefix.Length);
        }
        #endregion

        #region System methods
        /// <summary>
        /// Сравнение строк текстовых данных для упорядочивания.
        /// </summary>
        /// <param name="other">Строка.</param>
        /// <returns>Статус сравнения.</returns>
        public int CompareTo(CTextLine? other)
        {
            return _rawString.CompareTo(other?._rawString);
        }

        /// <summary>
        /// Получение дубликата объекта.
        /// </summary>
        /// <param name="parameters">Параметры дублирования объекта.</param>
        /// <returns>Дубликат объекта.</returns>
        public CTextLine Duplicate(CParameters? parameters = null)
        {
            return new CTextLine(this.RawString);
        }
        #endregion

        #region Operators
        /// <summary>
        /// Сложение строк.
        /// </summary>
        /// <param name="left">Первая строка текстовых данных.</param>
        /// <param name="right">Вторая строка текстовых данных.</param>
        /// <returns>Объединённая строка текстовых данных.</returns>
        public static CTextLine operator +(CTextLine left, CTextLine right)
        {
            return new CTextLine(left._rawString + right._rawString);
        }
        #endregion

        #region Operators conversion 
        /// <summary>
        /// Неявное преобразование в объект типа String.
        /// </summary>
        /// <param name="text_line">Строка текстовых данных.</param>
        /// <returns>Строка.</returns>
        public static implicit operator string(CTextLine text_line)
        {
            return text_line.RawString;
        }

        /// <summary>
        /// Неявное преобразование в объект типа <see cref="CTextLine"/>.
        /// </summary>
        /// <param name="str">Строка.</param>
        /// <returns>Строка текстовых данных.</returns>
        public static implicit operator CTextLine(string str)
        {
            return new CTextLine(str);
        }
        #endregion
    }
    /**@}*/
}