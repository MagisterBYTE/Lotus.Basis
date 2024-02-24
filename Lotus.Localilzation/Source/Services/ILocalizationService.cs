using System;
using System.ComponentModel;

namespace Lotus.Localilzation
{
	/// <summary>
	/// Определение интерфейса для локализации данных.
	/// </summary>
	public interface ILocalizationService : INotifyPropertyChanged
	{
		/// <summary>
		/// Текущие локализованные данные
		/// </summary>
		ILocalizeData CultureData { get; }

		/// <summary>
		/// Событие изменение культуры
		/// </summary>
		event EventHandler OnCultureChanged;

		/// <summary>
		/// Индексация локализованных данных по ключу.
		/// </summary>
		/// <param name="key">Ключ.</param>
		/// <returns>Локализованные данные или ключ если данные не найдены.</returns>
		string this[string key] { get; }

		/// <summary>
		/// Получение массива локализованных данных которые поддерживает данный сервис локализации 
		/// и все его дополнительные сервисы
		/// </summary>
		/// <returns>Массив локализованных данных.</returns>
		ILocalizeData[] GetCultures();

		/// <summary>
		/// Выбор текущей языка/культуры по имени
		/// </summary>
		/// <param name="cultureName">Название языка/культуры в формате ISO 639-1 или ISO 639-3.</param>
		void SelectCulture(string cultureName);

		/// <summary>
		/// Получение массива дополнительных сервисов локализации связанных с данным сервисом.
		/// </summary>
		/// <returns>Массив дополнительных сервисов локализации.</returns>
		ILocalizationService[] GetExtraServices();

		/// <summary>
		/// Добавление дополнительного сервиса локализации.
		/// </summary>
		/// <param name="service">Сервис локализации.</param>
		void AddExtraService(ILocalizationService service);

		/// <summary>
		/// Удаление дополнительного сервиса локализации.
		/// </summary>
		/// <param name="service">Сервис локализации.</param>
		void RemoveExtraService(ILocalizationService service);
	}
}
