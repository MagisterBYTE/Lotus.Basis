using System;

using Newtonsoft.Json;

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

        #region Override methods
        /// <summary>
        /// Запись свойства.
        /// </summary>
        /// <param name="writer">Писатель Json.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="serializer">Сериализатор Json.</param>
        public override void WriteJson(JsonWriter writer, Vector2Df value, JsonSerializer serializer)
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
        public override Vector2Df ReadJson(JsonReader reader, Type objectType, Vector2Df existingValue,
                bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                return Vector2Df.DeserializeFromString(reader.Value.ToString()!);
            }

            return existingValue;
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

        #region Override methods 
        /// <summary>
        /// Запись свойства.
        /// </summary>
        /// <param name="writer">Писатель Json.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="serializer">Сериализатор Json.</param>
        public override void WriteJson(JsonWriter writer, Vector2D value, JsonSerializer serializer)
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
        public override Vector2D ReadJson(JsonReader reader, Type objectType, Vector2D existingValue,
                bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                return Vector2D.DeserializeFromString(reader.Value.ToString()!);
            }

            return existingValue;
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

        #region Override methods 
        /// <summary>
        /// Запись свойства.
        /// </summary>
        /// <param name="writer">Писатель Json.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="serializer">Сериализатор Json.</param>
        public override void WriteJson(JsonWriter writer, Vector2Di value, JsonSerializer serializer)
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
        //-------------------------------------------------------------------------------------------------------
        public override Vector2Di ReadJson(JsonReader reader, Type objectType, Vector2Di existingValue,
                bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                return Vector2Di.DeserializeFromString(reader.Value.ToString()!);
            }

            return existingValue;
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

        #region Override methods 
        /// <summary>
        /// Запись свойства.
        /// </summary>
        /// <param name="writer">Писатель Json.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="serializer">Сериализатор Json.</param>
        public override void WriteJson(JsonWriter writer, Vector3Df value, JsonSerializer serializer)
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
        public override Vector3Df ReadJson(JsonReader reader, Type objectType, Vector3Df existingValue,
                bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                return Vector3Df.DeserializeFromString(reader.Value.ToString()!);
            }

            return existingValue;
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

        #region Override methods 
        /// <summary>
        /// Запись свойства.
        /// </summary>
        /// <param name="writer">Писатель Json.</param>
        /// <param name="value">Значение свойства.</param>
        /// <param name="serializer">Сериализатор Json.</param>
        public override void WriteJson(JsonWriter writer, Vector3D value, JsonSerializer serializer)
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
        public override Vector3D ReadJson(JsonReader reader, Type objectType, Vector3D existingValue,
                bool hasExistingValue, JsonSerializer serializer)
        {
            if (reader.Value != null)
            {
                return Vector3D.DeserializeFromString(reader.Value.ToString()!);
            }

            return existingValue;
        }
        #endregion
    }
    /**@}*/
}