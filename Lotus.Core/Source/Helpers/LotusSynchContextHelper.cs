using System;
using System.Threading;

namespace Lotus.Core
{
	/** \addtogroup CoreHelpers
	*@{*/
	/// <summary>
	/// Статический класс реализующий дополнительные методы с контекстом синхронизации.
	/// </summary>
	public static class XSynchContextHelper
	{
		/// <summary>
		/// Запуск действия в том же контексте.
		/// </summary>
		/// <param name="context">Контекстом синхронизации.</param>
		/// <param name="actionToRun">Действие.</param>
		public static void RunWithinContext(this SynchronizationContext context, Action actionToRun)
		{
			if (context == SynchronizationContext.Current || context == null)
			{
				actionToRun?.Invoke();
			}
			else
			{
				context.Send(_ => actionToRun?.Invoke(), null);
			}
		}
	}
	/**@}*/
}