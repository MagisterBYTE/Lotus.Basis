#if UNITY_EDITOR
using Lotus.Core.Inspector;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор атрибута LotusPreviewAttribute
    /// </summary>
    [CustomPropertyDrawer(typeof(LotusPreviewAttribute), true)]
    public class LotusPreviewAttributeDrawer : PropertyDrawer
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
            var height = EditorGUI.GetPropertyHeight(property, label, property.isExpanded);

            if (property.propertyType == SerializedPropertyType.ObjectReference)
            {
                var previewTexture = GetAssetPreview(property);
                if (previewTexture != null)
                {
                    height += GetAssetPreviewSize(property).y;
                }
            }

            return height;
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
                if (property.propertyType == SerializedPropertyType.ObjectReference)
                {
                    var propertyRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
                    EditorGUI.PropertyField(propertyRect, property, label);

                    var previewTexture = GetAssetPreview(property);
                    if (previewTexture != null)
                    {
                        var previewSize = GetAssetPreviewSize(property);
                        var previewRect = new Rect(position.x + 2, position.y + EditorGUIUtility.singleLineHeight,
                            position.width, previewSize.y);
                        GUI.Label(previewRect, previewTexture);
                    }
                }
                else
                {
                    EditorGUI.PropertyField(position, property, label, property.isExpanded);
                }
            }
            EditorGUI.EndProperty();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение текстуры предпросмотра у сериализуемого свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Текстура или null</returns>
        private Texture2D GetAssetPreview(SerializedProperty property)
        {
            if (property.propertyType != SerializedPropertyType.ObjectReference)
            {
                return null;
            }

            if (property.objectReferenceValue == null)
            {
                return null;
            }

            return AssetPreview.GetAssetPreview(property.objectReferenceValue);
        }

        /// <summary>
        /// Получение размера текстуры предпросмотра у сериализуемого свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Размер текстуры</returns>
        private Vector2 GetAssetPreviewSize(SerializedProperty property)
        {
            var previewTexture = GetAssetPreview(property);
            if (previewTexture == null)
            {
                return Vector2.zero;
            }

            var attr = attribute as LotusPreviewAttribute;
            var targetHeight = attr != null ? attr.PreviewHeight : 200f;
            var targetWidth = targetHeight;

            var width = Mathf.Clamp(targetWidth, 0, previewTexture.width);
            var height = Mathf.Clamp(targetHeight, 0, previewTexture.height);

            return new Vector2(width, height);
        }
        #endregion
    }
    /**@}*/
}
#endif
