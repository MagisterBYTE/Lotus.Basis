using System;
using System.Text.Json.Nodes;

namespace Lotus.Core
{
    /** \addtogroup CoreViewModel
	*@{*/
    /// <summary>
    /// Класс реализующий минимальный элемент ViewModel базового объекта
    /// </summary>
    public class ViewModelObject : ViewModelHierarchy<object>
    {
        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="parentItem">Родительский узел.</param>
        public ViewModelObject(object model, ILotusViewModelHierarchy? parentItem)
            : base(model, parentItem)
        {
        }
        #endregion

        #region ILotusViewModelHierarchy methods
        /// <summary>
        /// Создание конкретной ViewModel для указанной модели.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="parent">Родительский элемент ViewModel.</param>
        /// <returns>ViewModel.</returns>
        public override ILotusViewModelHierarchy CreateViewModelHierarchy(object model, ILotusViewModelHierarchy? parent)
        {
            return new ViewModelObject(model, parent);
        }

        /// <summary>
        /// Построение дочерней иерархии согласно источнику данных.
        /// </summary>
        public override void BuildFromModel()
        {
        }
        #endregion
    }

    /// <summary>
    /// Класс для элементов отображения которые поддерживают концепцию просмотра и управления с полноценной.
    /// поддержкой всех операций.
    /// </summary>
    /// <remarks>
    /// Данная коллекции позволяет управлять видимостью данных, обеспечивает их сортировку, группировку, фильтрацию, 
    /// позволяет выбирать данные и производить над ними операции
    /// </remarks>
    public class CollectionViewObject : CollectionViewModelHierarchy<ViewModelObject, object>
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CollectionViewObject()
            : base(string.Empty)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя коллекции.</param>
        public CollectionViewObject(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя коллекции.</param>
        /// <param name="source">Источник данных.</param>
        public CollectionViewObject(string name, object source)
            : base(name, source)
        {
        }
        #endregion
    }
    /**@}*/
}