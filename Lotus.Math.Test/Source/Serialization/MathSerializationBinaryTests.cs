using NUnit.Framework;
using NUnit.Framework.Legacy;
using System.Collections.Generic;
using System.IO;

namespace Lotus.Maths
{
    [TestFixture]
    public class MathSerializationBinaryTests
    {
        private const double EpsilonD = 0.00001;

        [Test]
        public void XMathSerializationBinary_WriteRead_Vector2D_RoundTrip()
        {
            var original = new Vector2D(1.5, 2.5);
            Vector2D result;

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    writer.Write(original);
                }

                stream.Position = 0;

                using (var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    result = reader.ReadMathVector2D();
                }
            }

            ClassicAssert.AreEqual(original.X, result.X, EpsilonD);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonD);
        }

        [Test]
        public void XMathSerializationBinary_WriteRead_Vector2D_WithRef_RoundTrip()
        {
            var original = new Vector2D(3.7, 4.9);
            var result = new Vector2D();

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    writer.Write(ref original);
                }

                stream.Position = 0;

                using (var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    reader.Read(ref result);
                }
            }

            ClassicAssert.AreEqual(original.X, result.X, EpsilonD);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonD);
        }

        [Test]
        public void XMathSerializationBinary_WriteRead_Vector2DList_RoundTrip()
        {
            var original = new List<Vector2D>
            {
                new Vector2D(1.0, 2.0),
                new Vector2D(3.0, 4.0),
                new Vector2D(5.0, 6.0)
            };
            Vector2D[]? result;

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    writer.Write(original);
                }

                stream.Position = 0;

                using (var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    result = reader.ReadMathVectors2D();
                }
            }

            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(original.Count, result.Length);
            for (int i = 0; i < original.Count; i++)
            {
                ClassicAssert.AreEqual(original[i].X, result[i].X, EpsilonD);
                ClassicAssert.AreEqual(original[i].Y, result[i].Y, EpsilonD);
            }
        }

        [Test]
        public void XMathSerializationBinary_WriteRead_NullVector2DList_ReturnsNull()
        {
            Vector2D[]? result;

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    writer.Write((IList<Vector2D>?)null);
                }

                stream.Position = 0;

                using (var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    result = reader.ReadMathVectors2D();
                }
            }

            ClassicAssert.IsNull(result);
        }

        [Test]
        public void XMathSerializationBinary_WriteRead_EmptyVector2DList_ReturnsNull()
        {
            var original = new List<Vector2D>();
            Vector2D[]? result;

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    writer.Write(original);
                }

                stream.Position = 0;

                using (var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    result = reader.ReadMathVectors2D();
                }
            }

            ClassicAssert.IsNull(result);
        }

        [Test]
        public void XMathSerializationBinary_WriteRead_Rect2D_RoundTrip()
        {
            var original = new Rect2D(1.0, 2.0, 10.0, 20.0);
            Rect2D result;

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    writer.Write(original);
                }

                stream.Position = 0;

                using (var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    result = reader.ReadMathRect2D();
                }
            }

            ClassicAssert.AreEqual(original.X, result.X, EpsilonD);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonD);
            ClassicAssert.AreEqual(original.Width, result.Width, EpsilonD);
            ClassicAssert.AreEqual(original.Height, result.Height, EpsilonD);
        }

        [Test]
        public void XMathSerializationBinary_WriteRead_Rect2D_WithRef_RoundTrip()
        {
            var original = new Rect2D(5.0, 6.0, 15.0, 25.0);
            var result = new Rect2D();

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    writer.Write(ref original);
                }

                stream.Position = 0;

                using (var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    reader.Read(ref result);
                }
            }

            ClassicAssert.AreEqual(original.X, result.X, EpsilonD);
            ClassicAssert.AreEqual(original.Y, result.Y, EpsilonD);
            ClassicAssert.AreEqual(original.Width, result.Width, EpsilonD);
            ClassicAssert.AreEqual(original.Height, result.Height, EpsilonD);
        }

        [Test]
        public void XMathSerializationBinary_WriteRead_Rect2DList_RoundTrip()
        {
            var original = new List<Rect2D>
            {
                new Rect2D(1.0, 2.0, 10.0, 20.0),
                new Rect2D(3.0, 4.0, 30.0, 40.0),
                new Rect2D(5.0, 6.0, 50.0, 60.0)
            };
            Rect2D[]? result;

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    writer.Write(original);
                }

                stream.Position = 0;

                using (var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    result = reader.ReadMathRects2D();
                }
            }

            ClassicAssert.IsNotNull(result);
            ClassicAssert.AreEqual(original.Count, result.Length);
            for (int i = 0; i < original.Count; i++)
            {
                ClassicAssert.AreEqual(original[i].X, result[i].X, EpsilonD);
                ClassicAssert.AreEqual(original[i].Y, result[i].Y, EpsilonD);
                ClassicAssert.AreEqual(original[i].Width, result[i].Width, EpsilonD);
                ClassicAssert.AreEqual(original[i].Height, result[i].Height, EpsilonD);
            }
        }

        [Test]
        public void XMathSerializationBinary_WriteRead_NullRect2DList_ReturnsNull()
        {
            Rect2D[]? result;

            using (var stream = new MemoryStream())
            {
                using (var writer = new BinaryWriter(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    writer.Write((IList<Rect2D>?)null);
                }

                stream.Position = 0;

                using (var reader = new BinaryReader(stream, System.Text.Encoding.UTF8, leaveOpen: true))
                {
                    result = reader.ReadMathRects2D();
                }
            }

            ClassicAssert.IsNull(result);
        }

        [Test]
        public void XMathSerializationBinary_Constants_AreCorrect()
        {
            ClassicAssert.AreEqual(-1, XMathSerializationBinary.ZERO_DATA);
            ClassicAssert.AreEqual(1, XMathSerializationBinary.EXISTING_DATA);
            ClassicAssert.AreEqual(198418, XMathSerializationBinary.SUCCESS_LABEL);
        }
    }
}
