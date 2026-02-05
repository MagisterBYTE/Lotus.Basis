using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.IO;
using System.Xml;

namespace Lotus.Maths
{
    [TestFixture]
    public class MathSerializationXmlTests
    {
        private const float EpsilonF = 0.00001f;
        private const double EpsilonD = 0.00001;

        #region Rect2Df Tests
        [Test]
        public void XMathSerializationXml_WriteRead_Rect2Df_RoundTrip()
        {
            var original = new Rect2Df(1.0f, 2.0f, 10.0f, 20.0f);
            Rect2Df result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteRect2DToAttribute("rect", original);
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.ReadToDescendant("Test");
                    result = reader.ReadMathRect2DfFromAttribute("rect");
                }
            }

            ClassicAssert.AreEqual(original.X, result.X, EpsilonF);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonF);
            ClassicAssert.AreEqual(original.Width, result.Width, EpsilonF);
            ClassicAssert.AreEqual(original.Height, result.Height, EpsilonF);
        }

        [Test]
        public void XMathSerializationXml_Read_Rect2Df_WithDefaultValue_ReturnsDefault()
        {
            Rect2Df defaultValue = new Rect2Df(100.0f, 200.0f, 300.0f, 400.0f);
            Rect2Df result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.Read();
                    result = reader.ReadMathRect2DfFromAttribute("rect", defaultValue);
                }
            }

            ClassicAssert.AreEqual(defaultValue, result);
        }
        #endregion

        #region Rect2D Tests
        [Test]
        public void XMathSerializationXml_WriteRead_Rect2D_RoundTrip()
        {
            var original = new Rect2D(1.0, 2.0, 10.0, 20.0);
            Rect2D result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteRect2DToAttribute("rect", original);
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.ReadToDescendant("Test");
                    result = reader.ReadMathRect2DFromAttribute("rect");
                }
            }

            ClassicAssert.AreEqual(original.X, result.X, EpsilonD);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonD);
            ClassicAssert.AreEqual(original.Width, result.Width, EpsilonD);
            ClassicAssert.AreEqual(original.Height, result.Height, EpsilonD);
        }

        [Test]
        public void XMathSerializationXml_Read_Rect2D_WithDefaultValue_ReturnsDefault()
        {
            Rect2D defaultValue = new Rect2D(100.0, 200.0, 300.0, 400.0);
            Rect2D result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.ReadToDescendant("Test");
                    result = reader.ReadMathRect2DFromAttribute("rect", defaultValue);
                }
            }

            ClassicAssert.AreEqual(defaultValue, result);
        }
        #endregion

        #region Vector2D Tests
        [Test]
        public void XMathSerializationXml_WriteRead_Vector2D_RoundTrip()
        {
            var original = new Vector2D(1.5, 2.5);
            Vector2D result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteVector2DToAttribute("vector", original);
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.ReadToDescendant("Test");
                    result = reader.ReadMathVector2DFromAttribute("vector");
                }
            }

            ClassicAssert.AreEqual(original.X, result.X, EpsilonD);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonD);
        }

        [Test]
        public void XMathSerializationXml_Read_Vector2D_WithDefaultValue_ReturnsDefault()
        {
            Vector2D defaultValue = new Vector2D(100.0, 200.0);
            Vector2D result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.Read();
                    result = reader.ReadMathVector2DFromAttribute("vector", defaultValue);
                }
            }

            ClassicAssert.AreEqual(defaultValue, result);
        }
        #endregion

        #region Vector2Df Tests
        [Test]
        public void XMathSerializationXml_WriteRead_Vector2Df_RoundTrip()
        {
            var original = new Vector2Df(1.5f, 2.5f);
            Vector2Df result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteVector2DToAttribute("vector", original);
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.ReadToDescendant("Test");
                    result = reader.ReadMathVector2DfFromAttribute("vector");
                }
            }

            ClassicAssert.AreEqual(original.X, result.X, EpsilonF);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonF);
        }

        [Test]
        public void XMathSerializationXml_Read_Vector2Df_WithDefaultValue_ReturnsDefault()
        {
            Vector2Df defaultValue = new Vector2Df(100.0f, 200.0f);
            Vector2Df result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.Read();
                    result = reader.ReadMathVector2DfFromAttribute("vector", defaultValue);
                }
            }

            ClassicAssert.AreEqual(defaultValue, result);
        }
        #endregion

        #region Vector2Di Tests
        [Test]
        public void XMathSerializationXml_WriteRead_Vector2Di_RoundTrip()
        {
            var original = new Vector2Di(10, 20);
            Vector2Di result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteVector2DToAttribute("vector", original);
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.ReadToDescendant("Test");
                    result = reader.ReadMathVector2DiFromAttribute("vector");
                }
            }

            ClassicAssert.AreEqual(original.X, result.X);
            ClassicAssert.AreEqual(original.Y, result.Y);
        }

        [Test]
        public void XMathSerializationXml_Read_Vector2Di_WithDefaultValue_ReturnsDefault()
        {
            Vector2Di defaultValue = new Vector2Di(100, 200);
            Vector2Di result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.Read();
                    result = reader.ReadMathVector2DiFromAttribute("vector", defaultValue);
                }
            }

            ClassicAssert.AreEqual(defaultValue, result);
        }
        #endregion

        #region Vector3D Tests
        [Test]
        public void XMathSerializationXml_WriteRead_Vector3D_RoundTrip()
        {
            var original = new Vector3D(1.5, 2.5, 3.5);
            Vector3D result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteVector3DToAttribute("vector", original);
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.ReadToDescendant("Test");
                    result = reader.ReadMathVector3DFromAttribute("vector");
                }
            }

            ClassicAssert.AreEqual(original.X, result.X, EpsilonD);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonD);
            ClassicAssert.AreEqual(original.Z, result.Z, EpsilonD);
        }

        [Test]
        public void XMathSerializationXml_Read_Vector3D_WithDefaultValue_ReturnsDefault()
        {
            Vector3D defaultValue = new Vector3D(100.0, 200.0, 300.0);
            Vector3D result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.Read();
                    result = reader.ReadMathVector3DFromAttribute("vector", defaultValue);
                }
            }

            ClassicAssert.AreEqual(defaultValue, result);
        }
        #endregion

        #region Vector3Df Tests
        [Test]
        public void XMathSerializationXml_WriteRead_Vector3Df_RoundTrip()
        {
            var original = new Vector3Df(1.5f, 2.5f, 3.5f);
            Vector3Df result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteVector3DToAttribute("vector", original);
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.ReadToDescendant("Test");
                    result = reader.ReadMathVector3DfFromAttribute("vector");
                }
            }

            ClassicAssert.AreEqual(original.X, result.X, EpsilonF);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonF);
            ClassicAssert.AreEqual(original.Z, result.Z, EpsilonF);
        }

        [Test]
        public void XMathSerializationXml_Read_Vector3Df_WithDefaultValue_ReturnsDefault()
        {
            var defaultValue = new Vector3Df(100.0f, 200.0f, 300.0f);
            Vector3Df result;

            using (var stream = new MemoryStream())
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    writer.WriteStartElement("Test");
                    writer.WriteEndElement();
                    writer.Flush();
                }

                stream.Position = 0;

                using (var reader = XmlReader.Create(stream))
                {
                    reader.Read();
                    result = reader.ReadMathVector3DfFromAttribute("vector", defaultValue);
                }
            }

            ClassicAssert.AreEqual(defaultValue, result);
        }
        #endregion
    }
}
