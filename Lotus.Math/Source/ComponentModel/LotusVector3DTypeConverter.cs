using System;
using System.ComponentModel;
using System.Globalization;

namespace Lotus.Maths
{
    /** \addtogroup MathComponentModel
	*@{*/
    /// <summary>
    /// Конвертер типа <see cref="Vector3D"/> для поддержки задания значения из строки в XAML.
    /// Ожидаемый формат: "X Y Z" или "X;Y;Z" (инвариантная культура).
    /// Пример: Value="1.5 -2.3 0"
    /// </summary>
    public class LotusVector3DTypeConverter : TypeConverter
    {
        private static readonly char[] _separators = [' ', ';', '\t'];

        /// <inheritdoc />
        public override bool CanConvertFrom(ITypeDescriptorContext? context, Type sourceType)
            => sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);

        /// <inheritdoc />
        public override bool CanConvertTo(ITypeDescriptorContext? context, Type? destinationType)
            => destinationType == typeof(string) || base.CanConvertTo(context, destinationType);

        /// <inheritdoc />
        public override object ConvertFrom(ITypeDescriptorContext? context, CultureInfo? culture, object value)
        {
            if (value is not string text)
            {
                return base.ConvertFrom(context, culture, value)!;
            }

            var parts = text.Split(_separators, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 3 &&
                XNumberHelper.TryParseDouble(parts[0], out var x) &&
                XNumberHelper.TryParseDouble(parts[1], out var y) &&
                XNumberHelper.TryParseDouble(parts[2], out var z))
            {
                return new Vector3D(x, y, z);
            }

            throw new FormatException($"Невозможно преобразовать '{text}' в Vector3D. Ожидаемый формат: \"X Y Z\".");
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Vector3D v)
            {
                return $"{v.X.ToString(CultureInfo.InvariantCulture)} {v.Y.ToString(CultureInfo.InvariantCulture)} {v.Z.ToString(CultureInfo.InvariantCulture)}";
            }

            return base.ConvertTo(context, culture, value, destinationType)!;
        }
    }
    /**@}*/
}
