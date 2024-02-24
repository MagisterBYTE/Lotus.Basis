using System;
using System.IO;
using System.Collections.Generic;
using System.Globalization;

namespace Lotus.Localilzation
{
	/// <summary>
	/// Класс для предоставления пустых локализованных данных.
	/// </summary>
	public class EmptyLocalizeData : ILocalizeData
	{
		/// <summary>
		/// Культура.
		/// </summary>
		public CultureInfo Culture { get; private set; }

		/// <summary>
		/// Путь для загрузки локализованных данных.
		/// </summary>
		/// <remarks>Не используется</remarks>
		public Uri Path { get; private set; }

		/// <summary>
		/// Статус загрузки локализованных данных.
		/// </summary>
		public bool IsLoaded => true;

		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <param name="cultureName">Название языка/культуры в формате ISO 639-1 или ISO 639-3.</param>
		public EmptyLocalizeData(string cultureName)
		{
			Culture = new CultureInfo(cultureName);
		}

		/// <summary>
		/// Индексация локализованных данных по ключу.
		/// </summary>
		/// <param name="key">Ключ.</param>
		/// <returns>Ключ.</returns>
		public string this[string key]
		{
			get
			{
				return key;
			}
		}

		/// <summary>
		/// Перезагрузка локализованных данных.
		/// </summary>
		/// <returns>Статус перезагрузки.</returns>
		public bool Reload()
		{
			return true;
		}

		/// <summary>
		/// Преобразование к текстовому представлению
		/// </summary>
		/// <returns>Наименование культуры</returns>
		public override string ToString()
		{
			return Culture.NativeName;
		}
	}
}
