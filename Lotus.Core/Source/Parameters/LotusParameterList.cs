using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Lotus.Core
{
    /** \addtogroup CoreParameters
	*@{*/
    /// <summary>
    /// Класс для представления параметра значения которого представляет собой список указанного типа.
    /// </summary>
    /// <typeparam name="TType">Тип элемента списка.</typeparam>
    [Serializable]
    public class CParameterList<TType> : ParameterItem<ListArray<TType>>
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
            get { return TParameterValueType.List; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CParameterList()
        {
            _value = new ListArray<TType>();
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="parameterName">Имя параметра.</param>
        /// <param name="items">Список элементов.</param>
        public CParameterList(string parameterName, params TType[] items)
            : base(parameterName)
        {
            _value = new ListArray<TType>(items);
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="id">Идентификатор параметра.</param>
        /// <param name="items">Список элементов.</param>
        public CParameterList(int id, params TType[] items)
            : base(id)
        {
            _value = new ListArray<TType>(items);
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

            streamWriter.Write(XCharHelper.DoubleQuotes);
            streamWriter.Write(Name);
            streamWriter.Write(XCharHelper.DoubleQuotes);

            streamWriter.Write(":\n");
            streamWriter.Write(XStringHelper.Depths[depth]);
            streamWriter.Write("[");

            if (typeof(TType).IsSupportInterface<IParameterItem>())
            {
                foreach (IParameterItem? item in _value!)
                {
                    if (item != null)
                    {
                        item.WriteToJson(streamWriter, depth + 1, true);
                        streamWriter.Write(",");
                    }
                }
            }
            else
            {
                foreach (var item in _value!)
                {
                    streamWriter.Write(item);
                    streamWriter.Write(",");
                }
            }

            streamWriter.Write("\n");
            streamWriter.Write(XStringHelper.Depths[depth]);
            streamWriter.Write("]");
        }
        #endregion
    }
    /**@}*/
}