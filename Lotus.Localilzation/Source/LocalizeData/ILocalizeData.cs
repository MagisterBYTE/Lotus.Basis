using System;
using System.Globalization;

namespace Lotus.Localilzation
{
	/// <summary>
	/// Интерфейс для предоставления локализованных данных.
	/// </summary>
	public interface ILocalizeData
	{
		/// <summary>
		/// Культура.
		/// </summary>
		CultureInfo Culture { get; }

		/// <summary>
		/// Путь для загрузки локализованных данных.
		/// </summary>
		Uri Path { get; }

		/// <summary>
		/// Статус загрузки локализованных данных.
		/// </summary>
		bool IsLoaded { get; }

		/// <summary>
		/// Индексация локализованных данных по ключу.
		/// </summary>
		/// <param name="key">Ключ.</param>
		/// <returns>Локализованные данные или ключ если данные не найдены.</returns>
		string this[string key] { get; }

		/// <summary>
		/// Перезагрузка локализованных данных.
		/// </summary>
		/// <returns>Статус перезагрузки.</returns>
		bool Reload();
	}
}