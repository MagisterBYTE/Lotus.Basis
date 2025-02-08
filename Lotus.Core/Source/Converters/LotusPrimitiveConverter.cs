using System;

namespace Lotus.Core
{
    /** \addtogroup CoreConverters
    *@{*/
    /// <summary>
    /// Статический класс реализующий конвертацию в примитивные типы.
    /// </summary>
    public static class XPrimitiveConverter
    {
        /// <summary>
        /// Преобразование объекта к примитивному типу по указанному коду типа.
        /// </summary>
        /// <remarks>
        /// К примитивными данным относятся все числовые типы, строковой тип, логический тип и перечисление.
        /// </remarks>
        /// <param name="typeCode">Код типа.</param>
        /// <param name="value">Значение.</param>
        /// <returns>Значение примитивного типа.</returns>
        public static TPrimitive? ToPrimitive<TPrimitive>(TypeCode typeCode, object value)
        {
            return default;
        }

        /// <summary>
        /// Преобразование текста к примитивному типу по указанному коду типа.
        /// </summary>
        /// <remarks>
        /// К примитивными данным относятся все числовые типы, строковой тип, логический тип и перечисление.
        /// </remarks>
        /// <param name="typeCode">Код типа.</param>
        /// <param name="text">Текстовое значение.</param>
        /// <returns>Значение примитивного типа.</returns>
        public static TPrimitive? ParsePrimitive<TPrimitive>(TypeCode typeCode, string text)
        {
            object? result = null;

            switch (typeCode)
            {
                case TypeCode.Empty:
                    {
                    }
                    break;
                case TypeCode.Object:
                    {
                    }
                    break;
                case TypeCode.DBNull:
                    {
                    }
                    break;
                case TypeCode.Boolean:
                    {
                        result = XBooleanConverter.Parse(text);
                    }
                    break;
                case TypeCode.Char:
                    {
                        result = text[0];
                    }
                    break;
                case TypeCode.SByte:
                    {
                        result = (sbyte)XNumberConverter.ParseInt(text);
                    }
                    break;
                case TypeCode.Byte:
                    {
                        result = (byte)XNumberConverter.ParseInt(text);
                    }
                    break;
                case TypeCode.Int16:
                    {
                        result = (short)XNumberConverter.ParseInt(text);
                    }
                    break;
                case TypeCode.UInt16:
                    {
                        result = (ushort)XNumberConverter.ParseInt(text);
                    }
                    break;
                case TypeCode.Int32:
                    {
                        result = XNumberConverter.ParseInt(text);
                    }
                    break;
                case TypeCode.UInt32:
                    {
                        result = (uint)XNumberConverter.ParseInt(text);
                    }
                    break;
                case TypeCode.Int64:
                    {
                        result = XNumberConverter.ParseLong(text);
                    }
                    break;
                case TypeCode.UInt64:
                    {
                        result = (ulong)XNumberConverter.ParseLong(text);
                    }
                    break;
                case TypeCode.Single:
                    {
                        result = XNumberConverter.ParseSingle(text);
                    }
                    break;
                case TypeCode.Double:
                    {
                        result = XNumberConverter.ParseDouble(text);
                    }
                    break;
                case TypeCode.Decimal:
                    {
                        result = XNumberConverter.ParseDecimal(text);
                    }
                    break;
                case TypeCode.DateTime:
                    {
                        result = XDateTimeConverter.Parse(text);
                    }
                    break;
                case TypeCode.String:
                    {
                        result = text;
                    }
                    break;
                default:
                    break;
            }

            if (result == null)
            {
                return default;
            }

            return (TPrimitive)result;
        }
    }
    /**@}*/
}