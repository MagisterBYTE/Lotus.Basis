using System;

namespace Lotus.Core
{
    /** \addtogroup CoreExtension
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы расширений с делегатами.
    /// </summary>
    public static class XDelegateExtension
    {
        /// <summary>
        /// Проверка на наличие обработчик в списке делегатов.
        /// </summary>
        /// <param name="this">Делегат.</param>
        /// <param name="delegat">Проверяемый делегат.</param>
        /// <returns>Статус наличия.</returns>
        public static bool HasHandler(this Action @this, Action delegat)
        {
            return @this.GetInvocationList().ContainsElement(delegat);
        }
    }
    /**@}*/
}