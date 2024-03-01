using System;
using System.Collections.Generic;
using System.IO;

namespace Lotus.Core
{
    /** \addtogroup CoreExtension
	*@{*/
    /// <summary>
    /// Статический класс реализующий методы расширения для бинарного потока.
    /// </summary>
    public static class XBinaryStreamExtension
    {
        #region Const
        /// <summary>
        /// Нулевые данные по значению в контексте записи/чтения ссылочных объектов бинарного потока.
        /// </summary>
        public const int ZERO_DATA = -1;

        /// <summary>
        /// Существующие данные по значению в контексте записи/чтения ссылочных объектов бинарного потока.
        /// </summary>
        public const int EXIST_DATA = 1;

        /// <summary>
        /// Метка успешности.
        /// </summary>
        public const int SUCCESS_LABEL = 198418;
        #endregion

        #region Write methods 
        /// <summary>
        /// Запись структуры DateTime.
        /// </summary>
        /// <param name="writer">Средство записи данных в бинарном формате.</param>
        /// <param name="dateTime">Список целых значений.</param>
        public static void Write(this BinaryWriter writer, DateTime dateTime)
        {
            writer.Write(dateTime.Ticks);
        }

        /// <summary>
        /// Запись списка целых значений.
        /// </summary>
        /// <param name="writer">Средство записи данных в бинарном формате.</param>
        /// <param name="integers">Список целых значений.</param>
        public static void Write(this BinaryWriter writer, IList<int> integers)
        {
            // Записываем данные по порядку
            if (integers != null && integers.Count > 0)
            {
                for (var i = 0; i < integers.Count; i++)
                {
                    writer.Write(integers[i]);
                }
            }
        }

        /// <summary>
        /// Запись списка вещественных значений одинарной точности.
        /// </summary>
        /// <param name="writer">Средство записи данных в бинарном формате.</param>
        /// <param name="floats">Список вещественных значений одинарной точности.</param>
        public static void Write(this BinaryWriter writer, IList<float> floats)
        {
            // Записываем данные по порядку
            if (floats != null && floats.Count > 0)
            {
                for (var i = 0; i < floats.Count; i++)
                {
                    writer.Write(floats[i]);
                }
            }
        }

        /// <summary>
        /// Запись списка вещественных значений двойной точности.
        /// </summary>
        /// <param name="writer">Средство записи данных в бинарном формате.</param>
        /// <param name="doubles">Список вещественных значений двойной точности.</param>
        public static void Write(this BinaryWriter writer, IList<double> doubles)
        {
            // Записываем данные по порядку
            if (doubles != null && doubles.Count > 0)
            {
                for (var i = 0; i < doubles.Count; i++)
                {
                    writer.Write(doubles[i]);
                }
            }
        }

        /// <summary>
        /// Запись списка примитивных типов данных.
        /// </summary>
        /// <param name="writer">Средство записи данных в бинарном формате.</param>
        /// <param name="primitives">Список примитивных данных.</param>
        public static void Write<TPrimitive>(this BinaryWriter writer, IList<TPrimitive> primitives)
        {
            if (primitives != null && primitives.Count > 0)
            {
                var type_item = typeof(TPrimitive);

                // Перечисление
                if (type_item.IsEnum)
                {
                    // Записываем данные по порядку
                    for (var i = 0; i < primitives.Count; i++)
                    {
                        writer.Write((int)(object)primitives[i]!);
                    }
                }
                else
                {
                    var type_code = Type.GetTypeCode(type_item);
                    switch (type_code)
                    {
                        case TypeCode.Empty:
                            break;
                        case TypeCode.Object:
                            break;
                        case TypeCode.DBNull:
                            break;
                        case TypeCode.Boolean:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((bool)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.Char:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((char)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.SByte:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((sbyte)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.Byte:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((byte)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.Int16:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((short)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.UInt16:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((ushort)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.Int32:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((int)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.UInt32:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((uint)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.Int64:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((long)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.UInt64:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((ulong)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.Single:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((float)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.Double:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((double)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.Decimal:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((decimal)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.DateTime:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((DateTime)(object)primitives[i]!);
                                }
                            }
                            break;
                        case TypeCode.String:
                            {
                                // Записываем данные по порядку
                                for (var i = 0; i < primitives.Count; i++)
                                {
                                    writer.Write((string)(object)primitives[i]!);
                                }
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        #endregion

        #region Read methods 
        /// <summary>
        /// Чтение структуры DateTime.
        /// </summary>
        /// <param name="reader">Средство чтения данных в бинарном формате.</param>
        /// <returns>Объект DateTime.</returns>
        public static DateTime ReadDateTime(this BinaryReader reader)
        {
            return DateTime.FromBinary(reader.ReadInt64());
        }

        /// <summary>
        /// Чтение массива целых значений.
        /// </summary>
        /// <param name="reader">Средство чтения данных в бинарном формате.</param>
        /// <param name="count">Количество элементов.</param>
        /// <returns>Массив целых значений.</returns>
        public static int[] ReadIntegers(this BinaryReader reader, int count)
        {
            // Создаем массив
            var integers = new int[count];

            // Читаем данные по порядку
            for (var i = 0; i < count; i++)
            {
                integers[i] = reader.ReadInt32();
            }

            return integers;
        }

        /// <summary>
        /// Чтение массива вещественных значений одинарной точности.
        /// </summary>
        /// <param name="reader">Средство чтения данных в бинарном формате.</param>
        /// <param name="count">Количество элементов.</param>
        /// <returns>Массив вещественных значений одинарной точности.</returns>
        public static float[] ReadFloats(this BinaryReader reader, int count)
        {
            // Создаем массив
            var floats = new float[count];

            // Читаем данные по порядку
            for (var i = 0; i < count; i++)
            {
                floats[i] = reader.ReadSingle();
            }

            return floats;
        }

        /// <summary>
        /// Чтение массива вещественных значений двойной точности.
        /// </summary>
        /// <param name="reader">Средство чтения данных в бинарном формате.</param>
        /// <param name="count">Количество элементов.</param>
        /// <returns>Массив вещественных значений двойной точности.</returns>
        public static double[] ReadDoubles(this BinaryReader reader, int count)
        {
            // Создаем массив
            var doubles = new double[count];

            // Читаем данные по порядку
            for (var i = 0; i < count; i++)
            {
                doubles[i] = reader.ReadDouble();
            }

            return doubles;
        }

        /// <summary>
        /// Чтение массива примитивных типов данных.
        /// </summary>
        /// <remarks>
        /// К примитивными данным относятся все числовые типы, строковой тип, логический тип и перечисление.
        /// </remarks>
        /// <param name="reader">Средство чтения данных в бинарном формате.</param>
        /// <param name="count">Количество элементов.</param>
        /// <returns>Массив примитивных данных.</returns>
        public static TPrimitive[] ReadPimitives<TPrimitive>(this BinaryReader reader, int count)
        {
            var type_item = typeof(TPrimitive);

            // Создаем массив
            var primitives = new TPrimitive[count];

            // Перечисление
            if (type_item.IsEnum)
            {
                // Читаем данные по порядку
                for (var i = 0; i < count; i++)
                {
                    primitives[i] = (TPrimitive)(object)XConverter.ToEnumOfType(type_item, reader.ReadInt32());
                }
            }
            else
            {
                var type_code = Type.GetTypeCode(type_item);
                switch (type_code)
                {
                    case TypeCode.Empty:
                        break;
                    case TypeCode.Object:
                        break;
                    case TypeCode.DBNull:
                        break;
                    case TypeCode.Boolean:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(bool));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.Char:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(char));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.SByte:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(sbyte));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.Byte:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(byte));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.Int16:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(short));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.UInt16:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(ushort));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.Int32:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(ushort));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.UInt32:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(ushort));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.Int64:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(ushort));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.UInt64:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(ushort));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.Single:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(ushort));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.Double:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(ushort));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.Decimal:
                        {
                            var bytes = reader.ReadBytes(count * sizeof(ushort));
                            Buffer.BlockCopy(bytes, 0, primitives, 0, bytes.Length);
                        }
                        break;
                    case TypeCode.DateTime:
                        {
                            for (var i = 0; i < count; i++)
                            {
                                primitives[i] = (TPrimitive)(object)reader.ReadDateTime();
                            }
                        }
                        break;
                    case TypeCode.String:
                        {
                            for (var i = 0; i < count; i++)
                            {
                                primitives[i] = (TPrimitive)(object)reader.ReadString();
                            }
                        }
                        break;
                    default:
                        break;
                }
            }


            return primitives;
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий методы расширения для текстового потока.
    /// </summary>
    public static class XTextStreamExtension
    {
        #region Write methods 
        #endregion

        #region Read methods
        #endregion
    }
    /**@}*/
}