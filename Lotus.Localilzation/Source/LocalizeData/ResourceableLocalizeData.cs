using System;
using System.Collections.Generic;
using System.Globalization;

namespace Lotus.Localilzation
{
	/// <summary>
	/// Базовый класс для предоставления локализованных данных которые могут быть загружены из различных источников.
	/// </summary>
	public abstract class ResourceableLocalizeData : ILocalizeData
	{
		#region Static methods
		/// <summary>
		/// Чтение данных по локализации со строки в формате JSON.
		/// </summary>
		/// <param name="json">Строка в формате JSON.</param>
		/// <returns>Словарь локализованных данных.</returns>
		public static Dictionary<string, string> ReadJsonStringDictionary(string json)
		{
			var dict = new Dictionary<string, string>();

			var configs = json.Split('\r', '\n', StringSplitOptions.RemoveEmptyEntries);

			foreach (var line in configs)
			{
				var configLine = line.Trim();

				if (!configLine.Contains(":") || configLine.StartsWith('\\'))
					continue;

                int endPos;
                var key = PickStringToken(configLine, 0, out endPos);

				if (key == null)
					continue;

				var value = PickStringToken(configLine, endPos + 1, out _);

				if (value == null)
					continue;

				dict.Add(key, value);
			}

			return dict;
		}

		/// <summary>
		/// Получить токен
		/// </summary>
		/// <param name="line">Строка</param>
		/// <param name="startPos">Начальная позиция</param>
		/// <param name="endPos">Конечная позиция</param>
		/// <returns>Токен</returns>
		private static string? PickStringToken(string line, int startPos, out int endPos)
		{
			var begin = -1;
			var escape = -1;
			endPos = -1;

			for (var i = startPos; i < line.Length; ++i)
			{
				var ch = line[i];

				if (ch == '"')
				{
					if (escape == i - 1 && escape != -1)
					{
						escape = -1;
					}
					else if (begin == -1)
					{
						begin = i;
					}
					else if (begin != -1)
					{
						endPos = i;

						return line.Substring(begin + 1, endPos - begin - 1);
					}
				}
				else if (ch == '\\')
				{
					escape = i;
				}
			}

			return null;
		}
		#endregion

		/// <summary>
		/// Словарь локализованных данных
		/// </summary>
		protected Dictionary<string, string>? LocalTexts = null;

		/// <summary>
		/// Культура.
		/// </summary>
		public CultureInfo Culture { get; private set; }

		/// <summary>
		/// Путь для загрузки локализованных данных.
		/// </summary>
		public Uri Path { get; private set; }

		/// <summary>
		/// Статус загрузки локализованных данных.
		/// </summary>
		public bool IsLoaded => LocalTexts != null;

		/// <summary>
		/// Конструктор инициализирует объект класса указанными параметрами.
		/// </summary>
		/// <remarks>
		/// Название файла должно совпадать с названием языка/культуры в формате ISO 639-1 или ISO 639-3.
		/// </remarks>
		/// <param name="uri">Путь длязагрузки ресурсов</param>
		protected ResourceableLocalizeData(Uri uri)
		{
			var localPath = uri.IsAbsoluteUri ? uri.LocalPath : uri.OriginalString;
			Culture = new CultureInfo(System.IO.Path.GetFileNameWithoutExtension(localPath));
			Path = uri;
		}

		/// <summary>
		/// Индексация локализованных данных по ключу.
		/// </summary>
		/// <param name="key">Ключ.</param>
		/// <returns>Локализованные данные или ключ если данные не найдены.</returns>
		public string this[string key]
		{
			get
			{
				if (LocalTexts != null && LocalTexts.TryGetValue(key, out var text))
				{
					return text;
				}

				return key;
			}
		}

		/// <summary>
		/// Перезагрузка локализованных данных.
		/// </summary>
		/// <returns>Статус перезагрузки.</returns>
		public abstract bool Reload();

		/// <summary>
		/// Преобразование к текстовому представлению.
		/// </summary>
		/// <returns>Наименование культуры.</returns>
		public override string ToString()
		{
			return Culture.NativeName;
		}
	}
}