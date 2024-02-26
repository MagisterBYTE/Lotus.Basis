using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут для определения статуса вхождения элемента инспектора свойств в определённую группу.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
#if UNITY_2017_1_OR_NEWER
	public sealed class LotusInGroupAttribute : UnityEngine.PropertyAttribute
#else
    public sealed class LotusInGroupAttribute : Attribute
#endif
    {
        #region Fields
        internal readonly string _groupName;
        #endregion

        #region Properties
        /// <summary>
        /// Имя группы которой принадлежит данный элемент.
        /// </summary>
        public string GroupName
        {
            get { return _groupName; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="groupName">Имя группы которой принадлежит данный элемент.</param>
        public LotusInGroupAttribute(string groupName)
        {
            _groupName = groupName;
        }
        #endregion
    }
    /**@}*/
}