#if UNITY_2017_1_OR_NEWER
using System;

namespace Lotus.Core
{
    /**
     * \defgroup CoreUnityTypes Подсистема общих типов Unity
     * \ingroup Core
     * \brief Подсистема общих типов реализует общие типы данных в Unity.
     * @{
     */
    /// <summary>
    /// Атрибут для определения порядка исполнения скрипта
    /// </summary>
    /// <remarks>
    /// Используется что бы автоматически сформировать нужный порядок исполнения скриптов
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class LotusExecutionOrderAttribute : Attribute
    {
        #region Fields
        internal int _executionOrder = -1;
        #endregion

        #region Properties
        /// <summary>
        /// Порядок исполнения скрипта
        /// </summary>
        public int ExecutionOrder
        {
            get { return _executionOrder; }
            set { _executionOrder = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="executionOrder">Порядок исполнения скрипта</param>
        public LotusExecutionOrderAttribute(int executionOrder)
        {
            _executionOrder = executionOrder;
        }
        #endregion
    }
    /**@}*/
}
#endif
