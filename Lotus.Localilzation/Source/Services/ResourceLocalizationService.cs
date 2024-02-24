using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Resources;

namespace Lotus.Localilzation
{
	/// <summary>
	/// Сервис реализующий локализацию данных из стандартного ресурса <see cref="ResourceManager"/>
	/// </summary>
	public class ResourceLocalizationService : BaseLocalizationService
	{
		private readonly ResourceManager _resourceManager;

		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <remarks>
		/// Название языка/культуры должно быть в формате ISO 639-1 или ISO 639-3.
		/// </remarks>
		/// <param name="resourceManager">Ресурс менеджер</param>
		/// <param name="cultures">Массив названия языка/культур</param>
		public ResourceLocalizationService(ResourceManager resourceManager, params string[] cultures)
		{
			_resourceManager = resourceManager;

			foreach (var culture in cultures)
			{
				var cultureData = new EmptyLocalizeData(culture);
				Cultures.Add(cultureData);
			}

			SelectCulture(CultureInfo.CurrentCulture.Name);
		}

		/// <summary>
		/// Индексация локализованных данных по ключу.
		/// </summary>
		/// <param name="key">Ключ.</param>
		/// <returns>Локализованные данные или ключ если данные не найдены.</returns>
		public override string this[string key]
		{
			get
			{
				foreach (var service in _extraServices)
				{
					string value = service[key];

					if (string.IsNullOrEmpty(value) == false && value != key)
					{
						return value;
					}
				}

				var text = _resourceManager.GetString(key, _localizeData.Culture);
				if (string.IsNullOrEmpty(text) == false)
				{
					return text;
				}

				return key;
			}
		}
	}
}
