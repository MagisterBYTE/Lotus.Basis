#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;

using Lotus.Core;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Класс для хранения метаданных члена объекта и связанных с ним атрибутов
    /// </summary>
    public class CCachedMember
    {
        #region Fields
        /// <summary>
        /// Полный путь
        /// </summary>
        public string FullPath;

        /// <summary>
        /// Метаданные члена объекта
        /// </summary>
        public MemberInfo Member;

        /// <summary>
        /// Все атрибуты объявленные в члене объекта
        /// </summary>
        public Attribute[] Attributes;
        #endregion

        #region Constructors
        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        public CCachedMember(SerializedProperty property)
        {
            FullPath = property.GetHashNameType();
            Member = property.GetFieldInfo();
            if (Member != null)
            {
                Attributes = Member.GetAttributes<Attribute>();
            }
        }

        /// <summary>
        /// Конструктор инициализирует объект класса указанными параметрами
        /// </summary>
        /// <param name="memberInfo">Метаданные члена объекта</param>
        public CCachedMember(MemberInfo memberInfo)
        {
            FullPath = memberInfo.GetMemberPath();
            Member = memberInfo;
            if (Member != null)
            {
                Attributes = Member.GetAttributes<Attribute>();
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение атрибута
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибута</typeparam>
        /// <returns>Атрибут</returns>
        public TAttribute GetAttribute<TAttribute>() where TAttribute : Attribute
        {
            if (Attributes != null)
            {
                for (var i = 0; i < Attributes.Length; i++)
                {
                    if (Attributes[i] is TAttribute attribute)
                    {
                        return attribute;
                    }
                }
            }

            return null;
        }
        #endregion
    }

    /// <summary>
    /// Статический класс реализующий кэшированные данные для инспектора свойств
    /// </summary>
    /// <remarks>
    /// Только для режима редактора Unity
    /// </remarks>
    public static class XEditorCachedData
    {
        #region Fields
        private static Dictionary<string, CCachedMember> _members = new Dictionary<string, CCachedMember>();
        private static Dictionary<string, UnityEditor.Editor> _editors = new Dictionary<string, UnityEditor.Editor>();
        #endregion

        #region Properties
        /// <summary>
        /// Словарь кэшированных метаданных членов объекта
        /// </summary>
        public static Dictionary<string, CCachedMember> Members
        {
            get
            {
                if (_members == null)
                {
                    _members = new Dictionary<string, CCachedMember>();
                }

                return _members;
            }
        }

        /// <summary>
        /// Словарь кэшированных редакторов объектов
        /// </summary>
        public static Dictionary<string, UnityEditor.Editor> Editors
        {
            get
            {
                if (_editors == null)
                {
                    _editors = new Dictionary<string, UnityEditor.Editor>();
                }

                return _editors;
            }
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получить кэшированные метаданные для указанного свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Кэшированные метаданные</returns>
        public static CCachedMember GetMember(SerializedProperty property)
        {
            var key = property.GetHashNameType();
            if (Members.TryGetValue(key, out var result))
            {
                return result;
            }

            result = new CCachedMember(property);
            Members.Add(key, result);
            return result;
        }

        /// <summary>
        /// Получить кэшированные метаданные для указанного члена объекта
        /// </summary>
        /// <param name="memberInfo">Метаданные члена объекта</param>
        /// <returns>Кэшированные метаданные</returns>
        public static CCachedMember GetMember(MemberInfo memberInfo)
        {
            var key = memberInfo.GetMemberPath();
            if (Members.TryGetValue(key, out var result))
            {
                return result;
            }

            result = new CCachedMember(memberInfo);
            Members.Add(key, result);
            return result;
        }

        /// <summary>
        /// Получение кэшированного редактора объекта для указанного свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Редактор объекта</returns>
        public static UnityEditor.Editor GetEditor(SerializedProperty property)
        {
            var key = property.GetHashNameInstance();
            if (Editors.TryGetValue(key, out var result))
            {
                // Только если ссылаемся на один и тот же объект
                if (result.target == property.objectReferenceValue)
                {
                    return result;
                }

                // Удаляем
                UnityEngine.Object.DestroyImmediate(result);
                Editors.Remove(key);

                // Создаем новый
                result = UnityEditor.Editor.CreateEditor(property.objectReferenceValue);
                Editors.Add(key, result);
                return result;
            }

            result = UnityEditor.Editor.CreateEditor(property.objectReferenceValue);
            Editors.Add(key, result);
            return result;
        }

        /// <summary>
        /// Принудительная очистка(удаление) редакторов объектов
        /// </summary>
        public static void ClearEditors()
        {
            if (_editors != null && _editors.Count > 0)
            {
                foreach (var item in Editors.Values)
                {
                    if (item != null)
                    {
                        UnityEngine.Object.DestroyImmediate(item);
                    }
                }

                _editors.Clear();
            }
        }

        /// <summary>
        /// Получение атрибута
        /// </summary>
        /// <typeparam name="TAttribute">Тип атрибута</typeparam>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Атрибут</returns>
        public static TAttribute GetAttribute<TAttribute>(SerializedProperty property) where TAttribute : Attribute
        {
            var result = GetMember(property);
            return result.GetAttribute<TAttribute>();
        }
        #endregion
    }
    /**@}*/
}
#endif
