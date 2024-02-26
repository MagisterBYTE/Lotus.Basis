using System;

using Newtonsoft.Json;

namespace Lotus.Core.Serialization
{
    /** \addtogroup CoreSerialization
	*@{*/
    /// <summary>
    /// Реализация конвертера для типа <see cref="TColor"/>.
    /// </summary>
    public class ColorConverter : JsonConverter<TColor>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера.
        /// </summary>
        public static readonly ColorConverter Instance = new ColorConverter();
        #endregion

        #region Properties
        /// <summary>
        /// Возможность прочитать свойство.
        /// </summary>
        public override bool CanRead
        {
            get
            {
                return true;
            }
        }

        /// <summary>
        /// Возможность записать свойство.
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }
        #endregion

        #region Methods 
        /// <summary>
        /// Запись свойства.
        /// </summary>
        /// <param name="writer">Писатель Json.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="serializer">Сериализатор Json.</param>
        public override void WriteJson(JsonWriter writer, TColor value, JsonSerializer serializer)
        {
            writer.WriteValue(value.SerializeToString());
        }

        /// <summary>
        /// Чтение свойства.
        /// </summary>
        /// <param name="reader">Читатель Json.</param>
        /// <param name="objectType">Тип объекта.</param>
        /// <param name="existingValue">Статус существования значение.</param>
        /// <param name="hasExistingValue">Статус существования значение.</param>
        /// <param name="serializer">Сериализатор Json.</param>
        /// <returns>Значение свойства.</returns>
        public override TColor ReadJson(JsonReader reader, Type objectType, TColor existingValue,
                bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                return TColor.DeserializeFromString(reader.Value.ToString()!);
            }

            return existingValue;

        }
        #endregion
    }
}