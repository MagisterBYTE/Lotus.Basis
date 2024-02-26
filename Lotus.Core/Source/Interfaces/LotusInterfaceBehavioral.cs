using System;

namespace Lotus.Core
{
    /** \addtogroup CoreInterfaces
	*@{*/
    /// <summary>
    /// Определение интерфейса для объектов поддерживающих проверку.
    /// </summary>
    /// <typeparam name="TType">Тип объекта.</typeparam>
    public interface ILotusCheckAll<out TType>
    {
        /// <summary>
        /// Проверка объекта на удовлетворение указанного предиката.
        /// </summary>
        /// <remarks>
        /// Объект удовлетворяет условию предиката если каждый его элемент удовлетворяет условию предиката.
        /// </remarks>
        /// <param name="match">Предикат проверки.</param>
        /// <returns>Статус проверки.</returns>
        bool CheckAll(Predicate<TType?> match);
    }

    /// <summary>
    /// Определение интерфейса для объектов поддерживающих проверку.
    /// </summary>
    /// <typeparam name="TType">Тип объекта.</typeparam>
    public interface ILotusCheckOne<out TType>
    {
        /// <summary>
        /// Проверка объекта на удовлетворение указанного предиката.
        /// </summary>
        /// <remarks>
        /// Объект удовлетворяет условию предиката если хотя бы один его элемент удовлетворяет условию предиката.
        /// </remarks>
        /// <param name="match">Предикат проверки.</param>
        /// <returns>Статус проверки.</returns>
        bool CheckOne(Predicate<TType?> match);
    }

    /// <summary>
    /// Определение интерфейса для объектов поддерживающих посещение посетителем.
    /// </summary>
    /// <typeparam name="TType">Тип объекта.</typeparam>
    public interface ILotusVisit<out TType>
    {
        /// <summary>
        /// Посещение элементов списка указанным посетителем.
        /// </summary>
        /// <param name="onVisitor">Делегат посетителя.</param>
        void Visit(Action<TType?> onVisitor);
    }

    /// <summary>
    /// Определение интерфейса для объектов реализующих понятие индекса.
    /// </summary>
    public interface ILotusIndexable
    {
        /// <summary>
        /// Индекс элемента.
        /// </summary>
        int Index { get; set; }
    }
    /**@}*/
}