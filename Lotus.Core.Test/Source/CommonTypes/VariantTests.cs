using System;
using System.ComponentModel;
using System.Reflection;

using Newtonsoft.Json.Linq;

using NUnit.Framework;
using NUnit.Framework.Internal;
using NUnit.Framework.Legacy;

namespace Lotus.Core.CommonTypes
{
    /// <summary>
    /// Тесты для <see cref="CVariant"/>.
    /// </summary>
    [TestFixture]
    public class VariantTests
    {
        /// <summary>
        /// Тест метода GetHashCode - исправленный метод без бесконечной рекурсии.
        /// </summary>
        [Test]
        public void GetHashCode_DoesNotCauseInfiniteRecursion()
        {
            var variant1 = new CVariant();
            variant1.IntegerValue = 100;

            var variant2 = new CVariant();
            variant2.IntegerValue = 100;

            // Должно работать без исключений и рекурсии
            var hash1 = variant1.GetHashCode();
            var hash2 = variant2.GetHashCode();

            ClassicAssert.AreEqual(hash1, hash2);
        }

        /// <summary>
        /// Тест метода GetHashCode - возвращает разные хеши для разных значений.
        /// </summary>
        [Test]
        public void GetHashCode_ReturnsDifferentHashesForDifferentValues()
        {
            var variant1 = new CVariant();
            variant1.IntegerValue = 100;

            var variant2 = new CVariant();
            variant2.IntegerValue = 200;

            var hash1 = variant1.GetHashCode();
            var hash2 = variant2.GetHashCode();

            ClassicAssert.AreNotEqual(hash1, hash2);
        }

        /// <summary>
        /// Тест метода DeserializeFromString - реализованный метод парсит тип.
        /// </summary>
        [Test]
        public void DeserializeFromString_WithValidFormat_ParsesType()
        {
            var variant = CVariant.DeserializeFromString("[Integer]");

            ClassicAssert.IsNotNull(variant);
            // Проверяем, что тип был установлен через свойство ValueType
            ClassicAssert.AreEqual(TValueType.Integer, variant.ValueType);
        }

        /// <summary>
        /// Тест метода DeserializeFromString - с пустой строкой возвращает пустой вариант.
        /// </summary>
        [Test]
        public void DeserializeFromString_WithEmptyString_ReturnsEmptyVariant()
        {
            var variant = CVariant.DeserializeFromString("");

            ClassicAssert.IsNotNull(variant);
            // Пустой вариант имеет тип Void по умолчанию
            ClassicAssert.AreEqual(TValueType.Void, variant.ValueType);
        }

        /// <summary>
        /// Тест метода DeserializeFromString - с null возвращает пустой вариант.
        /// </summary>
        [Test]
        public void DeserializeFromString_WithNull_ReturnsEmptyVariant()
        {
            var variant = CVariant.DeserializeFromString(null!);

            ClassicAssert.IsNotNull(variant);
            // Пустой вариант имеет тип Void по умолчанию
            ClassicAssert.AreEqual(TValueType.Void, variant.ValueType);
        }

        /// <summary>
        /// Тест метода DeserializeFromString - с нераспознанным форматом возвращает пустой вариант.
        /// </summary>
        [Test]
        public void DeserializeFromString_WithInvalidFormat_ReturnsEmptyVariant()
        {
            var variant = CVariant.DeserializeFromString("InvalidFormat");

            ClassicAssert.IsNotNull(variant);
            // Нераспознанный формат возвращает пустой вариант с типом Void
            ClassicAssert.AreEqual(TValueType.Void, variant.ValueType);
        }

        /// <summary>
        /// Тест уведомлений о свойствах - BooleanValue.
        /// </summary>
        [Test]
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

            ClassicAssert.IsTrue(notified);
            ClassicAssert.IsTrue(notifiedValue);
            ClassicAssert.AreEqual(nameof(CVariant.BooleanValue), notifiedProperty);
        }

        /// <summary>
        /// Тест уведомлений о свойствах - IntegerValue.
        /// </summary>
        [Test]
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

            ClassicAssert.IsTrue(notified);
            ClassicAssert.AreEqual(42, notifiedValue);
            ClassicAssert.AreEqual(nameof(CVariant.IntegerValue), notifiedProperty);
        }

        /// <summary>
        /// Тест уведомлений о свойствах - StringValue.
        /// </summary>
        [Test]
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

            ClassicAssert.IsTrue(notified);
            ClassicAssert.AreEqual("Test", notifiedValue);
            ClassicAssert.AreEqual(nameof(CVariant.StringValue), notifiedProperty);
        }

        /// <summary>
        /// Тест различных типов значений - Boolean.
        /// </summary>
        [Test]
        public void BooleanValue_SetsCorrectType()
        {
            var variant = new CVariant();
            variant.BooleanValue = true;

            ClassicAssert.AreEqual(TValueType.Boolean, variant.ValueType);
            ClassicAssert.IsTrue(variant.BooleanValue);
        }

        /// <summary>
        /// Тест различных типов значений - Integer.
        /// </summary>
        [Test]
        public void IntegerValue_SetsCorrectType()
        {
            var variant = new CVariant();
            variant.IntegerValue = 42;

            ClassicAssert.AreEqual(TValueType.Integer, variant.ValueType);
            ClassicAssert.AreEqual(42, variant.IntegerValue);
        }

        /// <summary>
        /// Тест различных типов значений - Float.
        /// </summary>
        [Test]
        public void FloatValue_SetsCorrectType()
        {
            var variant = new CVariant();
            variant.FloatValue = 3.14f;

            ClassicAssert.AreEqual(TValueType.Float, variant.ValueType);
            ClassicAssert.AreEqual(3.14f, variant.FloatValue, 0.001f);
        }

        /// <summary>
        /// Тест различных типов значений - String.
        /// </summary>
        [Test]
        public void StringValue_SetsCorrectType()
        {
            var variant = new CVariant();
            variant.StringValue = "Test";

            ClassicAssert.AreEqual(TValueType.String, variant.ValueType);
            ClassicAssert.AreEqual("Test", variant.StringValue);
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
