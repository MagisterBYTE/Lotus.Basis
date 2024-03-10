using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Lotus.Localization
{
    /// <summary>
    /// Класс для локализованных данных которые содержаться во встроенном ресурсе в файле формат Json.
    /// </summary>
    /// <remarks>
    /// Имя файла должно совпадать с названиям культуры.
    /// </remarks>
    public class EmbeddedResourceJsonLocalizeData : ResourceableLocalizeData
    {
        /// <summary>
        /// Имя сборки из которой загружается файл локализации.
        /// </summary>
        private readonly string? _assemblyName;

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами.
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="assemblyName">Имя сборки</param>
        /// <param name="autoLoad">Статус автозагрузки</param>
        public EmbeddedResourceJsonLocalizeData(string fileName, string? assemblyName, bool autoLoad = false) :
            base(new Uri(fileName, UriKind.Relative))
        {
            _assemblyName = assemblyName;
            if (autoLoad)
            {
                Reload();
            }
        }

        /// <summary>
        /// Перезагрузка локализованных данных.
        /// </summary>
        /// <returns>Статус перезагрузки.</returns>
        public override bool Reload()
        {
            try
            {
                var current = _assemblyName == null ? Assembly.GetExecutingAssembly() : Assembly.Load(_assemblyName);
                var localPath = Path.IsAbsoluteUri ? Path.LocalPath : Path.OriginalString;
                var nameCulture = current.GetManifestResourceNames().First(x => x.Contains(localPath));
                using var resourceStream = current.GetManifestResourceStream(nameCulture);
                if (resourceStream != null)
                {
                    using (var sr = new StreamReader(resourceStream))
                    {
                        var tempDict = ReadJsonStringDictionary(sr.ReadToEnd());

                        if (tempDict != null)
                        {
                            LocalTexts = tempDict;
                        }
                    }
                }

                return LocalTexts != null;
            }
            catch
            {
                return false;
            }
        }
    }
}