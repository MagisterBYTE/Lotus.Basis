using System.Collections.Generic;
using System.Text;

namespace Lotus.Core
{
    /** \addtogroup CoreHumanizer
    *@{*/
    /// <summary>
    /// Методы расширения интерфейса <see cref="ILotusPersonInfo"/> для получения различных форматов имени пользователя.
    /// </summary>
    public static class XPersonInfoExtensions
    {
        /// <summary>
        /// Возвращает краткое имя в формате "Фамилия И. О." (официальный формат).
        /// Пример: "Иванов И. И." или "Иванов А." при отсутствии отчества.
        /// </summary>
        /// <param name="personInfo">Объект с персональными данными пользователя.</param>
        /// <param name="skipIfNameEmpty">
        /// Если true, возвращает пустую строку при отсутствии имени.
        /// Если false, пытается сформировать имя только из фамилии.
        /// </param>
        /// <returns>
        /// Отформатированное краткое имя или пустая строка, если недостаточно данных.
        /// </returns>
        /// <remarks>
        /// Алгоритм:
        /// 1. Приоритет: Фамилия + инициалы имени и отчества
        /// 2. Если имя отсутствует, но указана фамилия и skipIfNameEmpty=false - возвращается только фамилия
        /// 3. Если данных недостаточно - возвращается пустая строка
        /// </remarks>
        public static string GetShortName(this ILotusPersonInfo? personInfo, bool skipIfNameEmpty = true)
        {
            if (personInfo == null)
                return string.Empty;

            // Проверяем наличие минимально необходимых данных
            if (string.IsNullOrWhiteSpace(personInfo.Surname))
                return string.Empty;

            if (string.IsNullOrWhiteSpace(personInfo.Name) && skipIfNameEmpty)
                return string.Empty;

            var builder = new StringBuilder();

            // Всегда начинаем с фамилии
            builder.Append(personInfo.Surname.Trim());

            // Добавляем инициал имени, если имя указано
            if (!string.IsNullOrWhiteSpace(personInfo.Name))
            {
                builder.Append(' ');
                builder.Append(personInfo.Name.Trim()[0]);
                builder.Append('.');

                // Добавляем инициал отчества, если оно указано
                if (!string.IsNullOrWhiteSpace(personInfo.Patronymic))
                {
                    builder.Append(' ');
                    builder.Append(personInfo.Patronymic.Trim()[0]);
                    builder.Append('.');
                }
            }
            else if (!skipIfNameEmpty)
            {
                // Если имя не указано, но skipIfNameEmpty=false - возвращаем только фамилию
                return builder.ToString();
            }

            return builder.ToString();
        }

        /// <summary>
        /// Возвращает полное имя в формате "Фамилия Имя Отчество".
        /// Пример: "Иванов Иван Иванович".
        /// </summary>
        /// <param name="personInfo">Объект с персональными данными пользователя.</param>
        /// <param name="skipMiddleNameIfEmpty">
        /// Если true, пропускает отчество при его отсутствии (формат "Фамилия Имя").
        /// Если false, всегда использует трехчастный формат, даже если отчество пустое.
        /// </param>
        /// <returns>
        /// Отформатированное полное имя или пустая строка, если недостаточно данных.
        /// </returns>
        /// <remarks>
        /// Алгоритм:
        /// 1. Минимально необходимые данные: Фамилия и Имя
        /// 2. Отчество добавляется при наличии или если skipMiddleNameIfEmpty=false
        /// 3. Все части разделяются одним пробелом
        /// </remarks>
        public static string GetFullName(this ILotusPersonInfo? personInfo, bool skipMiddleNameIfEmpty = true)
        {
            if (personInfo == null)
                return string.Empty;

            // Проверяем наличие обязательных данных
            if (string.IsNullOrWhiteSpace(personInfo.Surname) ||
                string.IsNullOrWhiteSpace(personInfo.Name))
                return string.Empty;

            var parts = new List<string>
            {
                personInfo.Surname.Trim(),
                personInfo.Name.Trim()
            };

            // Обрабатываем отчество в зависимости от флага
            bool hasPatronymic = !string.IsNullOrWhiteSpace(personInfo.Patronymic);

            if (hasPatronymic)
            {
                parts.Add(personInfo.Patronymic.Trim());
            }
            else if (!skipMiddleNameIfEmpty)
            {
                // Если нужно всегда показывать три части, добавляем пустое отчество
                parts.Add(string.Empty);
            }

            return string.Join(" ", parts);
        }

        /// <summary>
        /// Возвращает отображаемое имя в неформальном стиле "Имя Фамилия".
        /// Пример: "Иван Иванов".
        /// </summary>
        /// <param name="personInfo">Объект с персональными данными пользователя.</param>
        /// <param name="includePatronymic">
        /// Если true, добавляет отчество в формате "Иван Иванович Иванов" (для некоторых культур).
        /// По умолчанию false.
        /// </param>
        /// <param name="fallbackToShortName">
        /// Если true, при отсутствии имени возвращает краткий формат (фамилию с инициалами).
        /// Если false, возвращает пустую строку.
        /// </param>
        /// <returns>
        /// Удобочитаемое имя для отображения в интерфейсах.
        /// </returns>
        /// <remarks>
        /// Алгоритм:
        /// 1. Основной формат: Имя + Фамилия (западный стиль)
        /// 2. При необходимости можно включить отчество (менее распространено)
        /// 3. Имеет fallback на краткое имя при отсутствии имени
        /// </remarks>
        public static string GetDisplayName(this ILotusPersonInfo? personInfo,
                                            bool includePatronymic = false,
                                            bool fallbackToShortName = true)
        {
            if (personInfo == null)
                return string.Empty;

            // Проверяем наличие имени - основного для DisplayName
            if (string.IsNullOrWhiteSpace(personInfo.Name))
            {
                // Fallback на краткое имя, если разрешено
                return fallbackToShortName ? personInfo.GetShortName(false) : string.Empty;
            }

            var builder = new StringBuilder();

            // Всегда начинаем с имени
            builder.Append(personInfo.Name.Trim());

            // Добавляем отчество, если требуется
            if (includePatronymic && !string.IsNullOrWhiteSpace(personInfo.Patronymic))
            {
                builder.Append(' ');
                builder.Append(personInfo.Patronymic.Trim());
            }

            // Добавляем фамилию, если она есть
            if (!string.IsNullOrWhiteSpace(personInfo.Surname))
            {
                builder.Append(' ');
                builder.Append(personInfo.Surname.Trim());
            }

            return builder.ToString();
        }

        /// <summary>
        /// Возвращает инициалы в формате "ИИ" (первые буквы имени и фамилии).
        /// Используется для аватаров, сокращений.
        /// Пример: "ИИ" для Ивана Иванова.
        /// </summary>
        /// <param name="personInfo">Объект с персональными данными пользователя.</param>
        /// <returns>Двухбуквенные инициалы или пустая строка.</returns>
        public static string GetInitials(this ILotusPersonInfo? personInfo)
        {
            if (personInfo == null)
                return string.Empty;

            var initials = new StringBuilder(2);

            if (!string.IsNullOrWhiteSpace(personInfo.Name))
                initials.Append(personInfo.Name.Trim()[0]);

            if (!string.IsNullOrWhiteSpace(personInfo.Surname))
                initials.Append(personInfo.Surname.Trim()[0]);

            return initials.ToString().ToUpperInvariant();
        }

        /// <summary>
        /// Универсальный метод для получения имени в указанном формате.
        /// </summary>
        /// <param name="personInfo">Объект с персональными данными пользователя.</param>
        /// <param name="format">Формат имени: Short, Full, Display, или Initials.</param>
        /// <returns>Имя в запрошенном формате.</returns>
        public static string GetFormattedName(this ILotusPersonInfo? personInfo, TPersonNameFormat format)
        {
            return format switch
            {
                TPersonNameFormat.Short => personInfo.GetShortName(),
                TPersonNameFormat.Full => personInfo.GetFullName(),
                TPersonNameFormat.Display => personInfo.GetDisplayName(),
                TPersonNameFormat.Initials => personInfo.GetInitials(),
                _ => personInfo.GetDisplayName()
            };
        }
    }
    /**@}*/
}
