using System;

namespace Lotus.Core.Inspector
{
    /** \addtogroup CoreInspectorAttribute
	*@{*/
    /// <summary>
    /// Атрибут для расширенного отображения и управления элементами стандартных коллекций и коллекциями Lotus.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class LotusReorderableAttribute : Attribute
    {
        #region Fields
        internal string _titleFieldName;

        // Методы изменения коллекции
        internal string _itemsChangedMethodName;
        internal string _contextMenuMethodName;

        // Методы переупорядочивания коллекции
        internal string _sortAscendingMethodName;
        internal string _sortDescendingMethodName;
        internal string _reorderItemChangedMethodName;

        // Методы для рисования элементов коллекции
        internal string _drawItemMethodName;
        internal string _heightItemMethodName;
        #endregion

        #region Properties
        /// <summary>
        /// Имя поля дочернего свойства выступающее в качестве заголовка для сложных свойств.
        /// </summary>
        public string TitleFieldName
        {
            get { return _titleFieldName; }
            set { _titleFieldName = value; }
        }

        //
        // МЕТОДЫ ИЗМЕНЕНИЯ КОЛЛЕКЦИИ
        //
        /// <summary>
        /// Имя метода вызываемого после изменения количества элементов списка (Добавления / удаления элементов).
        /// </summary>
        /// <remarks>
        /// Метод не должен принимать аргументов.
        /// Пустое значение означает что метод не используется
        /// </remarks>
        public string ItemsChangedMethodName
        {
            get { return _itemsChangedMethodName; }
            set { _itemsChangedMethodName = value; }
        }

        /// <summary>
        /// Имя метода вызываемого для формирования контекстного меню.
        /// </summary>
        /// <remarks>
        /// Метод должен возвращать список элементов соответствующего типа.
        /// Пустое значение означает что метод не используется
        /// </remarks>
        public string ContextMenuMethodName
        {
            get { return _contextMenuMethodName; }
            set { _contextMenuMethodName = value; }
        }

        //
        // МЕТОДЫ ПЕРЕУПОРЯДОЧИВАНИЯ КОЛЛЕКЦИИ
        //
        /// <summary>
        /// Имя метода для сортировки элементов коллекции по возрастанию.
        /// </summary>
        /// <remarks>
        /// Метод не должен принимать аргументов.
        /// Пустое значение означает что сортировка не используется
        /// </remarks>
        public string SortAscendingMethodName
        {
            get { return _sortAscendingMethodName; }
            set { _sortAscendingMethodName = value; }
        }

        /// <summary>
        /// Имя метода для сортировки элементов коллекции по убыванию.
        /// </summary>
        /// <remarks>
        /// Метод не должен принимать аргументов.
        /// Пустое значение означает что сортировка не используется
        /// </remarks>
        public string SortDescendingMethodName
        {
            get { return _sortDescendingMethodName; }
            set { _sortDescendingMethodName = value; }
        }

        /// <summary>
        /// Имя метода вызываемого после изменения порядка элементов (Перемещение элемента, сортировка).
        /// </summary>
        /// <remarks>
        /// Метод должен принимать два аргумента:
        /// 1. Целый тип - индекс предыдущей позиции
        /// 2. Целый тип - индекс новой позиции
        /// Пустое значение означает что метод не используется
        /// </remarks>
        public string ReorderItemChangedMethodName
        {
            get { return _reorderItemChangedMethodName; }
            set { _reorderItemChangedMethodName = value; }
        }

        //
        // МЕТОДЫ ДЛЯ РИСОВАНИЯ ЭЛЕМЕНТОВ КОЛЛЕКЦИИ
        //
        /// <summary>
        /// Имя метода для рисование элемента коллекции.
        /// </summary>
        /// <remarks>
        /// Метод должен принимать два аргумента:
        /// 1. Тип прямоугольник - область отображения элемента
        /// 2. Целый тип - индекс текущего элемента
        /// </remarks>
        public string DrawItemMethodName
        {
            get { return _drawItemMethodName; }
            set { _drawItemMethodName = value; }
        }

        /// <summary>
        /// Имя метода для вычисление высоты элемента.
        /// </summary>
        /// <remarks>
        /// Метод должен принимать один аргумент целого типа обозначающий индекс элемента и возвращать высоту элемента
        /// Пустое значение обозначение что будет принята высота одного контрола
        /// </remarks>
        public string HeightItemMethodName
        {
            get { return _heightItemMethodName; }
            set { _heightItemMethodName = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public LotusReorderableAttribute()
        {
        }
        #endregion
    }
    /**@}*/
}