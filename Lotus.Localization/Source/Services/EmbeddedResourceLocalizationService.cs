
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Lotus.Localization
{
    /// <summary>
    /// Сервис реализующий локализацию данных из встроенного ресурса в файле формат Json.
    /// </summary>
    public class EmbeddedResourceJsonLocalizationService : BaseLocalizationService
    {
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <remarks>
        /// Название файлов языка/культуры должно быть в формате ISO 639-1 или ISO 639-3.
        /// </remarks>
        /// <param name="fileNames">Список файлов</param>
        /// <param name="assemblyName">Имя сборки</param>
        public EmbeddedResourceJsonLocalizationService(string[] fileNames, string? assemblyName)
        {
            foreach (var fileName in fileNames)
            {
                var assetCultureData = new EmbeddedResourceJsonLocalizeData(fileName, assemblyName);

                Cultures.Add(assetCultureData);
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
                    var value = service[key];

                    if (string.IsNullOrEmpty(value) == false && value != key)
                    {
                        return value;
                    }
                }

                if (_localizeData != null && _localizeData[key] is string text)
                {
                    return text;
                }

                return key;
            }
        }
    }
}