#if UNITY_EDITOR
using Lotus.Core.Inspector;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор атрибута LotusInGroupAttribute
    /// </summary>
    /// <remarks>
    /// В кастомном инспекторе Lotus элементы с этим атрибутом рисуются через
    /// <see cref="LotusGroupingAttributeDrawer"/>. Drawer оставлен для стандартного инспектора Unity.
    /// </remarks>
    [CustomPropertyDrawer(typeof(LotusInGroupAttribute), true)]
    public class LotusInGroupAttributeDrawer : PropertyDrawer
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
