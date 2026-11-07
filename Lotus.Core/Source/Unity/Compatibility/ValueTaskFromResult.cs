using System.Threading.Tasks;

namespace Lotus.Core
{
    /// <summary>
    /// Совместимая обёртка над ValueTask.FromResult (недоступен в части версий Unity).
    /// </summary>
    internal static class XValueTask
    {
        /// <summary>
        /// Создаёт успешно завершённый <see cref="ValueTask{TResult}"/>.
        /// </summary>
        /// <typeparam name="TResult">Тип результата.</typeparam>
        /// <param name="result">Результат.</param>
        /// <returns>ValueTask с результатом.</returns>
#if UNITY_2017_1_OR_NEWER
        public static ValueTask<TResult> FromResult<TResult>(TResult result)
        {
            return new ValueTask<TResult>(result);
        }
#else
        public static ValueTask<TResult> FromResult<TResult>(TResult result)
        {
            return ValueTask.FromResult(result);
        }
#endif
    }
}
