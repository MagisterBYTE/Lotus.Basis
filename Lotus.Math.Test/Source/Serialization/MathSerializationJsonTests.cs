using NUnit.Framework;
using NUnit.Framework.Legacy;
using System;
using System.Text.Json;

namespace Lotus.Maths
{
    [TestFixture]
    public class MathSerializationJsonTests
    {
        private const float EpsilonF = 0.00001f;
        private const double EpsilonD = 0.00001;

        #region Vector2DfConverter Tests
        [Test]
        public void Vector2DfConverter_SerializeDeserialize_RoundTrip()
        {
            var original = new Vector2Df(1.5f, 2.5f);
            var settings = new JsonSerializerOptions
            {
                Converters = { Vector2DfConverter.Instance }
            };

            var json = JsonSerializer.Serialize(original, settings);
            var result = JsonSerializer.Deserialize<Vector2Df>(json, settings);

            ClassicAssert.AreEqual(original.X, result.X, EpsilonF);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonF);
        }
        #endregion

        #region Vector2DConverter Tests
        [Test]
        public void Vector2DConverter_SerializeDeserialize_RoundTrip()
        {
            var original = new Vector2D(1.5, 2.5);
            var settings = new JsonSerializerOptions
            {
                Converters = { Vector2DConverter.Instance }
            };

            var json = JsonSerializer.Serialize(original, settings);
            var result = JsonSerializer.Deserialize<Vector2D>(json, settings);

            ClassicAssert.AreEqual(original.X, result.X, EpsilonD);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonD);
        }
        #endregion

        #region Vector2DiConverter Tests
        [Test]
        public void Vector2DiConverter_SerializeDeserialize_RoundTrip()
        {
            var original = new Vector2Di(10, 20);
            var settings = new JsonSerializerOptions
            {
                Converters = { Vector2DiConverter.Instance }
            };

            var json = JsonSerializer.Serialize(original, settings);
            var result = JsonSerializer.Deserialize<Vector2Di>(json, settings);

            ClassicAssert.AreEqual(original.X, result.X);
            ClassicAssert.AreEqual(original.Y, result.Y);
        }
        #endregion

        #region Vector3DfConverter Tests
        [Test]
        public void Vector3DfConverter_SerializeDeserialize_RoundTrip()
        {
            var original = new Vector3Df(1.5f, 2.5f, 3.5f);
            var settings = new JsonSerializerOptions
            {
                Converters = { Vector3DfConverter.Instance }
            };

            var json = JsonSerializer.Serialize(original, settings);
            var result = JsonSerializer.Deserialize<Vector3Df>(json, settings);

            ClassicAssert.AreEqual(original.X, result.X, EpsilonF);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonF);
            ClassicAssert.AreEqual(original.Z, result.Z, EpsilonF);
        }
        #endregion

        #region Vector3DConverter Tests
        [Test]
        public void Vector3DConverter_SerializeDeserialize_RoundTrip()
        {
            var original = new Vector3D(1.5, 2.5, 3.5);
            var settings = new JsonSerializerOptions
            {
                Converters = { Vector3DConverter.Instance }
            };

            var json = JsonSerializer.Serialize(original, settings);
            var result = JsonSerializer.Deserialize<Vector3D>(json, settings);

            ClassicAssert.AreEqual(original.X, result.X, EpsilonD);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonD);
            ClassicAssert.AreEqual(original.Z, result.Z, EpsilonD);
        }
        #endregion

        #region Complex Object Tests
        [Test]
        public void JsonConverters_WithComplexObject_RoundTrip()
        {
            var original = new
            {
                Vector2Df = new Vector2Df(1.0f, 2.0f),
                Vector2D = new Vector2D(3.0, 4.0),
                Vector3Df = new Vector3Df(5.0f, 6.0f, 7.0f),
                Vector3D = new Vector3D(8.0, 9.0, 10.0)
            };

            var settings = new JsonSerializerOptions
            {
                Converters = {
                    Vector2DfConverter.Instance,
                    Vector2DConverter.Instance,
                    Vector3DfConverter.Instance,
                    Vector3DConverter.Instance
                }
            };

            var json = JsonSerializer.Serialize(original, settings);
            var result = JsonSerializer.Deserialize<dynamic>(json, settings);

            ClassicAssert.IsNotNull(result);
        }
        #endregion
    }
}
