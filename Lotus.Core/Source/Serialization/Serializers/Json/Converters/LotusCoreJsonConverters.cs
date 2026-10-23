using System;
using System.Text.Json;
using System.Text.Json.Serialization;

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
        public static readonly ColorConverter Instance = new();
        #endregion

        #region Methods 

        /// <summary>
        /// Чтение свойства.
        /// </summary>
        /// <param name="reader">Читатель Json.</param>
        /// <param name="typeToConvert">Тип объекта.</param>
        /// <param name="options">Опции сериализатора Json.</param>
        /// <returns>Значение свойства.</returns>
        public override TColor Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            // Если токен равен null, возвращаем значение по умолчанию (аналог existingValue в старой реализации)
            if (reader.TokenType == JsonTokenType.Null)
            {
                return default;
            }

            // Получаем строковое значение из JSON
            var value = reader.GetString();

            if (!string.IsNullOrEmpty(value))
            {
                return TColor.DeserializeFromString(value);
            }

            return default;
        }

        /// <summary>
        /// Запись свойства.
        /// </summary>
        /// <param name="writer">Писатель Json.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="options">Опции сериализатора Json.</param>
        public override void Write(Utf8JsonWriter writer, TColor value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }
    /**@}*/
}