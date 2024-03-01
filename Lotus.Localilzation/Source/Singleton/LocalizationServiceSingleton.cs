
using System;

namespace Lotus.Localilzation
{
    /// <summary>
    /// Глобальный класс для доступа к сервису локализации
    /// </summary>
    public static class LocalizationServiceSingleton
    {
        private static Func<ILocalizationService> _initConstructor;
        private static ILocalizationService _default;

        /// <summary>
        /// Сервис локализации.
        /// </summary>
        public static ILocalizationService Default
        {
            get
            {
                if (_default != null)
                {
                    return _default;
                }
                else
                {
                    if (_initConstructor != null)
                    {
                        _default = _initConstructor();
                        return _default;
                    }
                    else
                    {
                        return _default!;
                    }
                }
            }
        }

        /// <summary>
        /// Инициализация сервиса локализации.
        /// </summary>
        /// <param name="initConstructor">Метод конструктор для инициализации.</param>
        public static void Init(Func<ILocalizationService> initConstructor)
        {
            _initConstructor = initConstructor;
            _default = _initConstructor();
        }
    }
}