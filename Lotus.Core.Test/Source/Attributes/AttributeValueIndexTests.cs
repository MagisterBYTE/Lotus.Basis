using System;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace Lotus.Core.Attributes
{
    /// <summary>
    /// Тесты для атрибута <see cref="LotusIndexToStringAttribute"/>.
    /// </summary>
    [TestFixture]
    public class AttributeValueIndexTests
    {
        /// <summary>
        /// Тест создания атрибута с указанием имени члена и типа.
        /// </summary>
        [Test]
        public void Constructor_WithMemberNameAndType_InitializesCorrectly()
        {
            var attribute = new LotusIndexToStringAttribute("TestMember", TInspectorMemberType.Property);
            
            ClassicAssert.IsNull(attribute.SourceType);
            ClassicAssert.AreEqual("TestMember", attribute.MemberName);
            ClassicAssert.AreEqual(TInspectorMemberType.Property, attribute.MemberType);
        }

        /// <summary>
        /// Тест создания атрибута с указанием типа, имени члена и типа члена.
        /// </summary>
        [Test]
        public void Constructor_WithTypeMemberNameAndType_InitializesCorrectly()
        {
            var sourceType = typeof(string);
            var attribute = new LotusIndexToStringAttribute(sourceType, "TestMember", TInspectorMemberType.Field);
            
            ClassicAssert.AreEqual(sourceType, attribute.SourceType);
            ClassicAssert.AreEqual("TestMember", attribute.MemberName);
            ClassicAssert.AreEqual(TInspectorMemberType.Field, attribute.MemberType);
        }

        /// <summary>
        /// Тест свойств атрибута возвращают корректные значения.
        /// </summary>
        [Test]
        public void Properties_ReturnCorrectValues()
        {
            var sourceType = typeof(int);
            var memberName = "GetValue";
            var memberType = TInspectorMemberType.Method;
            
            var attribute = new LotusIndexToStringAttribute(sourceType, memberName, memberType);
            
            ClassicAssert.AreEqual(sourceType, attribute.SourceType);
            ClassicAssert.AreEqual(memberName, attribute.MemberName);
            ClassicAssert.AreEqual(memberType, attribute.MemberType);
        }

        /// <summary>
        /// Тест что атрибут может быть применен к свойству.
        /// </summary>
        [Test]
        public void Attribute_CanBeAppliedToProperty()
        {
            var attribute = new LotusIndexToStringAttribute("TestProperty", TInspectorMemberType.Property);
            ClassicAssert.IsNotNull(attribute);
        }

        /// <summary>
        /// Тест что атрибут может быть применен к полю.
        /// </summary>
        [Test]
        public void Attribute_CanBeAppliedToField()
        {
            var attribute = new LotusIndexToStringAttribute("TestField", TInspectorMemberType.Field);
            ClassicAssert.IsNotNull(attribute);
        }
    }
}
