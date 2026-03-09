using System;
using System.ComponentModel;
using System.Globalization;

namespace Lotus.Maths
{
    /**
     * \defgroup MathComponentModel Подсистема компонентной модели
     * \ingroup Math
     * \brief Подсистема компонентной модели реализует типы и структуры данных для адаптации к пользовательскому интерфейсу.
     * @{
     */
    /// <summary>
    /// Конвертер типа <see cref="Vector2D"/> для поддержки задания значения из строки в XAML.
    /// Ожидаемый формат: "X Y" или "X;Y" (инвариантная культура).
    /// Пример: Value="1.5 -2.3"
    /// </summary>
    public class LotusVector2DTypeConverter : TypeConverter
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
            if (parts.Length == 2 &&
                XNumberHelper.TryParseDouble(parts[0], out var x) &&
                XNumberHelper.TryParseDouble(parts[1], out var y))
            {
                return new Vector2D(x, y);
            }

            throw new FormatException($"Невозможно преобразовать '{text}' в Vector2D. Ожидаемый формат: \"X Y\".");
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext? context, CultureInfo? culture, object? value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is Vector2D v)
            {
                return $"{v.X.ToString(CultureInfo.InvariantCulture)} {v.Y.ToString(CultureInfo.InvariantCulture)}";
            }

            return base.ConvertTo(context, culture, value, destinationType)!;
        }
    }
    /**@}*/
}
