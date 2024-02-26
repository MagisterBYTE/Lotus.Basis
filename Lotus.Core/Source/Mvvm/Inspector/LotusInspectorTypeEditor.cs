using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspector
	*@{*/
    /// <summary>
    /// Атрибут для определения редактора для свойства.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class LotusInspectorTypeEditor : Attribute
    {
        #region Fields
        internal Type _editorType;
        #endregion

        #region Properties
        /// <summary>
        /// Тип редактора для свойства.
        /// </summary>
        public Type EditorType
        {
            get { return _editorType; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="editorType">Тип редактора для свойства.</param>
        public LotusInspectorTypeEditor(Type editorType)
        {
            _editorType = editorType;
        }
        #endregion
    }
    /**@}*/
}