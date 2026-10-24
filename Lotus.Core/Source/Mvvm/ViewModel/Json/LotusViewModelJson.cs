using System;
using System.Text.Json.Nodes;

namespace Lotus.Core
{
    /** \addtogroup CoreViewModel
	*@{*/
    /// <summary>
    /// Класс реализующий минимальный элемент ViewModel данных формата JSON.
    /// </summary>
    public class ViewModelJson : ViewModelHierarchy<JsonObject>
    {
        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="parentItem">Родительский узел.</param>
        public ViewModelJson(JsonObject model, ILotusViewModelHierarchy? parentItem)
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
            if (model is JsonObject jobject)
            {
                return new ViewModelJson(jobject, parent);
            }

            throw new NotImplementedException("Model must be type <JObject>");
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
    public class CollectionViewJson : CollectionViewModelHierarchy<ViewModelJson, JsonObject>
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CollectionViewJson()
            : base(string.Empty)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя коллекции.</param>
        public CollectionViewJson(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя коллекции.</param>
        /// <param name="source">Источник данных.</param>
        public CollectionViewJson(string name, JsonObject source)
            : base(name, source)
        {
        }
        #endregion
    }
    /**@}*/
}