#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Lotus.Core;

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширения класса SerializedProperty
    /// </summary>
    /// <remarks>
    /// Только для режима редактора Unity
    /// </remarks>
    public static class XSerializedPropertyExtension
    {
        #region Constants
        /// <summary>
        /// Имя внутреннего массива для всех типов коллекций системы Lotus
        /// </summary>
        public const string NAME_LOTUS_ARRAY = nameof(ListArray<int>._arrayOfItems);

        /// <summary>
        /// Имя суффикса массива
        /// </summary>
        public const string NAME_SUFFIX_ARRAY = ".Array.data[";
        #endregion

        #region Main methods
        /// <summary>
        /// Проверка на свойство скрипта
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Статус скрипта</returns>
        public static bool IsScript(this SerializedProperty property)
        {
            return property.name.Equals("m_Script") &&
                property.type.Equals("PPtr<MonoScript>") &&
                property.propertyType == SerializedPropertyType.ObjectReference &&
                property.propertyPath.Equals("m_Script");
        }

        /// <summary>
        /// Проверка на свойство первого/корневого уровня
        /// </summary>
        /// <remarks>
        /// Первый/корневой уровень означает что свойство объявлено непосредственно в компоненте
        /// </remarks>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Статус свойства первого/корневого уровня</returns>
        public static bool IsRoot(this SerializedProperty property)
        {
            var index = property.propertyPath.IndexOf(XCharHelper.Dot);
            return index == -1;
        }

        /// <summary>
        /// Получение объекта которому принадлежит указанное сериализуемое свойство
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Объект</returns>
        public static object GetInstance(this SerializedProperty property)
        {
            return property.serializedObject.targetObject;
        }

        /// <summary>
        /// Получение поля данных которое формирует указанное сериализуемое свойство
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Поле данных свойства</returns>
        public static FieldInfo GetFieldInfo(this SerializedProperty property)
        {
            object instance = property.serializedObject.targetObject;
            FieldInfo fieldInfo = null;

            if (property.IsRoot())
            {
                if (instance != null)
                {
                    fieldInfo = XReflection.GetField(instance, property.propertyPath);
                }
            }
            else
            {
                var path = property.propertyPath.Replace(NAME_SUFFIX_ARRAY, "[");
                var elements = path.Split('.');

                for (var i = 0; i < elements.Length; i++)
                {
                    var element = elements[i];

                    if (element.Contains("["))
                    {
                        var elementName = element.Substring(0, element.IndexOf("["));
                        var index = element.ExtractNumberLast();
                        instance = XReflection.GetFieldValue(instance, elementName, index, out fieldInfo);
                    }
                    else
                    {
                        instance = XReflection.GetFieldValue(instance, element, out fieldInfo);
                    }
                }
            }

            return fieldInfo;
        }

        /// <summary>
        /// Получение строкового значения сериализуемого свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Строковое значение</returns>
        public static string AsString(this SerializedProperty property)
        {
            var result = "";
            switch (property.propertyType)
            {
                case SerializedPropertyType.Generic:
                    break;
                case SerializedPropertyType.Integer:
                    {
                        result = property.intValue.ToString();
                    }
                    break;
                case SerializedPropertyType.Boolean:
                    {
                        result = property.boolValue.ToString();
                    }
                    break;
                case SerializedPropertyType.Float:
                    {
                        result = property.floatValue.ToString();
                    }
                    break;
                case SerializedPropertyType.String:
                    {
                        result = property.stringValue;
                    }
                    break;
                case SerializedPropertyType.Color:
                    {
                        result = property.colorValue.ToString();
                    }
                    break;
                case SerializedPropertyType.ObjectReference:
                    {
                        if (property.objectReferenceValue != null)
                        {
                            result = property.objectReferenceValue.name;
                        }
                    }
                    break;
                case SerializedPropertyType.LayerMask:
                    {
                        result = LayerMask.LayerToName(property.intValue);
                    }
                    break;
                case SerializedPropertyType.Enum:
                    {
                        result = property.enumDisplayNames[property.enumValueIndex];
                    }
                    break;
                case SerializedPropertyType.Vector2:
                    {
                        result = property.vector2Value.ToString();
                    }
                    break;
                case SerializedPropertyType.Vector3:
                    {
                        result = property.vector3Value.ToString();
                    }
                    break;
                case SerializedPropertyType.Vector4:
                    {
                        result = property.vector4Value.ToString();
                    }
                    break;
                case SerializedPropertyType.Rect:
                    {
                        result = property.rectValue.ToString();
                    }
                    break;
                case SerializedPropertyType.ArraySize:
                    {
                        result = property.arraySize.ToString();
                    }
                    break;
                case SerializedPropertyType.Character:
                    {
                        result = property.stringValue;
                    }
                    break;
                case SerializedPropertyType.AnimationCurve:
                    {
                        result = property.animationCurveValue.ToString();
                    }
                    break;
                case SerializedPropertyType.Bounds:
                    {
                        result = property.boundsValue.ToString();
                    }
                    break;
                case SerializedPropertyType.Gradient:
                    {
                        result = property.gradientValue != null ? property.gradientValue.ToString() : string.Empty;
                    }
                    break;
                case SerializedPropertyType.Quaternion:
                    {
                        result = property.quaternionValue.ToString();
                    }
                    break;
                case SerializedPropertyType.ExposedReference:
                    {
                        if (property.objectReferenceValue != null)
                        {
                            result = property.objectReferenceValue.name;
                        }
                    }
                    break;
                case SerializedPropertyType.FixedBufferSize:
                    break;
                case SerializedPropertyType.Vector2Int:
                    {
                        result = property.vector2IntValue.ToString();
                    }
                    break;
                case SerializedPropertyType.Vector3Int:
                    {
                        result = property.vector3IntValue.ToString();
                    }
                    break;
                case SerializedPropertyType.RectInt:
                    {
                        result = property.rectIntValue.ToString();
                    }
                    break;
                case SerializedPropertyType.BoundsInt:
                    {
                        result = property.boundsIntValue.ToString();
                    }
                    break;
                case SerializedPropertyType.ManagedReference:
                    {
                        result = property.managedReferenceFullTypename;
                    }
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Получение списка дочерних свойств
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Список дочерних свойств</returns>
        public static List<SerializedProperty> GetChildrenProperties(this SerializedProperty property)
        {
            var prop = property.Copy();
            var depth = prop.depth;
            var result = new List<SerializedProperty>();

            var enter = true;
            while (prop.NextVisible(enter) && prop.depth > depth)
            {
                result.Add(prop.Copy());
                enter = false;
            }

            return result;
        }

        /// <summary>
        /// Сохранение сериализуемого свойства
        /// </summary>
        /// <remarks>
        /// Помечает объект и сцену как изменённые для возможности сохранить данные на диск
        /// </remarks>
        /// <param name="property">Сериализируемое свойство</param>
        public static void Save(this SerializedProperty property)
        {
            EditorUtility.SetDirty(property.serializedObject.targetObject);
            if (!EditorApplication.isPlaying && property.serializedObject.targetObject is Component component)
            {
                EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
            }
        }
        #endregion

        #region Invoke methods
        /// <summary>
        /// Вызов метода из родительского свойства или текущего свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="methodName">Имя метода</param>
        /// <returns>Результат выполнения метода</returns>
        public static object Invoke(this SerializedProperty property, string methodName)
        {
            var parent = property.GetParentValue<object>();
            if (parent != null && XReflection.ContainsMethod(parent, methodName))
            {
                return XReflection.InvokeMethod(parent, methodName);
            }

            var value = property.GetValue<object>();
            if (XReflection.ContainsMethod(value, methodName))
            {
                return XReflection.InvokeMethod(value, methodName);
            }

            return null;
        }

        /// <summary>
        /// Вызов метода из родительского свойства или текущего свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="methodName">Имя метода</param>
        /// <param name="arg">Аргумент метода</param>
        /// <returns>Результат выполнения метода</returns>
        public static object Invoke(this SerializedProperty property, string methodName, object arg)
        {
            var parent = property.GetParentValue<object>();
            if (parent != null && XReflection.ContainsMethod(parent, methodName))
            {
                return XReflection.InvokeMethod(parent, methodName, arg);
            }

            var value = property.GetValue<object>();
            if (XReflection.ContainsMethod(value, methodName))
            {
                return XReflection.InvokeMethod(value, methodName, arg);
            }

            return null;
        }

        /// <summary>
        /// Вызов метода из родительского свойства или текущего свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="methodName">Имя метода</param>
        /// <param name="arg1">Первый аргумент метода</param>
        /// <param name="arg2">Второй аргумент метода</param>
        /// <returns>Результат выполнения метода</returns>
        public static object Invoke(this SerializedProperty property, string methodName, object arg1, object arg2)
        {
            var parent = property.GetParentValue<object>();
            if (parent != null && XReflection.ContainsMethod(parent, methodName))
            {
                return XReflection.InvokeMethod(parent, methodName, arg1, arg2);
            }

            var value = property.GetValue<object>();
            if (XReflection.ContainsMethod(value, methodName))
            {
                return XReflection.InvokeMethod(value, methodName, arg1, arg2);
            }

            return null;
        }
        #endregion

        #region Value methods
        /// <summary>
        /// Получение значения сериализуемого свойства
        /// </summary>
        /// <remarks>
        /// http://answers.unity3d.com/questions/425012/get-the-instance-the-serializedproperty-belongs-to.html
        /// </remarks>
        /// <typeparam name="TValue">Тип значения свойства</typeparam>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Значение сериализуемого свойства</returns>
        public static TValue GetValue<TValue>(this SerializedProperty property)
        {
            object instance = property.serializedObject.targetObject;

            var path = property.propertyPath.Replace(NAME_SUFFIX_ARRAY, "[");
            var elements = path.Split('.');

            for (var i = 0; i < elements.Length; i++)
            {
                var element = elements[i];

                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = element.ExtractNumberLast();
                    instance = XReflection.GetFieldValue(instance, elementName, index);
                }
                else
                {
                    instance = XReflection.GetFieldValue(instance, element);
                }
            }

            if (instance is TValue value)
            {
                return value;
            }

            return default;
        }

        /// <summary>
        /// Получение значения сериализуемого свойства напрямую без использования рефлексии
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Значение сериализуемого свойства</returns>
        public static object GetValueDirect(this SerializedProperty property)
        {
            object result = null;
            switch (property.propertyType)
            {
                case SerializedPropertyType.Generic:
                    break;
                case SerializedPropertyType.Integer:
                    {
                        result = property.type == "long" ? property.longValue : property.intValue;
                    }
                    break;
                case SerializedPropertyType.Boolean:
                    {
                        result = property.boolValue;
                    }
                    break;
                case SerializedPropertyType.Float:
                    {
                        result = property.type == "double" ? property.doubleValue : property.floatValue;
                    }
                    break;
                case SerializedPropertyType.String:
                    {
                        result = property.stringValue;
                    }
                    break;
                case SerializedPropertyType.Color:
                    {
                        result = property.colorValue;
                    }
                    break;
                case SerializedPropertyType.ObjectReference:
                    {
                        if (property.objectReferenceValue != null)
                        {
                            result = property.objectReferenceValue;
                        }
                    }
                    break;
                case SerializedPropertyType.LayerMask:
                    {
                        result = property.intValue;
                    }
                    break;
                case SerializedPropertyType.Enum:
                    {
                        result = property.enumValueIndex;
                    }
                    break;
                case SerializedPropertyType.Vector2:
                    {
                        result = property.vector2Value;
                    }
                    break;
                case SerializedPropertyType.Vector3:
                    {
                        result = property.vector3Value;
                    }
                    break;
                case SerializedPropertyType.Vector4:
                    {
                        result = property.vector4Value;
                    }
                    break;
                case SerializedPropertyType.Rect:
                    {
                        result = property.rectValue;
                    }
                    break;
                case SerializedPropertyType.ArraySize:
                    {
                        result = property.arraySize;
                    }
                    break;
                case SerializedPropertyType.Character:
                    {
                        result = property.stringValue;
                    }
                    break;
                case SerializedPropertyType.AnimationCurve:
                    {
                        result = property.animationCurveValue;
                    }
                    break;
                case SerializedPropertyType.Bounds:
                    {
                        result = property.boundsValue;
                    }
                    break;
                case SerializedPropertyType.Gradient:
                    {
                        result = property.gradientValue;
                    }
                    break;
                case SerializedPropertyType.Quaternion:
                    {
                        result = property.quaternionValue;
                    }
                    break;
                case SerializedPropertyType.ExposedReference:
                    {
                        if (property.objectReferenceValue != null)
                        {
                            result = property.objectReferenceValue;
                        }
                    }
                    break;
                case SerializedPropertyType.FixedBufferSize:
                    break;
                case SerializedPropertyType.Vector2Int:
                    {
                        result = property.vector2IntValue;
                    }
                    break;
                case SerializedPropertyType.Vector3Int:
                    {
                        result = property.vector3IntValue;
                    }
                    break;
                case SerializedPropertyType.RectInt:
                    {
                        result = property.rectIntValue;
                    }
                    break;
                case SerializedPropertyType.BoundsInt:
                    {
                        result = property.boundsIntValue;
                    }
                    break;
                case SerializedPropertyType.ManagedReference:
                    {
                        result = property.managedReferenceValue;
                    }
                    break;
                default:
                    break;
            }

            return result;
        }

        /// <summary>
        /// Установка значения сериализуемого свойства
        /// </summary>
        /// <typeparam name="TValue">Тип значения свойства</typeparam>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="value">Значение сериализуемого свойства</param>
        public static void SetValue<TValue>(this SerializedProperty property, TValue value)
        {
            object instance = property.serializedObject.targetObject;
            var path = property.propertyPath.Replace(NAME_SUFFIX_ARRAY, "[");

            var elements = path.Split('.');
            foreach (var element in elements.Take(elements.Length - 1))
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = element.ExtractNumberLast();
                    instance = XReflection.GetFieldValue(instance, elementName, index);
                }
                else
                {
                    instance = XReflection.GetFieldValue(instance, element);
                }
            }

            if (ReferenceEquals(instance, null))
            {
                return;
            }

            try
            {
                var element = elements.Last();

                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = element.ExtractNumberLast();
                    XReflection.SetFieldValue(instance, elementName, value, index);
                }
                else
                {
                    XReflection.SetFieldValue(instance, element, value);
                }
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// Установить значение сериализуемого свойства напрямую без использования рефлексии
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="value">Значение</param>
        public static void SetValueDirect(this SerializedProperty property, object value)
        {
            property.serializedObject.Update();
            switch (property.propertyType)
            {
                case SerializedPropertyType.Generic:
                    break;
                case SerializedPropertyType.Integer:
                    {
                        if (property.type == "long")
                        {
                            property.longValue = Convert.ToInt64(value);
                        }
                        else
                        {
                            property.intValue = Convert.ToInt32(value);
                        }
                    }
                    break;
                case SerializedPropertyType.Boolean:
                    {
                        property.boolValue = Convert.ToBoolean(value);
                    }
                    break;
                case SerializedPropertyType.Float:
                    {
                        if (property.type == "double")
                        {
                            property.doubleValue = Convert.ToDouble(value);
                        }
                        else
                        {
                            property.floatValue = Convert.ToSingle(value);
                        }
                    }
                    break;
                case SerializedPropertyType.String:
                    {
                        property.stringValue = Convert.ToString(value);
                    }
                    break;
                case SerializedPropertyType.Color:
                    {
                        property.colorValue = XUnityColor.ToColor(value);
                    }
                    break;
                case SerializedPropertyType.ObjectReference:
                    {
                        if (value is UnityEngine.Object unityObject)
                        {
                            property.objectReferenceValue = unityObject;
                        }
                    }
                    break;
                case SerializedPropertyType.LayerMask:
                    {
                        property.intValue = XUnityLayerMask.ToLayerMask(value);
                    }
                    break;
                case SerializedPropertyType.Enum:
                    {
                        property.SetEnumValue(value);
                    }
                    break;
                case SerializedPropertyType.Vector2:
                    {
                        property.vector2Value = XUnityVector2.ToVector(value);
                    }
                    break;
                case SerializedPropertyType.Vector3:
                    {
                        property.vector3Value = XUnityVector3.ToVector(value);
                    }
                    break;
                case SerializedPropertyType.Vector4:
                    {
                        property.vector4Value = XUnityVector4.ToVector(value);
                    }
                    break;
                case SerializedPropertyType.Rect:
                    {
                        property.rectValue = XUnityRect.ToRect(value);
                    }
                    break;
                case SerializedPropertyType.ArraySize:
                    break;
                case SerializedPropertyType.Character:
                    break;
                case SerializedPropertyType.AnimationCurve:
                    {
                        if (value is AnimationCurve animationCurve)
                        {
                            property.animationCurveValue = animationCurve;
                        }
                    }
                    break;
                case SerializedPropertyType.Bounds:
                    {
                        property.boundsValue = XUnityBounds.ToBounds(value);
                    }
                    break;
                case SerializedPropertyType.Gradient:
                    {
                        if (value is Gradient gradient)
                        {
                            property.gradientValue = gradient;
                        }
                    }
                    break;
                case SerializedPropertyType.Quaternion:
                    {
                        property.quaternionValue = XUnityQuaternion.ToQuaternion(value);
                    }
                    break;
                case SerializedPropertyType.ExposedReference:
                    {
                        property.exposedReferenceValue = (UnityEngine.Object)value;
                    }
                    break;
                case SerializedPropertyType.FixedBufferSize:
                    break;
                case SerializedPropertyType.Vector2Int:
                    {
                        property.vector2IntValue = XUnityVector2Int.ToVector(value);
                    }
                    break;
                case SerializedPropertyType.Vector3Int:
                    {
                        property.vector3IntValue = XUnityVector3Int.ToVector(value);
                    }
                    break;
                case SerializedPropertyType.RectInt:
                    {
                        property.rectIntValue = XUnityRectInt.ToRect(value);
                    }
                    break;
                case SerializedPropertyType.BoundsInt:
                    {
                        property.boundsIntValue = XUnityBoundsInt.ToBounds(value);
                    }
                    break;
                case SerializedPropertyType.ManagedReference:
                    {
                        property.managedReferenceValue = value;
                    }
                    break;
                default:
                    break;
            }

            property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Ограничение значения сериализуемого свойства по максимуму
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="maxValue">Максимальное значение</param>
        public static void SetValueDirectMaximum(this SerializedProperty property, object maxValue)
        {
            property.serializedObject.Update();
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    {
                        if (property.type == "long")
                        {
                            var max = Convert.ToInt64(maxValue);
                            if (property.longValue > max)
                            {
                                property.longValue = max;
                            }
                        }
                        else
                        {
                            var max = Convert.ToInt32(maxValue);
                            if (property.intValue > max)
                            {
                                property.intValue = max;
                            }
                        }
                    }
                    break;
                case SerializedPropertyType.Float:
                    {
                        if (property.type == "double")
                        {
                            var max = Convert.ToDouble(maxValue);
                            if (property.doubleValue > max)
                            {
                                property.doubleValue = max;
                            }
                        }
                        else
                        {
                            var max = Convert.ToSingle(maxValue);
                            if (property.floatValue > max)
                            {
                                property.floatValue = max;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Ограничение значения сериализуемого свойства по минимуму
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="minValue">Минимальное значение</param>
        public static void SetValueDirectMinimum(this SerializedProperty property, object minValue)
        {
            property.serializedObject.Update();
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    {
                        if (property.type == "long")
                        {
                            var min = Convert.ToInt64(minValue);
                            if (property.longValue < min)
                            {
                                property.longValue = min;
                            }
                        }
                        else
                        {
                            var min = Convert.ToInt32(minValue);
                            if (property.intValue < min)
                            {
                                property.intValue = min;
                            }
                        }
                    }
                    break;
                case SerializedPropertyType.Float:
                    {
                        if (property.type == "double")
                        {
                            var min = Convert.ToDouble(minValue);
                            if (property.doubleValue < min)
                            {
                                property.doubleValue = min;
                            }
                        }
                        else
                        {
                            var min = Convert.ToSingle(minValue);
                            if (property.floatValue < min)
                            {
                                property.floatValue = min;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Изменения значения сериализуемого свойства (прибавление)
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="deltaValue">Шаг приращения</param>
        public static void SetValueDirectAdd(this SerializedProperty property, object deltaValue)
        {
            property.serializedObject.Update();
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    {
                        if (property.type == "long")
                        {
                            property.longValue += Convert.ToInt64(deltaValue);
                        }
                        else
                        {
                            property.intValue += Convert.ToInt32(deltaValue);
                        }
                    }
                    break;
                case SerializedPropertyType.Float:
                    {
                        if (property.type == "double")
                        {
                            property.doubleValue += Convert.ToDouble(deltaValue);
                        }
                        else
                        {
                            property.floatValue += Convert.ToSingle(deltaValue);
                        }
                    }
                    break;
                default:
                    break;
            }

            property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Изменения значения сериализуемого свойства (уменьшение)
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="deltaValue">Шаг приращения</param>
        public static void SetValueDirectSubtract(this SerializedProperty property, object deltaValue)
        {
            property.serializedObject.Update();
            switch (property.propertyType)
            {
                case SerializedPropertyType.Integer:
                    {
                        if (property.type == "long")
                        {
                            property.longValue -= Convert.ToInt64(deltaValue);
                        }
                        else
                        {
                            property.intValue -= Convert.ToInt32(deltaValue);
                        }
                    }
                    break;
                case SerializedPropertyType.Float:
                    {
                        if (property.type == "double")
                        {
                            property.doubleValue -= Convert.ToDouble(deltaValue);
                        }
                        else
                        {
                            property.floatValue -= Convert.ToSingle(deltaValue);
                        }
                    }
                    break;
                default:
                    break;
            }

            property.serializedObject.ApplyModifiedProperties();
        }

        /// <summary>
        /// Изменения значения сериализуемого свойства (уменьшение)
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="deltaValue">Шаг приращения</param>
        [Obsolete("Опечатка в имени. Используйте SetValueDirectSubtract")]
        public static void SetValueDirectSubstract(this SerializedProperty property, object deltaValue)
        {
            property.SetValueDirectSubtract(deltaValue);
        }
        #endregion

        #region Enum methods
        /// <summary>
        /// Установить значение сериализуемого свойства указанного типа перечисления
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления</typeparam>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="value">Значение</param>
        public static void SetEnumValue<TEnum>(this SerializedProperty property, TEnum value) where TEnum : struct
        {
            if (property.propertyType == SerializedPropertyType.Enum)
            {
                property.intValue = Convert.ToInt32(value);
            }
            else
            {
                Debug.LogErrorFormat("SerializedProperty <{0}> is not an enumeration type", property.name);
            }
        }

        /// <summary>
        /// Установить значение сериализуемого свойства перечислением
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="value">Значение</param>
        public static void SetEnumValue(this SerializedProperty property, Enum value)
        {
            if (property.propertyType == SerializedPropertyType.Enum)
            {
                if (value == null)
                {
                    property.enumValueIndex = 0;
                }
                else
                {
                    property.intValue = Convert.ToInt32(value);
                }
            }
            else
            {
                Debug.LogErrorFormat("SerializedProperty <{0}> is not an enumeration type", property.name);
            }
        }

        /// <summary>
        /// Установить значение сериализуемого свойства базовым объектом
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="value">Значение</param>
        public static void SetEnumValue(this SerializedProperty property, object value)
        {
            if (property.propertyType == SerializedPropertyType.Enum)
            {
                if (value == null)
                {
                    property.enumValueIndex = 0;
                }
                else
                {
                    property.intValue = Convert.ToInt32(value);
                }
            }
            else
            {
                Debug.LogErrorFormat("SerializedProperty <{0}> is not an enumeration type", property.name);
            }
        }

        /// <summary>
        /// Получить значение сериализуемого свойства указанного типа перечисления
        /// </summary>
        /// <typeparam name="TEnum">Тип перечисления</typeparam>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Значение</returns>
        public static TEnum GetEnumValue<TEnum>(this SerializedProperty property) where TEnum : Enum
        {
            if (property.propertyType == SerializedPropertyType.Enum)
            {
                try
                {
                    return XEnumConverter.ToEnum<TEnum>(property.intValue);
                }
                catch
                {
                    return default;
                }
            }

            Debug.LogErrorFormat("SerializedProperty <{0}> is not an enumeration type", property.name);
            return default;
        }

        /// <summary>
        /// Получить значение сериализуемого свойства указанного типа перечисления
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="enumType">Тип перечисления</param>
        /// <returns>Значение</returns>
        public static Enum GetEnumValue(this SerializedProperty property, Type enumType)
        {
            if (!enumType.IsEnum)
            {
                Debug.LogErrorFormat("Type <{0}> must be an enumerated type", enumType.Name);
                return null;
            }

            if (property.propertyType == SerializedPropertyType.Enum)
            {
                try
                {
                    return XEnumConverter.ToEnumOfType(enumType, property.intValue);
                }
                catch
                {
                    return Enum.GetValues(enumType).Cast<Enum>().First();
                }
            }

            Debug.LogErrorFormat("SerializedProperty <{0}> is not an enumeration type", property.name);
            return null;
        }
        #endregion

        #region Parent methods
        /// <summary>
        /// Получение пути родительского свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Путь к родительскому свойству</returns>
        public static string GetParentPath(this SerializedProperty property)
        {
            var lastDot = property.propertyPath.LastIndexOf('.');
            if (lastDot == -1)
            {
                return "";
            }

            return property.propertyPath.Substring(0, lastDot);
        }

        /// <summary>
        /// Получение родительского свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Родительское свойство</returns>
        public static SerializedProperty GetParentProperty(this SerializedProperty property)
        {
            var lastDot = property.propertyPath.LastIndexOf('.');
            if (lastDot == -1)
            {
                return property;
            }

            var parentPath = property.propertyPath.Substring(0, lastDot);
            return property.serializedObject.FindProperty(parentPath);
        }

        /// <summary>
        /// Получение значения родительского свойства или непосредственно компонента сериализуемого свойства
        /// </summary>
        /// <typeparam name="TValue">Тип значения свойства</typeparam>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Значение родительского свойства сериализуемого свойства</returns>
        public static TValue GetParentValue<TValue>(this SerializedProperty property)
        {
            object instance = property.serializedObject.targetObject;

            var path = property.propertyPath.Replace(NAME_SUFFIX_ARRAY, "[");
            var elements = path.Split('.');

            foreach (var element in elements.Take(elements.Length - 1))
            {
                if (element.Contains("["))
                {
                    var elementName = element.Substring(0, element.IndexOf("["));
                    var index = element.ExtractNumberLast();
                    instance = XReflection.GetFieldValue(instance, elementName, index);
                }
                else
                {
                    instance = XReflection.GetFieldValue(instance, element);
                }
            }

            return (TValue)instance;
        }
        #endregion

        #region Editor prefs methods
        /// <summary>
        /// Получение имени свойства привязанного к конкретному компоненту (игровому объекту)
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Уникальное имя</returns>
        public static string GetHashNameInstance(this SerializedProperty property)
        {
            return property.propertyPath + "_" + property.serializedObject.targetObject.GetEntityId().ToString();
        }

        /// <summary>
        /// Получение имени свойства привязанного к типу
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Уникальное имя</returns>
        public static string GetHashNameType(this SerializedProperty property)
        {
            var typeName = property.serializedObject.targetObject.GetType().FullName;
            if (property.propertyPath.IndexOf(NAME_SUFFIX_ARRAY, StringComparison.Ordinal) > -1)
            {
                var propertyPath = property.propertyPath.Replace(NAME_SUFFIX_ARRAY, "[");
                return typeName + '.' + propertyPath.RemoveAllBetweenSymbolWithSymbols('[', ']');
            }

            return typeName + '.' + property.propertyPath;
        }

        /// <summary>
        /// Сохранение значения по имени свойства привязанного к конкретному компоненту (игровому объекту)
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="prefix">Префикс имени</param>
        /// <param name="value">Значение</param>
        public static void SaveBoolEditor(this SerializedProperty property, string prefix, bool value)
        {
            EditorPrefs.SetBool(property.GetHashNameInstance() + prefix, value);
        }

        /// <summary>
        /// Загрузка значения по имени свойства привязанного к конкретному компоненту (игровому объекту)
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <param name="prefix">Префикс имени</param>
        /// <param name="defaultValue">Значение по умолчанию</param>
        /// <returns>Значение</returns>
        public static bool LoadEditorBool(this SerializedProperty property, string prefix, bool defaultValue = true)
        {
            return EditorPrefs.GetBool(property.GetHashNameInstance() + prefix, defaultValue);
        }
        #endregion

        #region Collection methods
        /// <summary>
        /// Проверка свойства на тип коллекции
        /// </summary>
        /// <remarks>
        /// К коллекциям относятся как стандартные коллекции так и коллекции системы Lotus
        /// </remarks>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Статус коллекции свойства</returns>
        public static bool IsCollection(this SerializedProperty property)
        {
            if (property.isArray && property.propertyType != SerializedPropertyType.String)
            {
                return true;
            }

            if (property.propertyType == SerializedPropertyType.Generic)
            {
                if (property.FindPropertyRelative(NAME_LOTUS_ARRAY) != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Проверка на элемент коллекции
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Статус элемент коллекции</returns>
        public static bool IsElementCollection(this SerializedProperty property)
        {
            return property.propertyPath[property.propertyPath.Length - 1] == ']';
        }

        /// <summary>
        /// Проверка свойства на тип стандартной коллекции: Array или List
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Статус коллекции свойства</returns>
        public static bool IsStandardCollection(this SerializedProperty property)
        {
            return property.isArray && property.propertyType != SerializedPropertyType.String;
        }

        /// <summary>
        /// Проверка свойства на тип коллекции Lotus: производные коллекции от <see cref="ListArray{TItem}"/>
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Статус коллекции свойства</returns>
        public static bool IsLotusCollection(this SerializedProperty property)
        {
            if (property.propertyType == SerializedPropertyType.Generic)
            {
                if (property.FindPropertyRelative(NAME_LOTUS_ARRAY) != null)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Получение сериализуемого свойства внутреннего массива данных от коллекции Lotus
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Сериализируемое свойство внутреннего массива данных</returns>
        public static SerializedProperty GetLotusInnerCollection(this SerializedProperty property)
        {
            return property.FindPropertyRelative(NAME_LOTUS_ARRAY);
        }
        #endregion

        #region Attribute methods
        /// <summary>
        /// Получение атрибута
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибута</typeparam>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Атрибут</returns>
        public static TAttribute GetAttribute<TAttribute>(this SerializedProperty property) where TAttribute : Attribute
        {
            return XEditorCachedData.GetMember(property).GetAttribute<TAttribute>();
        }
        #endregion
    }
    /**@}*/
}
#endif
