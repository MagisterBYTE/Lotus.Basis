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
    /// Редактор атрибута LotusEnabledEqualityAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusEnabledEqualityAttribute), true)]
    public class LotusEnabledEqualityAttributeDrawer : PropertyDrawer
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
            return EditorGUI.GetPropertyHeight(property, label, property.isExpanded);
        }

        /// <summary>
        /// Рисование сериализуемого свойства
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            var attr = attribute as LotusEnabledEqualityAttribute;

            // Получаем значение от члена объекта от которого зависит доступность
            var enabled = property.serializedObject.GetValueFromMember<bool>(attr.ManagingMemberName, attr.MemberType);

            EditorGUI.BeginProperty(position, label, property);
            {
                EditorGUI.BeginDisabledGroup(enabled != attr.Value);
                {
                    EditorGUI.PropertyField(position, property, label, property.isExpanded);
                }
                EditorGUI.EndDisabledGroup();
            }
            EditorGUI.EndProperty();
        }
        #endregion
    }
    /**@}*/
}
#endif
