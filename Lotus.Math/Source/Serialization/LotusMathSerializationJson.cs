using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace Lotus.Maths
{
    /** \addtogroup MathSerialization
	*@{*/
    /// <summary>
    /// Реализация конвертера для типа <see cref="Vector2Df"/>.
    /// </summary>
    public class Vector2DfConverter : JsonConverter<Vector2Df>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера.
        /// </summary>
        public static readonly Vector2DfConverter Instance = new();
        #endregion

        #region Override methods
        /// <summary>
        /// Чтение свойства.
        /// </summary>
        /// <param name="reader">Читатель Json.</param>
        /// <param name="typeToConvert">Тип объекта.</param>
        /// <param name="options">Опции сериализатора Json.</param>
        /// <returns>Значение свойства.</returns>
        public override Vector2Df Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                return Vector2Df.DeserializeFromString(value);
            }

            return default;
        }

        /// <summary>
        /// Запись свойства.
        /// </summary>
        /// <param name="writer">Писатель Json.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="options">Опции сериализатора Json.</param>
        public override void Write(Utf8JsonWriter writer, Vector2Df value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }

    /// <summary>
    /// Реализация конвертера для типа <see cref="Vector2D"/>.
    /// </summary>
    public class Vector2DConverter : JsonConverter<Vector2D>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера.
        /// </summary>
        public static readonly Vector2DConverter Instance = new();
        #endregion

        #region Override methods 
        /// <summary>
        /// Чтение свойства.
        /// </summary>
        /// <param name="reader">Читатель Json.</param>
        /// <param name="typeToConvert">Тип объекта.</param>
        /// <param name="options">Опции сериализатора Json.</param>
        /// <returns>Значение свойства.</returns>
        public override Vector2D Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                return Vector2D.DeserializeFromString(value);
            }

            return default;
        }

        /// <summary>
        /// Запись свойства.
        /// </summary>
        /// <param name="writer">Писатель Json.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="options">Опции сериализатора Json.</param>
        public override void Write(Utf8JsonWriter writer, Vector2D value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }

    /// <summary>
    /// Реализация конвертера для типа <see cref="Vector2Di"/>.
    /// </summary>
    public class Vector2DiConverter : JsonConverter<Vector2Di>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера.
        /// </summary>
        public static readonly Vector2DiConverter Instance = new();
        #endregion

        #region Override methods 
        /// <summary>
        /// Чтение свойства.
        /// </summary>
        /// <param name="reader">Читатель Json.</param>
        /// <param name="typeToConvert">Тип объекта.</param>
        /// <param name="options">Опции сериализатора Json.</param>
        /// <returns>Значение свойства.</returns>
        public override Vector2Di Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                return Vector2Di.DeserializeFromString(value);
            }

            return default;
        }

        /// <summary>
        /// Запись свойства.
        /// </summary>
        /// <param name="writer">Писатель Json.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="options">Опции сериализатора Json.</param>
        public override void Write(Utf8JsonWriter writer, Vector2Di value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }

    /// <summary>
    /// Реализация конвертера для типа <see cref="Vector3Df"/>.
    /// </summary>
    public class Vector3DfConverter : JsonConverter<Vector3Df>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера.
        /// </summary>
        public static readonly Vector3DfConverter Instance = new();
        #endregion

        #region Override methods 
        /// <summary>
        /// Чтение свойства.
        /// </summary>
        /// <param name="reader">Читатель Json.</param>
        /// <param name="typeToConvert">Тип объекта.</param>
        /// <param name="options">Опции сериализатора Json.</param>
        /// <returns>Значение свойства.</returns>
        public override Vector3Df Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                return Vector3Df.DeserializeFromString(value);
            }

            return default;
        }

        /// <summary>
        /// Запись свойства.
        /// </summary>
        /// <param name="writer">Писатель Json.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="options">Опции сериализатора Json.</param>
        public override void Write(Utf8JsonWriter writer, Vector3Df value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }

    /// <summary>
    /// Реализация конвертера для типа <see cref="Vector3D"/>.
    /// </summary>
    public class Vector3DConverter : JsonConverter<Vector3D>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера.
        /// </summary>
        public static readonly Vector3DConverter Instance = new();
        #endregion

        #region Override methods 
        /// <summary>
        /// Чтение свойства.
        /// </summary>
        /// <param name="reader">Читатель Json.</param>
        /// <param name="typeToConvert">Тип объекта.</param>
        /// <param name="options">Опции сериализатора Json.</param>
        /// <returns>Значение свойства.</returns>
        public override Vector3D Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
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
                return Vector3D.DeserializeFromString(value);
            }

            return default;
        }

        /// <summary>
        /// Запись свойства.
        /// </summary>
        /// <param name="writer">Писатель Json.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="options">Опции сериализатора Json.</param>
        public override void Write(Utf8JsonWriter writer, Vector3D value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }
    /**@}*/
}