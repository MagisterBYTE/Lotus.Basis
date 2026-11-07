using System;
using System.ComponentModel;
using System.Reflection;

namespace Lotus.Core.CommonTypes
{
    /// <summary>
    /// Тесты для <see cref="CVariant"/>.
    /// </summary>
    public class VariantTests
    {
        /// <summary>
        /// Тест метода GetHashCode - исправленный метод без бесконечной рекурсии.
        /// </summary>
        [Fact]
        public void GetHashCode_DoesNotCauseInfiniteRecursion()
        {
            var variant1 = new CVariant();
            variant1.IntegerValue = 100;

            var variant2 = new CVariant();
            variant2.IntegerValue = 100;

            // Должно работать без исключений и рекурсии
            var hash1 = variant1.GetHashCode();
            var hash2 = variant2.GetHashCode();

            Assert.Equal(hash1, hash2);
        }

        /// <summary>
        /// Тест метода GetHashCode - возвращает разные хеши для разных значений.
        /// </summary>
        [Fact]
        public void GetHashCode_ReturnsDifferentHashesForDifferentValues()
        {
            var variant1 = new CVariant();
            variant1.IntegerValue = 100;

            var variant2 = new CVariant();
            variant2.IntegerValue = 200;

            var hash1 = variant1.GetHashCode();
            var hash2 = variant2.GetHashCode();

            Assert.NotEqual(hash1, hash2);
        }

        /// <summary>
        /// Тест метода DeserializeFromString - реализованный метод парсит тип.
        /// </summary>
        [Fact]
        public void DeserializeFromString_WithValidFormat_ParsesType()
        {
            var variant = CVariant.DeserializeFromString("[Integer]");

            Assert.NotNull(variant);
            // Проверяем, что тип был установлен через свойство ValueType
            Assert.Equal(TValueType.Integer, variant.ValueType);
        }

        /// <summary>
        /// Тест метода DeserializeFromString - с пустой строкой возвращает пустой вариант.
        /// </summary>
        [Fact]
        public void DeserializeFromString_WithEmptyString_ReturnsEmptyVariant()
        {
            var variant = CVariant.DeserializeFromString("");

            Assert.NotNull(variant);
            // Пустой вариант имеет тип Void по умолчанию
            Assert.Equal(TValueType.Void, variant.ValueType);
        }

        /// <summary>
        /// Тест метода DeserializeFromString - с null возвращает пустой вариант.
        /// </summary>
        [Fact]
        public void DeserializeFromString_WithNull_ReturnsEmptyVariant()
        {
            var variant = CVariant.DeserializeFromString(null!);

            Assert.NotNull(variant);
            // Пустой вариант имеет тип Void по умолчанию
            Assert.Equal(TValueType.Void, variant.ValueType);
        }

        /// <summary>
        /// Тест метода DeserializeFromString - с нераспознанным форматом возвращает пустой вариант.
        /// </summary>
        [Fact]
        public void DeserializeFromString_WithInvalidFormat_ReturnsEmptyVariant()
        {
            var variant = CVariant.DeserializeFromString("InvalidFormat");

            Assert.NotNull(variant);
            // Нераспознанный формат возвращает пустой вариант с типом Void
            Assert.Equal(TValueType.Void, variant.ValueType);
        }

        /// <summary>
        /// Тест уведомлений о свойствах - BooleanValue.
        /// </summary>
        [Fact]
        public void BooleanValue_NotifiesOwner()
        {
            var notified = false;
            var notifiedValue = false;
            var notifiedProperty = string.Empty;

            var variant = new CVariant();
            variant.IOwner = new TestOwner((sender, value, property) =>
            {
                notified = true;
                notifiedValue = (bool)value;
                notifiedProperty = property;
            });

            variant.BooleanValue = true;

            Assert.True(notified);
            Assert.True(notifiedValue);
            Assert.Equal(nameof(CVariant.BooleanValue), notifiedProperty);
        }

        /// <summary>
        /// Тест уведомлений о свойствах - IntegerValue.
        /// </summary>
        [Fact]
        public void IntegerValue_NotifiesOwner()
        {
            var notified = false;
            var notifiedValue = 0;
            var notifiedProperty = string.Empty;

            var variant = new CVariant();
            variant.IOwner = new TestOwner((sender, value, property) =>
            {
                notified = true;
                notifiedValue = (int)value;
                notifiedProperty = property;
            });

            variant.IntegerValue = 42;

            Assert.True(notified);
            Assert.Equal(42, notifiedValue);
            Assert.Equal(nameof(CVariant.IntegerValue), notifiedProperty);
        }

        /// <summary>
        /// Тест уведомлений о свойствах - StringValue.
        /// </summary>
        [Fact]
        public void StringValue_NotifiesOwner()
        {
            var notified = false;
            var notifiedValue = string.Empty;
            var notifiedProperty = string.Empty;

            var variant = new CVariant();
            variant.IOwner = new TestOwner((sender, value, property) =>
            {
                notified = true;
                notifiedValue = (string)value;
                notifiedProperty = property;
            });

            variant.StringValue = "Test";

            Assert.True(notified);
            Assert.Equal("Test", notifiedValue);
            Assert.Equal(nameof(CVariant.StringValue), notifiedProperty);
        }

        /// <summary>
        /// Тест различных типов значений - Boolean.
        /// </summary>
        [Fact]
        public void BooleanValue_SetsCorrectType()
        {
            var variant = new CVariant();
            variant.BooleanValue = true;

            Assert.Equal(TValueType.Boolean, variant.ValueType);
            Assert.True(variant.BooleanValue);
        }

        /// <summary>
        /// Тест различных типов значений - Integer.
        /// </summary>
        [Fact]
        public void IntegerValue_SetsCorrectType()
        {
            var variant = new CVariant();
            variant.IntegerValue = 42;

            Assert.Equal(TValueType.Integer, variant.ValueType);
            Assert.Equal(42, variant.IntegerValue);
        }

        /// <summary>
        /// Тест различных типов значений - Float.
        /// </summary>
        [Fact]
        public void FloatValue_SetsCorrectType()
        {
            var variant = new CVariant();
            variant.FloatValue = 3.14f;

            Assert.Equal(TValueType.Float, variant.ValueType);
            Assert.Equal(3.14f, variant.FloatValue, 0.001f);
        }

        /// <summary>
        /// Тест различных типов значений - String.
        /// </summary>
        [Fact]
        public void StringValue_SetsCorrectType()
        {
            var variant = new CVariant();
            variant.StringValue = "Test";

            Assert.Equal(TValueType.String, variant.ValueType);
            Assert.Equal("Test", variant.StringValue);
        }

        /// <summary>
        /// Вспомогательный класс для тестирования уведомлений.
        /// </summary>
        private class TestOwner : ILotusOwnerObject
        {
            private readonly Action<object, object, string> _onNotify;

            public TestOwner(Action<object, object, string> onNotify)
            {
                _onNotify = onNotify;
            }

            public ILotusOwnerObject IOwner 
            { 
                get => throw new NotImplementedException(); 
                set => throw new NotImplementedException(); 
            }

            public void AttachOwnedObject(ILotusOwnedObject ownedObject, bool add)
            {
                throw new NotImplementedException();
            }

            public void DetachOwnedObject(ILotusOwnedObject ownedObject, bool remove)
            {
                throw new NotImplementedException();
            }

            public void OnNotifyUpdated(object sender, object value, string propertyName)
            {
                _onNotify(sender, value, propertyName);
            }

            public void OnNotifyUpdated(ILotusOwnedObject ownedObject, object data, string dataName)
            {
                _onNotify(ownedObject, data, dataName);
            }

            public bool OnNotifyUpdating(ILotusOwnedObject ownedObject, object data, string dataName)
            {
                throw new NotImplementedException();
            }

            public void UpdateOwnedObjects()
            {
                throw new NotImplementedException();
            }
        }
    }
}
