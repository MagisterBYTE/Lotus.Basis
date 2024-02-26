namespace Lotus.Core
{
    /** \addtogroup CoreMemento
	*@{*/
    /// <summary>
    /// Интерфейс объекта для который имеет внутреннее состояние с возможностью его предоставить и в последующем восстановить.
    /// </summary>
    public interface ILotusMementoOriginator
    {
        /// <summary>
        /// Получить состояние объекта.
        /// </summary>
        /// <param name="stateName">Наименование состояния объекта.</param>
        /// <returns>Состояние объекта.</returns>
        object GetMemento(string stateName);

        /// <summary>
        /// Установить состояние объекта.
        /// </summary>
        /// <param name="memento">Состояние объекта.</param>
        /// <param name="stateName">Наименование состояния объекта.</param>
        void SetMemento(object memento, string stateName);
    }
    /**@}*/
}