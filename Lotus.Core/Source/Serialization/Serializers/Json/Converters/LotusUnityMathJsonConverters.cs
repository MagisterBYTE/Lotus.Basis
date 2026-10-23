#if UNITY_2017_1_OR_NEWER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

using UnityEngine;

namespace Lotus.Core.Serialization
{
    /** \addtogroup CoreSerialization
    *@{*/
    /// <summary>
    /// Реализация конвертера для типа <see cref="Color32"/>
    /// </summary>
    public class UnityColor32Converter : JsonConverter<Color32>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера
        /// </summary>
        public static readonly UnityColor32Converter Instance = new UnityColor32Converter();
        #endregion


        #region Main methods
        /// <summary>
        /// Чтение свойства
        /// </summary>
        public override Color32 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var rawValue = reader.GetString();
            return XUnityColor32.DeserializeFromString(rawValue);
        }

        /// <summary>
        /// Запись свойства
        /// </summary>
        public override void Write(Utf8JsonWriter writer, Color32 value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }


    /// <summary>
    /// Реализация конвертера для типа <see cref="Vector2"/>
    /// </summary>
    public class UnityVector2Converter : JsonConverter<Vector2>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера
        /// </summary>
        public static readonly UnityVector2Converter Instance = new UnityVector2Converter();
        #endregion

        #region Main methods
        /// <summary>
        /// Чтение свойства
        /// </summary>
        public override Vector2 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var rawValue = reader.GetString();
            return XUnityVector2.DeserializeFromString(rawValue);
        }

        /// <summary>
        /// Запись свойства
        /// </summary>
        public override void Write(Utf8JsonWriter writer, Vector2 value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }


    /// <summary>
    /// Реализация конвертера для типа <see cref="Vector2Int"/>
    /// </summary>
    public class UnityVector2IntConverter : JsonConverter<Vector2Int>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера
        /// </summary>
        public static readonly UnityVector2IntConverter Instance = new UnityVector2IntConverter();
        #endregion


        #region Main methods
        /// <summary>
        /// Чтение свойства
        /// </summary>
        public override Vector2Int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var rawValue = reader.GetString();
            return XUnityVector2Int.DeserializeFromString(rawValue);
        }

        /// <summary>
        /// Запись свойства
        /// </summary>
        public override void Write(Utf8JsonWriter writer, Vector2Int value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }


    /// <summary>
    /// Реализация конвертера для типа <see cref="Vector3"/>
    /// </summary>
    public class UnityVector3Converter : JsonConverter<Vector3>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера
        /// </summary>
        public static readonly UnityVector3Converter Instance = new UnityVector3Converter();
        #endregion

        #region Main methods
        /// <summary>
        /// Чтение свойства
        /// </summary>
        public override Vector3 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var rawValue = reader.GetString();
            return XUnityVector3.DeserializeFromString(rawValue);
        }

        /// <summary>
        /// Запись свойства
        /// </summary>
        public override void Write(Utf8JsonWriter writer, Vector3 value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }


    /// <summary>
    /// Реализация конвертера для типа <see cref="Vector3Int"/>
    /// </summary>
    public class UnityVector3IntConverter : JsonConverter<Vector3Int>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера
        /// </summary>
        public static readonly UnityVector3IntConverter Instance = new UnityVector3IntConverter();
        #endregion

        #region Main methods
        /// <summary>
        /// Чтение свойства
        /// </summary>
        public override Vector3Int Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var rawValue = reader.GetString();
            return XUnityVector3Int.DeserializeFromString(rawValue);
        }

        /// <summary>
        /// Запись свойства
        /// </summary>
        public override void Write(Utf8JsonWriter writer, Vector3Int value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }


    /// <summary>
    /// Реализация конвертера для типа <see cref="Vector4"/>
    /// </summary>
    public class UnityVector4Converter : JsonConverter<Vector4>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера
        /// </summary>
        public static readonly UnityVector4Converter Instance = new UnityVector4Converter();
        #endregion

        #region Main methods
        /// <summary>
        /// Чтение свойства
        /// </summary>
        public override Vector4 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var rawValue = reader.GetString();
            return XUnityVector4.DeserializeFromString(rawValue);
        }

        /// <summary>
        /// Запись свойства
        /// </summary>
        public override void Write(Utf8JsonWriter writer, Vector4 value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }


    /// <summary>
    /// Реализация конвертера для типа <see cref="Quaternion"/>
    /// </summary>
    public class UnityQuaternionConverter : JsonConverter<Quaternion>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера
        /// </summary>
        public static readonly UnityQuaternionConverter Instance = new UnityQuaternionConverter();
        #endregion

        #region Main methods
        /// <summary>
        /// Чтение свойства
        /// </summary>
        public override Quaternion Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var rawValue = reader.GetString();
            return XUnityQuaternion.DeserializeFromString(rawValue);
        }

        /// <summary>
        /// Запись свойства
        /// </summary>
        public override void Write(Utf8JsonWriter writer, Quaternion value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.SerializeToString());
        }
        #endregion
    }


    /// <summary>
    /// Реализация конвертера для типа <see cref="Matrix4x4"/>
    /// </summary>
    public class UnityMatrix4x4Converter : JsonConverter<Matrix4x4>
    {
        #region Const
        /// <summary>
        /// Глобальный экземпляр конвертера
        /// </summary>
        public static readonly UnityMatrix4x4Converter Instance = new UnityMatrix4x4Converter();

        /// <summary>
        /// Массив имён свойств от m00 до m33
        /// </summary>
        private static readonly string[] Names = GetMemberNames();

        /// <summary>
        /// Словарь соответствия имён свойств их индексам
        /// </summary>
        private static readonly Dictionary<string, int> NamesToIndex = GetNamesToIndex(Names);
        #endregion

        #region Static methods

        /// <summary>
        /// Get the property names include from <c>m00</c> to <c>m33</c>
        /// </summary>
        /// <returns>The property names</returns>
        private static string[] GetMemberNames()
        {
            var indexes = new[] { "0", "1", "2", "3" };
            return indexes.SelectMany((row) => indexes.Select((column) => "m" + column + row)).ToArray();
        }

        /// <summary>
        /// Создание словаря соответствия
        /// </summary>
        private static Dictionary<string, int> GetNamesToIndex(string[] names)
        {
            var dict = new Dictionary<string, int>();
            for (var i = 0; i < names.Length; i++)
            {
                dict[names[i]] = i;
            }
            return dict;
        }
        #endregion

        #region Main methods

        /// <summary>
        /// Чтение свойства
        /// </summary>
        public override Matrix4x4 Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException($"Expected StartObject, but got {reader.TokenType}");
            }

            var matrix = new Matrix4x4();

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return matrix;
                }

                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    var propertyName = reader.GetString();

                    // Переходим к значению свойства
                    reader.Read();

                    if (NamesToIndex.TryGetValue(propertyName, out var index))
                    {
                        matrix[index] = reader.GetSingle(); // Используем GetSingle() для типа float
                    }
                    else
                    {
                        // Пропускаем неизвестное свойство, если оно есть в JSON
                        reader.Skip();
                    }
                }
            }

            throw new JsonException("Expected EndObject before end of stream.");
        }

        /// <summary>
        /// Запись свойства
        /// </summary>
        public override void Write(Utf8JsonWriter writer, Matrix4x4 value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            for (var i = 0; i < Names.Length; i++)
            {
                writer.WritePropertyName(Names[i]);
                writer.WriteNumberValue(value[i]);
            }
            writer.WriteEndObject();
        }
        #endregion
    }
}
#endif