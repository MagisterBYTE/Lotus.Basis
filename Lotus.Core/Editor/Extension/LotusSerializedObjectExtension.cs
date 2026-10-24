#if UNITY_EDITOR
using System;

using Lotus.Core;

using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Статический класс реализующий методы расширения класса SerializedObject
    /// </summary>
    /// <remarks>
    /// Только для режима редактора Unity
    /// </remarks>
    public static class XSerializedObjectExtension
    {
        #region Main methods
        /// <summary>
        /// Сохранение сериализуемого объекта
        /// </summary>
        /// <remarks>
        /// Помечает объект и сцену как изменённые для возможности сохранить данные на диск
        /// </remarks>
        /// <param name="serializedObject">Сериализируемый объект</param>
        public static void Save(this SerializedObject serializedObject)
        {
            EditorUtility.SetDirty(serializedObject.targetObject);
            if (!EditorApplication.isPlaying && serializedObject.targetObject is Component component)
            {
                EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
            }
        }

        /// <summary>
        /// Получение значения указанного члена данных
        /// </summary>
        /// <typeparam name="TValue">Тип значения</typeparam>
        /// <param name="serializedObject">Сериализируемый объект</param>
        /// <param name="memberName">Имя члена данных</param>
        /// <param name="memberType">Тип члена объекта для атрибутов поддержки инспектора свойств</param>
        /// <returns>Значение члена данных</returns>
        public static TValue GetValueFromMember<TValue>(this SerializedObject serializedObject, string memberName,
            TInspectorMemberType memberType)
        {
            object instance = serializedObject.targetObject;
            object result = null;

            switch (memberType)
            {
                case TInspectorMemberType.Field:
                    {
                        result = XReflection.GetFieldValue(instance, memberName);
                    }
                    break;
                case TInspectorMemberType.Property:
                    {
                        result = XReflection.GetPropertyValue(instance, memberName);
                    }
                    break;
                case TInspectorMemberType.Method:
                    {
                        result = XReflection.InvokeMethod(instance, memberName);
                    }
                    break;
                default:
                    break;
            }

            if (result is TValue value)
            {
                return value;
            }

            Debug.LogErrorFormat("Could not cast type {0} to the correct type for data member {1}",
                typeof(TValue).Name, memberName);
            return default;
        }
        #endregion

        #region Editor prefs methods
        /// <summary>
        /// Получение имени свойства привязанного к конкретному компоненту (игровому объекту)
        /// </summary>
        /// <param name="obj">Сериализируемый объект</param>
        /// <returns>Уникальное имя</returns>
        public static string GetHashNameInstance(this SerializedObject obj)
        {
            return obj.targetObject.GetEntityId().ToString();
        }

        /// <summary>
        /// Получение имени свойства привязанного к типу
        /// </summary>
        /// <param name="obj">Сериализируемый объект</param>
        /// <returns>Уникальное имя</returns>
        [Obsolete("Не используется. Для SerializedProperty используйте XSerializedPropertyExtension.GetHashNameType")]
        public static string GetHashNameType(this SerializedObject obj)
        {
            return obj.targetObject.GetType().FullName;
        }

        /// <summary>
        /// Сохранение значения по имени свойства привязанного к конкретному компоненту (игровому объекту)
        /// </summary>
        /// <param name="obj">Сериализируемый объект</param>
        /// <param name="prefix">Префикс имени</param>
        /// <param name="value">Значение</param>
        public static void SaveBoolEditor(this SerializedObject obj, string prefix, bool value)
        {
            EditorPrefs.SetBool(obj.GetHashNameInstance() + prefix, value);
        }

        /// <summary>
        /// Загрузка значения по имени свойства привязанного к конкретному компоненту (игровому объекту)
        /// </summary>
        /// <param name="obj">Сериализируемый объект</param>
        /// <param name="prefix">Префикс имени</param>
        /// <param name="defaultValue">Значение по умолчанию</param>
        /// <returns>Значение</returns>
        public static bool LoadEditorBool(this SerializedObject obj, string prefix, bool defaultValue = true)
        {
            return EditorPrefs.GetBool(obj.GetHashNameInstance() + prefix, defaultValue);
        }
        #endregion
    }
    /**@}*/
}
#endif
