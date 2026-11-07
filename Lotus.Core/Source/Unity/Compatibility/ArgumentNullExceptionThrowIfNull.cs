using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Lotus.Core
{
    /// <summary>
    /// Совместимая обёртка над ArgumentNullException.ThrowIfNull (недоступен в Unity).
    /// </summary>
    internal static class XArgumentNull
    {
        /// <summary>
        /// Бросает <see cref="ArgumentNullException"/>, если аргумент равен null.
        /// </summary>
        /// <param name="argument">Проверяемый аргумент.</param>
        /// <param name="paramName">Имя параметра.</param>
#if UNITY_2017_1_OR_NEWER
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull(object? argument, string? paramName)
        {
            if (argument is null)
            {
                throw new ArgumentNullException(paramName);
            }
        }
#else
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void ThrowIfNull([NotNull] object? argument, string? paramName)
        {
            ArgumentNullException.ThrowIfNull(argument, paramName);
        }
#endif
    }
}
