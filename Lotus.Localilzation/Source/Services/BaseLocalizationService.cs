using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace Lotus.Localilzation
{
	/// <summary>
	/// Базовый сервис локализацию данных
	/// </summary>
	public abstract class BaseLocalizationService : ILocalizationService
	{
		protected ILocalizeData _localizeData;
		protected readonly List<ILocalizationService> _extraServices = new List<ILocalizationService>();

		/// <summary>
		/// Список локализованных данных данного сервиса.
		/// </summary>
		public List<ILocalizeData> Cultures { get; } = new List<ILocalizeData>();

		/// <summary>
		/// Текущие локализованные данные
		/// </summary>
		public ILocalizeData CultureData
		{
			get { return _localizeData; }
			set
			{
				_localizeData = value;
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ILocalizeData)));

				// all bind in xaml can be refreshed
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item"));
				PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Item[]"));

				// all bind in code can be refreshed
				OnCultureChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// Событие изменение культуры
		/// </summary>
		public event EventHandler OnCultureChanged;

		/// <summary>
		/// Индексация локализованных данных по ключу.
		/// </summary>
		/// <param name="key">Ключ.</param>
		/// <returns>Локализованные данные или ключ если данные не найдены.</returns>
		public virtual string this[string key]
		{
			get
			{
				return key;
			}
		}

		/// <summary>
		/// Получение массива локализованных данных которые поддерживает данный сервис локализации 
		/// и все его дополнительные сервисы.
		/// </summary>
		/// <returns>Массив локализованных данных.</returns>
		public ILocalizeData[] GetCultures()
		{
			Dictionary<string, ILocalizeData> values = new Dictionary<string, ILocalizeData>();
			foreach (var i in Cultures)
			{
				if (!values.ContainsKey(i.Culture.Name))
				{
					values.Add(i.Culture.Name, i);
				}
			}

			foreach (var i in _extraServices)
			{
				var extraCultures = i.GetCultures();

				foreach (var extra in extraCultures)
				{
					if (!values.ContainsKey(extra.Culture.Name))
					{
						values.Add(extra.Culture.Name, extra);
					}
				}
			}

			return values.Values.ToArray();
		}

		/// <summary>
		/// Выбор текущей языка/культуры по имени.
		/// </summary>
		/// <param name="cultureName">Название языка/культуры в формате ISO 639-1 или ISO 639-3.</param>
		public void SelectCulture(string cultureName)
		{
			foreach (var i in _extraServices)
			{
				i.SelectCulture(cultureName);
			}

			var cultureData = Cultures.Find(x => cultureName.Contains(x.Culture.Name) || x.Culture.Name.Contains(cultureName));

			if(cultureData != null) 
			{
				Console.WriteLine($"Find culture {cultureData.Culture.Name}");
			}
			else
			{
				Console.WriteLine($"NULL");
			}


			if (cultureData == null)
			{
				cultureData = Cultures.Find(x => x.Culture.Name == "en-US");
			}

			if (cultureData != null)
			{
				CultureData = cultureData;

				if (!cultureData.IsLoaded)
				{
					cultureData.Reload();
				}
			}
		}

		#region Extra Services
		/// <summary>
		/// Получение массива дополнительных сервисов локализации связанных с данным сервисом.
		/// </summary>
		/// <returns>Массив дополнительных сервисов локализации.</returns>
		public ILocalizationService[] GetExtraServices()
		{
			return _extraServices.ToArray();
		}

		/// <summary>
		/// Добавление дополнительного сервиса локализации.
		/// </summary>
		/// <param name="service">Сервис локализации.</param>
		public void AddExtraService(ILocalizationService service)
		{
			_extraServices.Add(service);
		}

		/// <summary>
		/// Удаление дополнительного сервиса локализации.
		/// </summary>
		/// <param name="service">Сервис локализации.</param>
		public void RemoveExtraService(ILocalizationService service)
		{
			_extraServices.Remove(service);
		}
		#endregion

		#region Interface INotifyPropertyChanged
		/// <summary>
		/// Событие срабатывает ПОСЛЕ изменения свойства
		/// </summary>
		public event PropertyChangedEventHandler? PropertyChanged;
		#endregion
	}
}
