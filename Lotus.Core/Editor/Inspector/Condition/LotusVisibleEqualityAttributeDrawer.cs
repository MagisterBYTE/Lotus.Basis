#if UNITY_EDITOR
using Lotus.Core;
using Lotus.Core.Inspector;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор атрибута LotusVisibleEqualityAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusVisibleEqualityAttribute), true)]
    public class LotusVisibleEqualityAttributeDrawer : PropertyDrawer
    {
        #region Unity events
        /// <summary>
        /// Получение высоты редактируемого свойства элемента
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        /// <returns>Высота свойства элемента</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var attr = attribute as LotusVisibleEqualityAttribute;

            // Получаем значение от члена объекта от которого зависит видимость
            var enabled = property.serializedObject.GetValueFromMember<bool>(attr.ManagingMemberName, attr.MemberType);

            if (enabled == attr.Value)
            {
                return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
            }

            return 0;
        }

        /// <summary>
        /// Рисование сериализуемого свойства
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = attribute as LotusVisibleEqualityAttribute;

            // Получаем значение от члена объекта от которого зависит видимость
            var enabled = property.serializedObject.GetValueFromMember<bool>(attr.ManagingMemberName, attr.MemberType);

            if (enabled != attr.Value)
            {
                return;
            }

            EditorGUI.BeginProperty(position, label, property);
            {
                EditorGUI.PropertyField(position, property, label, property.isExpanded);
            }
            EditorGUI.EndProperty();
        }
        #endregion
    }
    /**@}*/
}
#endif
