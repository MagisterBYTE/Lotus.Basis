namespace Lotus.Core
{
    /**
     * \defgroup CoreFileSystem Подсистема файловой системы
     * \ingroup Core
     * \brief Подсистема файловой системы реализует концепцию элементов файловой системы как иерархическую модель данных.
     * @{
     */
    /// <summary>
    /// Интерфейс представляющий собой базовую сущность элемента файловой системы.
    /// </summary>
    public interface ILotusFileSystemEntity : ILotusNameable, ILotusCheckOne<ILotusFileSystemEntity>
    {
        /// <summary>
        /// Полное имя(полный путь) элемента файловой системы.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Удаление элемента файловой системы.
        /// </summary>
        void Delete();
    }
    /**@}*/
}