using System;

namespace Lotus.Core
{
    /**
     * \defgroup CoreUtilities Подсистема утилит
     * \ingroup Core
     * \brief Подсистема утилит содержит различные вспомогательные утилиты которые не относиться к другим подсистемам.
     * @{
     */
    /// <summary>
    /// Статический класс для установки только нового значения и информирования об этом.
    /// </summary>
    /// <remarks>
    /// Применяется когда при изменении свойства требуется выполнить дополнительную работу.
    /// </remarks>
    public static class XValueSet
    {
        /// <summary>
        /// Установка нового значения свойства типа значения.
        /// </summary>
        /// <typeparam name="TType">Тип объекта.</typeparam>
        /// <param name="currentValue">Текущие значение.</param>
        /// <param name="newValue">Новое значение.</param>
        /// <returns>Статус установки нового значения свойства.</returns>
        public static bool SetStruct<TType>(ref TType currentValue, in TType newValue) where TType : struct
        {
            if (currentValue.Equals(newValue))
            {
                return false;
            }

            currentValue = newValue;
            return true;
        }


        /// <summary>
        /// Установка нового значения свойства ссылочного типа.
        /// </summary>
        /// <typeparam name="TType">Тип объекта.</typeparam>
        /// <param name="currentValue">Текущие значение.</param>
        /// <param name="newValue">Новое значение.</param>
        /// <returns>Статус установки нового значения свойства.</returns>
        public static bool SetClass<TType>(ref TType currentValue, in TType newValue) where TType : class
        {
            if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
            {
                return false;
            }

            currentValue = newValue;
            return true;
        }
    }

    /// <summary>
    /// Статический класс для реализации утилизации экземпляра объект и установки нулевой ссылки на объект.
    /// </summary>
    public static class XDisposer
    {
        /// <summary>
        /// Утилизация (освобождение от неуправляемых ресурсов) экземпляра объект и установки нулевой ссылки на объект.
        /// </summary>
        /// <remarks>
        /// Этот метод скрывает любые брошенные исключения, которые могут возникнуть во время утилизации объекта.
        /// </remarks>
        /// <typeparam name="TResource">Тип объекта.</typeparam>
        /// <param name="resource">Ссылка на экземпляр объекта для утилизации.</param>
        public static void SafeDispose<TResource>(ref TResource? resource) where TResource : class
        {
            if (resource == null)
            {
                return;
            }

            var disposer = resource as IDisposable;
            if (disposer != null)
            {
                try
                {
                    disposer.Dispose();
                }
                catch
                {
                }
            }

            resource = null;
        }
    }
    /**@}*/
}