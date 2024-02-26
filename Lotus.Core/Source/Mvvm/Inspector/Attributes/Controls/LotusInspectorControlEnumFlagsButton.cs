using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут для отображения перечисления(с возможностью выбора нескольких значений) в виде кнопок.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusEnumFlagsButtonAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusEnumFlagsButtonAttribute : Attribute
#endif
    {
        #region Fields
        #endregion

        #region Properties
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public LotusEnumFlagsButtonAttribute()
        {
        }
        #endregion
    }
    /**@}*/
}