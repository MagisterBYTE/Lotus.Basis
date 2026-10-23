#if UNITY_2017_1_OR_NEWER
using System;

namespace Lotus.Core
{
    /**
     * \defgroup CoreUnityConsole Подсистема консоли Unity
     * \ingroup Core
     * \brief Подсистема консоли для осуществления отладки, отображения сообщений, отправки команд. Подсистема обеспечивает
        основной механизм отладки и конфигурирования приложения и предоставляет возможность просматривать,
        сортировать и показывать все сообщения приложения, использовать консоль для вызова команд и отправки
        сообщений для дополнительного управления игровым процессом.
     * \details
        ## Возможности/особенности
        1. Показ/скрытие окно консоли, управление списком сообщений (сортировка, очистка)
        2. Исполнение зарегистрированных команд
        3. Отправка сообщений

        ## Использование
        1. Диспетчер консоли можно использовать в ручную(непосредственно вызывать его методы в нужных местах)
        или посредством \ref Lotus.Core.LotusSystemDispatcher
     * @{
     */
    /// <summary>
    /// Класс для хранения информации о сообщении переданного через консоль
    /// </summary>
    public class CMessageConsole : CMessageArgs
    {
        #region Static methods
        /// <summary>
        /// Создание сообщения из строки ввода
        /// </summary>
        /// <param name="inputString">Строка ввода</param>
        /// <returns>Сформированное сообщение</returns>
        public static CMessageConsole CreateFromInputString(string inputString)
        {
            return new CMessageConsole(inputString);
        }
        #endregion

        #region Fields
        // Основное параметры
        protected internal string _argument;
        #endregion

        #region Properties
        /// <summary>
        /// Аргумент сообщения
        /// </summary>
        public string Argument
        {
            get { return _argument; }
            set { _argument = value; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор по умолчанию инициализирует объект класса предустановленными значениями
        /// </summary>
        public CMessageConsole()
        {
        }


        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="name">Имя сообщения</param>
        public CMessageConsole(string name)
            : base(name)
        {
        }


        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="name">Имя сообщения</param>
        /// <param name="argument">Аргумент сообщения</param>
        public CMessageConsole(string name, string argument)
            : base(name)
        {
            _argument = argument;
        }
        #endregion
    }
    /**@}*/
}
#endif
