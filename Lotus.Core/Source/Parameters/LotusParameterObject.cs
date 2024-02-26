using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Lotus.Core
{
    /** \addtogroup CoreParameters
	*@{*/
    /// <summary>
    /// Класс для представления параметра значения которого представляет собой базовый объект.
    /// </summary>
    [Serializable]
    public class CParameterObject : ParameterItem<object>
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
            get { return TParameterValueType.Object; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CParameterObject()
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterObject(string parameterName, object value)
            : base(parameterName)
        {
            _value = value;
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Идентификатор параметра.</param>
        /// <param name="value">Значения параметра.</param>
        public CParameterObject(int id, object value)
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
        }
        #endregion
    }
    /**@}*/
}