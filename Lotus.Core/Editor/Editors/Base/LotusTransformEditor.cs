#if UNITY_EDITOR
using System;

using Lotus.Core;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Редактор компонента трансформации
    /// </summary>
    [CustomEditor(typeof(Transform))]
    [CanEditMultipleObjects]
    public class LotusInspectorTransformEditor : UnityEditor.Editor
    {
        #region Constants
        /// <summary>
        /// Ширина поля для редактирования значения
        /// </summary>
        private const float FIELD_WIDTH = 212.0f;

        /// <summary>
        /// Статус расширенного режима отображения
        /// </summary>
        private const bool WIDE_MODE = true;

        /// <summary>
        /// Максимальное значение для позиции
        /// </summary>
        private const float MAX_VALUE_POS = 100000.0f;

        /// <summary>
        /// Контент для отображения позиции
        /// </summary>
        private static readonly GUIContent ContentPosition = new GUIContent("Position",
            "The local position of this Game Object relative to the parent");

        /// <summary>
        /// Контент для отображения вращения
        /// </summary>
        private static readonly GUIContent ContentRotation = new GUIContent("Rotation",
            "The local rotation of this Game Object relative to the parent");

        /// <summary>
        /// Контент для отображения масштаба
        /// </summary>
        private static readonly GUIContent ContentScale = new GUIContent("Scale",
            "The local scaling of this Game Object relative to the parent");

        /// <summary>
        /// Контент кнопки сброса значения
        /// </summary>
        private static readonly GUIContent ContentButtonZero = new GUIContent("X", "Set zero value");

        /// <summary>
        /// Предупреждение о большой мировой позиции
        /// </summary>
        private static readonly string WarningTextPos =
            "Due to floating-point precision limitations, it is recommended to bring the world coordinates of the GameObject within a smaller range.";
        #endregion

        #region Fields
        // Основные параметры
        protected Transform _transform;
        protected SerializedProperty _positionProperty;
        protected SerializedProperty _rotationProperty;
        protected SerializedProperty _scaleProperty;

        // Привязка
        [Obsolete("Snap-позиция не реализована в инспекторе")]
        protected bool _isSnapPositionX;
        [Obsolete("Snap-позиция не реализована в инспекторе")]
        protected bool _isSnapPositionY;
        [Obsolete("Snap-позиция не реализована в инспекторе")]
        protected bool _isSnapPositionZ;
        [Obsolete("Snap-позиция не реализована в инспекторе")]
        protected Vector3 _snapPositionSize;

        // Расширенное управление
        [Obsolete("Глобальная позиция не отображается в инспекторе")]
        protected bool _showGlobalPosition;
        protected bool _showStepMove;
        protected Vector3 _stepMove = new Vector3(2, 2, 2);
        #endregion

        #region Unity events
        /// <summary>
        /// Включение скрипта в инспекторе объектов
        /// </summary>
        public void OnEnable()
        {
            _transform = target as Transform;

            _positionProperty = serializedObject.FindProperty("m_LocalPosition");
            _rotationProperty = serializedObject.FindProperty("m_LocalRotation");
            _scaleProperty = serializedObject.FindProperty("m_LocalScale");
        }

        /// <summary>
        /// Рисование в инспекторе объектов
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Custom Inspector Lotus", EditorStyles.centeredGreyMiniLabel);

            EditorGUIUtility.wideMode = WIDE_MODE;

            // align field to right of inspector
            if (EditorGUIUtility.currentViewWidth - EditorGUIUtility.labelWidth < FIELD_WIDTH)
            {
                EditorGUIUtility.labelWidth = EditorGUIUtility.currentViewWidth - FIELD_WIDTH;
            }

            serializedObject.Update();

            PositionPropertyField();
            RotationPropertyField();
            ScalePropertyField();

            _showStepMove = XEditorInspector.PropertyBoolean("Step move", _showStepMove);

            var text = string.Format("I: {0}, WP: {1}, LS: {2}", _transform.GetSiblingIndex(), _transform.position.ToString(),
                _transform.lossyScale.ToString());
            EditorGUILayout.HelpBox(text, MessageType.None);

            if (!ValidatePosition(_transform.position))
            {
                EditorGUILayout.HelpBox(WarningTextPos, MessageType.Warning);
            }

            serializedObject.ApplyModifiedProperties();
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Проверка на корректность позиции
        /// </summary>
        /// <param name="position">Мировая позиция</param>
        /// <returns>Статус корректности позиции</returns>
        private bool ValidatePosition(Vector3 position)
        {
            if (Mathf.Abs(position.x) > MAX_VALUE_POS)
            {
                return false;
            }
            if (Mathf.Abs(position.y) > MAX_VALUE_POS)
            {
                return false;
            }
            if (Mathf.Abs(position.z) > MAX_VALUE_POS)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Установка нулевой позиции объекта
        /// </summary>
        private void SetPositionZero()
        {
            for (var i = 0; i < Selection.transforms.Length; i++)
            {
                Selection.transforms[i].localPosition = Vector3.zero;
            }

            _positionProperty.serializedObject.SetIsDifferentCacheDirty();
        }

        /// <summary>
        /// Смещение объекта на указанный шаг
        /// </summary>
        /// <param name="x">Смещение по X</param>
        /// <param name="y">Смещение по Y</param>
        /// <param name="z">Смещение по Z</param>
        private void OffsetPosition(float x, float y, float z)
        {
            for (var i = 0; i < Selection.transforms.Length; i++)
            {
                Selection.transforms[i].localPosition += new Vector3(x, y, z);
            }

            _positionProperty.serializedObject.SetIsDifferentCacheDirty();
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Редактор свойства позиции игрового объекта
        /// </summary>
        protected void PositionPropertyField()
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.BeginHorizontal();
                {
                    EditorGUILayout.PropertyField(_positionProperty, ContentPosition);
                    if (GUILayout.Button(ContentButtonZero, EditorStyles.miniButtonRight, GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
                    {
                        SetPositionZero();
                    }
                }
                EditorGUILayout.EndHorizontal();

                if (_showStepMove)
                {
                    EditorGUILayout.BeginHorizontal();
                    {
                        GUILayout.Space(EditorGUIUtility.labelWidth + 14);

                        if (GUILayout.Button(XStringHelper.TriangleLeft, EditorStyles.miniButtonLeft))
                        {
                            OffsetPosition(-_stepMove.x, 0, 0);
                        }

                        if (GUILayout.Button(XStringHelper.TriangleRight, EditorStyles.miniButtonRight))
                        {
                            OffsetPosition(_stepMove.x, 0, 0);
                        }

                        GUILayout.Space(12);

                        if (GUILayout.Button(XStringHelper.TriangleLeft, EditorStyles.miniButtonLeft))
                        {
                            OffsetPosition(0, -_stepMove.y, 0);
                        }

                        if (GUILayout.Button(XStringHelper.TriangleRight, EditorStyles.miniButtonRight))
                        {
                            OffsetPosition(0, _stepMove.y, 0);
                        }

                        GUILayout.Space(12);

                        if (GUILayout.Button(XStringHelper.TriangleLeft, EditorStyles.miniButtonLeft))
                        {
                            OffsetPosition(0, 0, -_stepMove.z);
                        }

                        if (GUILayout.Button(XStringHelper.TriangleRight, EditorStyles.miniButtonRight))
                        {
                            OffsetPosition(0, 0, _stepMove.z);
                        }

                        GUILayout.Space(22);
                    }
                    EditorGUILayout.EndHorizontal();
                }
            }
            EditorGUILayout.EndVertical();
        }

        /// <summary>
        /// Редактор свойства вращения игрового объекта
        /// </summary>
        protected void RotationPropertyField()
        {
            var localRotation = _transform.localRotation;
            var transforms = Selection.transforms;
            for (var i = 0; i < transforms.Length; i++)
            {
                if (localRotation != transforms[i].localRotation)
                {
                    EditorGUI.showMixedValue = true;
                    break;
                }
            }

            EditorGUI.BeginChangeCheck();

            var eulerAngles = Vector3.zero;

            EditorGUILayout.BeginHorizontal();
            {
                eulerAngles = EditorGUILayout.Vector3Field(ContentRotation, localRotation.eulerAngles);
                if (GUILayout.Button(ContentButtonZero, EditorStyles.miniButtonRight, GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
                {
                    eulerAngles = Vector3.zero;
                    for (var i = 0; i < Selection.transforms.Length; i++)
                    {
                        Selection.transforms[i].localEulerAngles = Vector3.zero;
                    }

                    _rotationProperty.serializedObject.SetIsDifferentCacheDirty();
                }
            }
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObjects(targets, "Rotation Changed");

                transforms = Selection.transforms;
                for (var i = 0; i < transforms.Length; i++)
                {
                    transforms[i].localEulerAngles = eulerAngles;
                }

                _rotationProperty.serializedObject.SetIsDifferentCacheDirty();
            }

            EditorGUI.showMixedValue = false;
        }

        /// <summary>
        /// Редактор свойства масштаба игрового объекта
        /// </summary>
        protected void ScalePropertyField()
        {
            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.PropertyField(_scaleProperty, ContentScale);
                if (GUILayout.Button(ContentButtonZero, EditorStyles.miniButtonRight, GUILayout.Width(XInspectorViewParams.BUTTON_MINI_WIDTH)))
                {
                    for (var i = 0; i < Selection.transforms.Length; i++)
                    {
                        Selection.transforms[i].localScale = Vector3.one;
                    }

                    _scaleProperty.serializedObject.SetIsDifferentCacheDirty();
                }
            }
            EditorGUILayout.EndHorizontal();
        }
        #endregion
    }
    /**@}*/
}
#endif
