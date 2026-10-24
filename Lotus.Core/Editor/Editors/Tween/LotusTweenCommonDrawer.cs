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
    /// Набор флагов для рисования параметров анимации
    /// </summary>
    [Flags]
    public enum TTweenDrawParam
    {
        /// <summary>
        /// Начальное и целевое значение
        /// </summary>
        StartAndTarget = 1,

        /// <summary>
        /// Индекс кривой для анимации вперед
        /// </summary>
        CurveForward = 2,

        /// <summary>
        /// Индекс кривой для анимации назад
        /// </summary>
        CurveBackward = 4,

        /// <summary>
        /// Режим проигрывания анимации
        /// </summary>
        WrapMode = 8,

        /// <summary>
        /// Продолжительность анимации
        /// </summary>
        Duration = 16,

        /// <summary>
        /// Все флаги
        /// </summary>
        All = StartAndTarget | CurveForward | CurveBackward | WrapMode | Duration
    }

    /// <summary>
    /// Базовый класс редактора для рисования параметров класса анимации
    /// </summary>
    public class LotusTweenBaseDrawer : PropertyDrawer
    {
        #region Constants
        /// <summary>
        /// Контент начального значения
        /// </summary>
        protected static readonly GUIContent ContentStart = new GUIContent("Start Value");

        /// <summary>
        /// Контент целевого значения
        /// </summary>
        protected static readonly GUIContent ContentTarget = new GUIContent("Target Value");

        /// <summary>
        /// Контент режима проигрывания
        /// </summary>
        protected static readonly GUIContent ContentWrap = new GUIContent("Wrap");
        #endregion

        #region Unity events
        /// <summary>
        /// Получение высоты свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        /// <returns>Высота свойства элемента</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.isExpanded)
            {
                return XInspectorViewParams.CONTROL_HEIGHT_SPACE * 9;
            }

            return XInspectorViewParams.CONTROL_HEIGHT_SPACE;
        }

        /// <summary>
        /// Рисование параметров элемента
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            XEditorInspector.PrepareContent(property);

            EditorGUI.BeginChangeCheck();
            EditorGUI.BeginProperty(position, XEditorInspector.Content, property);
            {
                var propertyName = property.FindPropertyRelative(nameof(CTweenInteger._name));
                var propertyStart = property.FindPropertyRelative(nameof(CTweenInteger._startValue));
                var propertyTarget = property.FindPropertyRelative(nameof(CTweenInteger._targetValue));
                var propertyForward = property.FindPropertyRelative(nameof(CTweenInteger._curveIndexForward));
                var propertyBackward = property.FindPropertyRelative(nameof(CTweenInteger._curveIndexBackward));
                var propertyWrap = property.FindPropertyRelative(nameof(CTweenInteger._wrapMode));
                var propertyIgnore = property.FindPropertyRelative(nameof(CTweenInteger._ignoreTimeScale));
                var propertyDuration = property.FindPropertyRelative(nameof(CTweenInteger._duration));

                position.height = XInspectorViewParams.CONTROL_HEIGHT;
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, XEditorInspector.Content);
                if (property.isExpanded)
                {
                    EditorGUI.indentLevel++;

                    position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    propertyName.stringValue = EditorGUI.TextField(position, "Name", propertyName.stringValue);

                    position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    EditorGUI.PropertyField(position, propertyStart, ContentStart, false);

                    position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    EditorGUI.PropertyField(position, propertyTarget, ContentTarget, false);

                    position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    var rectSelected = position;
                    rectSelected.width = position.width * 0.7f;

                    var rectCurve = position;
                    rectCurve.x = rectSelected.xMax;
                    rectCurve.width = position.width - rectSelected.width;

                    var curveStorage = LotusTweenDispatcher.CurveStorage;
                    if (curveStorage != null)
                    {
                        propertyForward.intValue = EditorGUI.Popup(rectSelected, "Curve Forward",
                            propertyForward.intValue, curveStorage.ListNames);

                        if (propertyForward.intValue > -1 && propertyForward.intValue < curveStorage.Curves.Count)
                        {
                            EditorGUI.CurveField(rectCurve, curveStorage.Curves[propertyForward.intValue].Curve);
                        }

                        position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                        rectSelected.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                        rectCurve.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;

                        propertyBackward.intValue = EditorGUI.Popup(rectSelected, "Curve Backward",
                            propertyBackward.intValue, curveStorage.ListNames);

                        if (propertyBackward.intValue > -1 && propertyBackward.intValue < curveStorage.Curves.Count)
                        {
                            EditorGUI.CurveField(rectCurve, curveStorage.Curves[propertyBackward.intValue].Curve);
                        }
                    }

                    position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    EditorGUI.PropertyField(position, propertyWrap, ContentWrap, false);

                    position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    propertyIgnore.boolValue = EditorGUI.Toggle(position, "IgnoreTimeScale", propertyIgnore.boolValue);

                    position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    propertyDuration.floatValue = EditorGUI.FloatField(position, "Duration", propertyDuration.floatValue);

                    EditorGUI.indentLevel--;
                }
            }
            EditorGUI.EndProperty();
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Рисование кривых, режима и длительности для базового tween
        /// </summary>
        /// <typeparam name="TAnimatableType">Тип анимируемого значения</typeparam>
        /// <param name="tween">Анимация</param>
        /// <param name="drawParam">Набор флагов для рисования параметров</param>
        protected static void DrawCurveAndDuration<TAnimatableType>(TweenGenericObject<TAnimatableType> tween,
            TTweenDrawParam drawParam)
        {
            var curveStorage = LotusTweenDispatcher.CurveStorage;

            if (drawParam.IsFlagSet(TTweenDrawParam.CurveForward) && curveStorage != null)
            {
                GUILayout.Space(2.0f);
                GUILayout.BeginHorizontal();
                {
                    tween._curveIndexForward = EditorGUILayout.Popup("Curve Forward", tween._curveIndexForward,
                        curveStorage.ListNames);

                    if (tween._curveIndexForward > -1 && tween._curveIndexForward < curveStorage.Curves.Count)
                    {
                        EditorGUILayout.CurveField(curveStorage.Curves[tween._curveIndexForward].Curve);
                    }
                }
                GUILayout.EndHorizontal();
            }

            if (drawParam.IsFlagSet(TTweenDrawParam.CurveBackward) && curveStorage != null)
            {
                GUILayout.Space(2.0f);
                GUILayout.BeginHorizontal();
                {
                    tween._curveIndexBackward = EditorGUILayout.Popup("Curve Backward", tween._curveIndexBackward,
                        curveStorage.ListNames);

                    if (tween._curveIndexBackward > -1 && tween._curveIndexBackward < curveStorage.Curves.Count)
                    {
                        EditorGUILayout.CurveField(curveStorage.Curves[tween._curveIndexBackward].Curve);
                    }
                }
                GUILayout.EndHorizontal();
            }

            if (drawParam.IsFlagSet(TTweenDrawParam.WrapMode))
            {
                GUILayout.Space(4.0f);
                tween.WrapMode = XEditorInspector.PropertyEnum("WrapMode", tween.WrapMode);
            }

            if (drawParam.IsFlagSet(TTweenDrawParam.Duration))
            {
                GUILayout.Space(2.0f);
                tween.IgnoreTimeScale = XEditorInspector.PropertyBoolean("IgnoreTimeScale", tween.IgnoreTimeScale);

                GUILayout.Space(2.0f);
                tween.Duration = XEditorInspector.PropertyFloatSlider("Duration", tween.Duration, 0.1f, 4.0f);
            }
        }
        #endregion
    }

    /// <summary>
    /// Редактор для рисования параметров класса для хранения и управления анимации целым значением
    /// </summary>
    [CustomPropertyDrawer(typeof(CTweenInteger))]
    public class LotusTweenIntegerDrawer : LotusTweenBaseDrawer
    {
        #region Draw methods
        /// <summary>
        /// Рисование параметров анимации целого значения
        /// </summary>
        /// <param name="tween">Анимация целого значения</param>
        /// <param name="drawParam">Набор флагов для рисования параметров</param>
        public static void Draw(CTweenInteger tween, TTweenDrawParam drawParam = TTweenDrawParam.All)
        {
            if (drawParam.IsFlagSet(TTweenDrawParam.StartAndTarget))
            {
                GUILayout.Space(2.0f);
                tween.StartValue = XEditorInspector.PropertyInt("Start Value", tween.StartValue);

                GUILayout.Space(2.0f);
                tween.TargetValue = XEditorInspector.PropertyInt("Target Value", tween.TargetValue);
            }

            DrawCurveAndDuration(tween, drawParam);
        }
        #endregion
    }

    /// <summary>
    /// Редактор для рисования параметров класса для хранения и управления анимации вещественным значением
    /// </summary>
    [CustomPropertyDrawer(typeof(CTweenSingle))]
    public class LotusTweenSingleDrawer : LotusTweenBaseDrawer
    {
        #region Draw methods
        /// <summary>
        /// Рисование параметров анимации вещественного значения
        /// </summary>
        /// <param name="tween">Анимация вещественного значения</param>
        /// <param name="drawParam">Набор флагов для рисования параметров</param>
        public static void Draw(CTweenSingle tween, TTweenDrawParam drawParam = TTweenDrawParam.All)
        {
            if (drawParam.IsFlagSet(TTweenDrawParam.StartAndTarget))
            {
                GUILayout.Space(2.0f);
                tween.StartValue = XEditorInspector.PropertyFloat("Start Value", tween.StartValue);

                GUILayout.Space(2.0f);
                tween.TargetValue = XEditorInspector.PropertyFloat("Target Value", tween.TargetValue);
            }

            DrawCurveAndDuration(tween, drawParam);
        }
        #endregion
    }

    /// <summary>
    /// Редактор для рисования параметров класса для хранения и управления анимации цветовым значением
    /// </summary>
    [CustomPropertyDrawer(typeof(CTweenColor))]
    public class LotusTweenColorDrawer : LotusTweenBaseDrawer
    {
        #region Draw methods
        /// <summary>
        /// Рисование параметров анимации цветового значения
        /// </summary>
        /// <param name="tween">Анимация цветового значения</param>
        /// <param name="drawParam">Набор флагов для рисования параметров</param>
        public static void Draw(CTweenColor tween, TTweenDrawParam drawParam = TTweenDrawParam.All)
        {
            if (drawParam.IsFlagSet(TTweenDrawParam.StartAndTarget))
            {
                GUILayout.Space(2.0f);
                tween.StartValue = XEditorInspector.PropertyColor("Start Value", tween.StartValue);

                GUILayout.Space(2.0f);
                tween.TargetValue = XEditorInspector.PropertyColor("Target Value", tween.TargetValue);
            }

            DrawCurveAndDuration(tween, drawParam);
        }
        #endregion
    }

    /// <summary>
    /// Редактор для рисования параметров класса для хранения и управления анимации 2D вектором
    /// </summary>
    [CustomPropertyDrawer(typeof(CTweenVector2D))]
    public class LotusTweenVector2DDrawer : LotusTweenBaseDrawer
    {
        #region Draw methods
        /// <summary>
        /// Рисование параметров анимации двухмерного вектора
        /// </summary>
        /// <param name="tween">Анимация двухмерного вектора</param>
        /// <param name="drawParam">Набор флагов для рисования параметров</param>
        public static void Draw(CTweenVector2D tween, TTweenDrawParam drawParam = TTweenDrawParam.All)
        {
            if (drawParam.IsFlagSet(TTweenDrawParam.StartAndTarget))
            {
                GUILayout.Space(2.0f);
                tween.StartValue = XEditorInspector.PropertyVector2D("Start Value", tween.StartValue);

                GUILayout.Space(2.0f);
                tween.TargetValue = XEditorInspector.PropertyVector2D("Target Value", tween.TargetValue);
            }

            DrawCurveAndDuration(tween, drawParam);
        }
        #endregion
    }

    /// <summary>
    /// Редактор для рисования параметров класса для хранения и управления анимации 3D вектором
    /// </summary>
    [CustomPropertyDrawer(typeof(CTweenVector3D))]
    public class LotusTweenVector3DDrawer : LotusTweenBaseDrawer
    {
        #region Draw methods
        /// <summary>
        /// Рисование параметров анимации трехмерного вектора
        /// </summary>
        /// <param name="tween">Анимация трехмерного вектора</param>
        /// <param name="drawParam">Набор флагов для рисования параметров</param>
        public static void Draw(CTweenVector3D tween, TTweenDrawParam drawParam = TTweenDrawParam.All)
        {
            if (drawParam.IsFlagSet(TTweenDrawParam.StartAndTarget))
            {
                GUILayout.Space(2.0f);
                tween.StartValue = XEditorInspector.PropertyVector3D("Start Value", tween.StartValue);

                GUILayout.Space(2.0f);
                tween.TargetValue = XEditorInspector.PropertyVector3D("Target Value", tween.TargetValue);
            }

            DrawCurveAndDuration(tween, drawParam);
        }
        #endregion
    }

    /// <summary>
    /// Редактор для рисования параметров класса для хранения и управления анимации спрайтами
    /// </summary>
    [CustomPropertyDrawer(typeof(CTweenSprite))]
    public class LotusTweenSpriteDrawer : PropertyDrawer
    {
        #region Constants
        /// <summary>
        /// Контент режима проигрывания
        /// </summary>
        protected static readonly GUIContent ContentWrap = new GUIContent("Wrap");

        /// <summary>
        /// Контент кнопки установки длительности из данных спрайта
        /// </summary>
        protected static readonly GUIContent ContentButtonSet = new GUIContent("Set", "Set duration from sprite data");
        #endregion

        #region Unity events
        /// <summary>
        /// Получение высоты свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        /// <returns>Высота свойства элемента</returns>
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            if (property.isExpanded)
            {
                return XInspectorViewParams.CONTROL_HEIGHT_SPACE * 8;
            }

            return XInspectorViewParams.CONTROL_HEIGHT_SPACE;
        }

        /// <summary>
        /// Рисование параметров элемента
        /// </summary>
        /// <param name="position">Прямоугольник для отображения</param>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="label">Надпись</param>
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            XEditorInspector.PrepareContent(property);

            EditorGUI.BeginChangeCheck();
            EditorGUI.BeginProperty(position, label, property);
            {
                var propertyName = property.FindPropertyRelative(nameof(CTweenSprite._name));
                var propertyIndex = property.FindPropertyRelative(nameof(CTweenSprite._storageSpriteIndex));
                var propertyStart = property.FindPropertyRelative(nameof(CTweenSprite._startFrame));
                var propertyTarget = property.FindPropertyRelative(nameof(CTweenSprite._targetFrame));
                var propertyWrap = property.FindPropertyRelative(nameof(CTweenSprite._wrapMode));
                var propertyDuration = property.FindPropertyRelative(nameof(CTweenSprite._duration));

                position.height = XInspectorViewParams.CONTROL_HEIGHT;
                property.isExpanded = EditorGUI.Foldout(position, property.isExpanded, label);
                if (property.isExpanded)
                {
                    EditorGUI.indentLevel++;

                    position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                    propertyName.stringValue = EditorGUI.TextField(position, "Name", propertyName.stringValue);

                    var spriteStorage = LotusTweenDispatcher.SpriteStorage;
                    if (spriteStorage != null && spriteStorage.GroupSprites != null)
                    {
                        if (spriteStorage.GroupCount == 0)
                        {
                            position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                            EditorGUI.LabelField(position, "Empty storages", EditorStyles.boldLabel);
                        }
                        else
                        {
                            position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                            propertyIndex.intValue = EditorGUI.Popup(position, "Storage Index", propertyIndex.intValue,
                                spriteStorage.ListNames);

                            position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                            if (spriteStorage[propertyIndex.intValue].Sprites[0] != null)
                            {
                                EditorGUI.ObjectField(position, spriteStorage[propertyIndex.intValue].Sprites[0].texture,
                                    typeof(Texture), false);
                            }

                            position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                            var countFrame = spriteStorage[propertyIndex.intValue].Count - 1;
                            propertyStart.intValue = EditorGUI.IntSlider(position, "Start Frame",
                                propertyStart.intValue, 0, countFrame);

                            position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                            var textTargetFrame = "Target Frame (max " + countFrame.ToString() + ")";
                            propertyTarget.intValue = EditorGUI.IntSlider(position, textTargetFrame,
                                propertyTarget.intValue, propertyStart.intValue, countFrame);

                            position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                            EditorGUI.PropertyField(position, propertyWrap, ContentWrap, false);

                            position.y += XInspectorViewParams.CONTROL_HEIGHT_SPACE;
                            position.width -= 36;
                            propertyDuration.floatValue = EditorGUI.FloatField(position, "Duration",
                                propertyDuration.floatValue);

                            var rectButton = position;
                            rectButton.x = position.xMax;
                            rectButton.width = 36;

                            if (GUI.Button(rectButton, ContentButtonSet, EditorStyles.miniButtonRight))
                            {
                                propertyDuration.floatValue = spriteStorage[propertyIndex.intValue].TimeAnimation;
                            }
                        }
                    }
                    EditorGUI.indentLevel--;
                }
            }
            EditorGUI.EndProperty();
            if (EditorGUI.EndChangeCheck())
            {
                property.serializedObject.ApplyModifiedProperties();
            }
        }
        #endregion

        #region Draw methods
        /// <summary>
        /// Рисование параметров анимации спрайта
        /// </summary>
        /// <param name="tween">Анимация спрайта</param>
        /// <param name="drawParam">Набор флагов для рисования параметров (зарезервирован, не используется)</param>
        public static void Draw(CTweenSprite tween, TTweenDrawParam drawParam = TTweenDrawParam.All)
        {
            _ = drawParam;

            var spriteStorage = LotusTweenDispatcher.SpriteStorage;
            if (spriteStorage != null && spriteStorage.GroupSprites != null && spriteStorage.GroupCount != 0)
            {
                GUILayout.Space(2.0f);
                var countFrame = spriteStorage[tween.StorageSpriteIndex].Count - 1;
                tween.StartFrame = XEditorInspector.PropertyIntSlider("Start Frame", tween.StartFrame, 0, countFrame);

                GUILayout.Space(2.0f);
                var textTargetFrame = "Target Frame (max " + countFrame.ToString() + ")";
                tween.TargetFrame = XEditorInspector.PropertyIntSlider(textTargetFrame, tween.TargetFrame,
                    tween.StartFrame, countFrame);
            }
            else
            {
                GUILayout.Space(2.0f);
                tween.StartFrame = XEditorInspector.PropertyInt("Start Frame", tween.StartFrame);

                GUILayout.Space(2.0f);
                tween.TargetFrame = XEditorInspector.PropertyInt("Target Frame", tween.TargetFrame);
            }

            if (spriteStorage != null && spriteStorage.GroupSprites != null)
            {
                if (spriteStorage.GroupCount == 0)
                {
                    XEditorInspector.DrawSection("Empty storages");
                }
                else
                {
                    GUILayout.Space(2.0f);
                    XEditorInspector.PropertyResource("Sprite",
                        spriteStorage[tween.StorageSpriteIndex].Sprites[0].texture);

                    GUILayout.Space(2.0f);
                    tween.StorageSpriteIndex = EditorGUILayout.Popup("Storage Index", tween.StorageSpriteIndex,
                        spriteStorage.ListNames);

                    GUILayout.Space(2.0f);
                    tween.WrapMode = XEditorInspector.PropertyEnum("WrapMode", tween.WrapMode);

                    GUILayout.Space(2.0f);
                    tween.Duration = XEditorInspector.PropertyFloat("Duration", tween.Duration);
                }
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
