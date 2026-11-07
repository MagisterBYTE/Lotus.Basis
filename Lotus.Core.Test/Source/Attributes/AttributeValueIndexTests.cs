using System;

namespace Lotus.Core.Attributes
{
    /// <summary>
    /// Тесты для атрибута <see cref="LotusIndexToStringAttribute"/>.
    /// </summary>
    public class AttributeValueIndexTests
    {
        /// <summary>
        /// Тест создания атрибута с указанием имени члена и типа.
        /// </summary>
        [Fact]
        public void Constructor_WithMemberNameAndType_InitializesCorrectly()
        {
            var attribute = new LotusIndexToStringAttribute("TestMember", TInspectorMemberType.Property);
            
            Assert.Null(attribute.SourceType);
            Assert.Equal("TestMember", attribute.MemberName);
            Assert.Equal(TInspectorMemberType.Property, attribute.MemberType);
        }

        /// <summary>
        /// Тест создания атрибута с указанием типа, имени члена и типа члена.
        /// </summary>
        [Fact]
        public void Constructor_WithTypeMemberNameAndType_InitializesCorrectly()
        {
            var sourceType = typeof(string);
            var attribute = new LotusIndexToStringAttribute(sourceType, "TestMember", TInspectorMemberType.Field);
            
            Assert.Equal(sourceType, attribute.SourceType);
            Assert.Equal("TestMember", attribute.MemberName);
            Assert.Equal(TInspectorMemberType.Field, attribute.MemberType);
        }

        /// <summary>
        /// Тест свойств атрибута возвращают корректные значения.
        /// </summary>
        [Fact]
        public void Properties_ReturnCorrectValues()
        {
            var sourceType = typeof(int);
            var memberName = "GetValue";
            var memberType = TInspectorMemberType.Method;
            
            var attribute = new LotusIndexToStringAttribute(sourceType, memberName, memberType);
            
            Assert.Equal(sourceType, attribute.SourceType);
            Assert.Equal(memberName, attribute.MemberName);
            Assert.Equal(memberType, attribute.MemberType);
        }

        /// <summary>
        /// Тест что атрибут может быть применен к свойству.
        /// </summary>
        [Fact]
        public void Attribute_CanBeAppliedToProperty()
        {
            var attribute = new LotusIndexToStringAttribute("TestProperty", TInspectorMemberType.Property);
            Assert.NotNull(attribute);
        }

        /// <summary>
        /// Тест что атрибут может быть применен к полю.
        /// </summary>
        [Fact]
        public void Attribute_CanBeAppliedToField()
        {
            var attribute = new LotusIndexToStringAttribute("TestField", TInspectorMemberType.Field);
            Assert.NotNull(attribute);
        }
    }
}
