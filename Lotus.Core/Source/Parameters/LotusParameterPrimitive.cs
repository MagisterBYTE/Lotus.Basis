using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Lotus.Core
{
    /** \addtogroup CoreParameters
	*@{*/
    /// <summary>
    /// Класс для представления параметра значения которого представляет собой логический тип.
    /// </summary>
    [Serializable]
    public class CParameterBool : ParameterItem<bool>
    {
        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Тип данных значения.
        /// </summary>
        [XmlAttribute]
        public override TParameterValueType ValueType
        {
            get { return TParameterValueType.Boolean; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CParameterBool()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterBool(string parameterName, bool value)
            : base(parameterName)
        {
            _value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Идентификатор параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterBool(int id, bool value)
            : base(id)
        {
            _value = value;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Запись в строковый поток в формате Json.
        /// </summary>
        /// <param name="streamWriter">Строковый поток.</param>
        /// <param name="depth">Текущая глубина вложенности.</param>
        /// <param name="isArray">Статус массива.</param>
        public override void WriteToJson(StreamWriter streamWriter, int depth, bool isArray)
        {
            streamWriter.Write(XCharHelper.NewLine);
            streamWriter.Write(XStringHelper.Depths[depth]);

            if (isArray == false)
            {
                streamWriter.Write(XCharHelper.DoubleQuotes);
                streamWriter.Write(Name);
                streamWriter.Write(XCharHelper.DoubleQuotes);

                streamWriter.Write(": ");
            }
            streamWriter.Write(Value);
        }
        #endregion
    }

    /// <summary>
    /// Класс для представления параметра значения которого представляет собой целый тип.
    /// </summary>
    [Serializable]
    public class CParameterInteger : ParameterItem<int>
    {
        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Тип данных значения.
        /// </summary>
        [XmlAttribute]
        public override TParameterValueType ValueType
        {
            get { return TParameterValueType.Integer; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CParameterInteger()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterInteger(string parameterName, int value)
            : base(parameterName)
        {
            _value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Идентификатор параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterInteger(int id, int value)
            : base(id)
        {
            _value = value;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Запись в строковый поток в формате Json.
        /// </summary>
        /// <param name="streamWriter">Строковый поток.</param>
        /// <param name="depth">Текущая глубина вложенности.</param>
        /// <param name="isArray">Статус массива.</param>
        public override void WriteToJson(StreamWriter streamWriter, int depth, bool isArray)
        {
            streamWriter.Write(XCharHelper.NewLine);
            streamWriter.Write(XStringHelper.Depths[depth]);

            if (isArray == false)
            {
                streamWriter.Write(XCharHelper.DoubleQuotes);
                streamWriter.Write(Name);
                streamWriter.Write(XCharHelper.DoubleQuotes);

                streamWriter.Write(": ");
            }

            streamWriter.Write(Value);
        }
        #endregion
    }

    /// <summary>
    /// Класс для представления параметра значения которого представляет собой вещественный тип.
    /// </summary>
    [Serializable]
    public class CParameterReal : ParameterItem<double>
    {
        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Тип данных значения.
        /// </summary>
        [XmlAttribute]
        public override TParameterValueType ValueType
        {
            get { return TParameterValueType.Real; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CParameterReal()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterReal(string parameterName, double value)
            : base(parameterName)
        {
            _value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Идентификатор параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterReal(int id, double value)
            : base(id)
        {
            _value = value;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Запись в строковый поток в формате Json.
        /// </summary>
        /// <param name="streamWriter">Строковый поток.</param>
        /// <param name="depth">Текущая глубина вложенности.</param>
        /// <param name="isArray">Статус массива.</param>
        public override void WriteToJson(StreamWriter streamWriter, int depth, bool isArray)
        {
            streamWriter.Write(XCharHelper.NewLine);
            streamWriter.Write(XStringHelper.Depths[depth]);

            if (isArray == false)
            {
                streamWriter.Write(XCharHelper.DoubleQuotes);
                streamWriter.Write(Name);
                streamWriter.Write(XCharHelper.DoubleQuotes);

                streamWriter.Write(": ");
            }

            streamWriter.Write(Value);
        }
        #endregion
    }

    /// <summary>
    /// Класс для представления параметра значения которого представляет собой тип даты-времени.
    /// </summary>
    [Serializable]
    public class CParameterDatetime : ParameterItem<DateTime>
    {
        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Тип данных значения.
        /// </summary>
        [XmlAttribute]
        public override TParameterValueType ValueType
        {
            get { return TParameterValueType.DateTime; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CParameterDatetime()
        {
            _value = DateTime.Now;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterDatetime(string parameterName, DateTime value)
            : base(parameterName)
        {
            _value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Идентификатор параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterDatetime(int id, DateTime value)
            : base(id)
        {
            _value = value;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Запись в строковый поток в формате Json.
        /// </summary>
        /// <param name="streamWriter">Строковый поток.</param>
        /// <param name="depth">Текущая глубина вложенности.</param>
        /// <param name="isArray">Статус массива.</param>
        public override void WriteToJson(StreamWriter streamWriter, int depth, bool isArray)
        {
            streamWriter.Write(XCharHelper.NewLine);
            streamWriter.Write(XStringHelper.Depths[depth]);

            if (isArray == false)
            {
                streamWriter.Write(XCharHelper.DoubleQuotes);
                streamWriter.Write(Name);
                streamWriter.Write(XCharHelper.DoubleQuotes);

                streamWriter.Write(": ");
            }

            streamWriter.Write(Value.ToShortTimeString());
        }
        #endregion
    }

    /// <summary>
    /// Класс для представления параметра значения которого представляет собой строковый тип.
    /// </summary>
    [Serializable]
    public class CParameterString : ParameterItem<string>
    {
        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Тип данных значения.
        /// </summary>
        [XmlAttribute]
        public override TParameterValueType ValueType
        {
            get { return TParameterValueType.String; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CParameterString()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterString(string parameterName, string value)
            : base(parameterName)
        {
            _value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Идентификатор параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterString(int id, string value)
            : base(id)
        {
            _value = value;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Запись в строковый поток в формате Json.
        /// </summary>
        /// <param name="streamWriter">Строковый поток.</param>
        /// <param name="depth">Текущая глубина вложенности.</param>
        /// <param name="isArray">Статус массива.</param>
        public override void WriteToJson(StreamWriter streamWriter, int depth, bool isArray)
        {
            streamWriter.Write(XCharHelper.NewLine);
            streamWriter.Write(XStringHelper.Depths[depth]);

            if (isArray == false)
            {
                streamWriter.Write(XCharHelper.DoubleQuotes);
                streamWriter.Write(Name);
                streamWriter.Write(XCharHelper.DoubleQuotes);

                streamWriter.Write(": ");
            }

            streamWriter.Write(XCharHelper.DoubleQuotes);
            streamWriter.Write(Value);
            streamWriter.Write(XCharHelper.DoubleQuotes);
        }
        #endregion
    }

    /// <summary>
    /// Класс для представления параметра значения которого представляет собой тип перечисления.
    /// </summary>
    /// <typeparam name="TEnum">Тип перечисления.</typeparam>
    [Serializable]
    public class CParameterEnum<TEnum> : ParameterItem<TEnum> where TEnum : Enum
    {
        #region Properties
        //
        // ОСНОВНЫЕ ПАРАМЕТРЫ
        //
        /// <summary>
        /// Тип данных значения.
        /// </summary>
        [XmlAttribute]
        public override TParameterValueType ValueType
        {
            get { return TParameterValueType.Enum; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CParameterEnum()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterEnum(string parameterName, TEnum value)
            : base(parameterName)
        {
            _value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Идентификатор параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterEnum(int id, TEnum value)
            : base(id)
        {
            _value = value;
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Запись в строковый поток в формате Json.
        /// </summary>
        /// <param name="streamWriter">Строковый поток.</param>
        /// <param name="depth">Текущая глубина вложенности.</param>
        /// <param name="isArray">Статус массива.</param>
        public override void WriteToJson(StreamWriter streamWriter, int depth, bool isArray)
        {
            streamWriter.Write(XCharHelper.NewLine);
            streamWriter.Write(XStringHelper.Depths[depth]);

            if (isArray == false)
            {
                streamWriter.Write(XCharHelper.DoubleQuotes);
                streamWriter.Write(Name);
                streamWriter.Write(XCharHelper.DoubleQuotes);

                streamWriter.Write(": ");
            }

            streamWriter.Write(XCharHelper.DoubleQuotes);
            streamWriter.Write(Value);
            streamWriter.Write(XCharHelper.DoubleQuotes);
        }
        #endregion
    }
    /**@}*/
}