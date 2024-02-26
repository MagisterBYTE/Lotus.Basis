using System;

using Newtonsoft.Json.Linq;

namespace Lotus.Core
{
    /** \addtogroup CoreViewModel
	*@{*/
    /// <summary>
    /// Класс реализующий минимальный элемент ViewModel данных формата JSON.
    /// </summary>
    public class CViewModelJson : ViewModelHierarchy<JObject>
    {
        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="model">Модель.</param>
        /// <param name="parentItem">Родительский узел.</param>
        public CViewModelJson(JObject model, ILotusViewModelHierarchy? parentItem)
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
            if (model is JObject jobject)
            {
                return new CViewModelJson(jobject, parent);
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
    public class CCollectionViewJson : CollectionViewModelHierarchy<CViewModelJson, JObject>
    {
        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public CCollectionViewJson()
            : base(string.Empty)
        {

        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя коллекции.</param>
        public CCollectionViewJson(string name)
            : base(name)
        {
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="name">Имя коллекции.</param>
        /// <param name="source">Источник данных.</param>
        public CCollectionViewJson(string name, JObject source)
            : base(name, source)
        {
        }
        #endregion
    }
    /**@}*/
}