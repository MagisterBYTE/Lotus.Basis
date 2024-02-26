namespace Lotus.Core
{
    /** \addtogroup CoreObjectPool
	*@{*/
    /// <summary>
    /// Интерфейс для определения объекта поддерживающего пул.
    /// </summary>
    /// <remarks>
    /// Максимально общая реализация.
    /// </remarks>
    public interface ILotusPoolObject
    {
        #region Properties
        /// <summary>
        /// Статус объекта из пула.
        /// </summary>
        /// <remarks>
        /// Позволяет определять был ли объект взят из пула и значит его надо вернуть или создан обычным образом.
        /// </remarks>
        bool IsPoolObject { get; }
        #endregion

        #region Methods 
        /// <summary>
        /// Псевдо-конструктор.
        /// </summary>
        /// <remarks>
        /// Вызывается диспетчером пула в момент взятия объекта из пула.
        /// </remarks>
        void OnPoolTake();

        /// <summary>
        /// Псевдо-деструктор.
        /// </summary>
        /// <remarks>
        /// Вызывается диспетчером пула в момент попадания объекта в пул.
        /// </remarks>
        void OnPoolRelease();
        #endregion
    }
    /**@}*/
}