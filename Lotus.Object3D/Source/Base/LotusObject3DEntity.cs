//---------------------------------------------------------------------------------------------------------------------
#if USE_WINDOWS
using System.Windows;
using System.Windows.Media;
using Media3D = System.Windows.Media.Media3D;
#endif

using Lotus.Core;

#nullable disable

namespace Lotus.Object3D
{
    /** \addtogroup Object3DBase
	*@{*/
    /// <summary>
    /// Базовая сущность в подсистеме 3D объекта для формирование иерархической структуры трехмерной сцены.
    /// </summary>
    public class Entity3D : CNameableInt, ILotusViewModelOwner, ILotusViewModelBuilder
    {
        #region Fields
        protected internal ILotusViewModel _ownerViewModel;
        #endregion

        #region Properties ILotusViewModelOwner 
        /// <summary>
        /// ViewModel.
        /// </summary>
        public ILotusViewModel OwnerViewModel
        {
            get { return _ownerViewModel; }
            set
            {
                _ownerViewModel = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями.
        /// </summary>
        public Entity3D()
        {

        }

        /// <summary>
        /// Конструктор инициализирует данные узла указанными значениями.
        /// </summary>
        /// <param name="name">Название узла.</param>
        /// <param name="id">Идентификатор узла.</param>
        public Entity3D(string name, int id)
        {
            _name = name;
            _id = id;
        }
        #endregion

        #region System methods
        /// <summary>
        /// Преобразование к текстовому представлению.
        /// </summary>
        /// <returns>Текстовое представление узла.</returns>
        public override string ToString()
        {
            return _name;
        }
        #endregion

        #region ILotusViewModelBuilder methods
        /// <summary>
        /// Получение количества дочерних узлов.
        /// </summary>
        /// <returns>Количество дочерних узлов.</returns>
        public virtual int GetCountChildrenNode()
        {
            return 0;
        }

        /// <summary>
        /// Получение дочернего узла по индексу.
        /// </summary>
        /// <param name="index">Индекс дочернего узла.</param>
        /// <returns>Дочерней узел.</returns>
        public virtual object GetChildrenNode(int index)
        {
            return null;
        }
        #endregion
    }
    /**@}*/
}