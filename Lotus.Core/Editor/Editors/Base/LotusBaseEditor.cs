#if UNITY_EDITOR
using System;
using System.Collections.Generic;
using System.Reflection;

using Lotus.Core;
using Lotus.Core.Inspector;

using UnityEditor;
using UnityEngine;

namespace Lotus.Editor
{
    /** \addtogroup CoreUnityEditor
    *@{*/
    /// <summary>
    /// Базовый редактор для редактирования свойств в инспекторе объектов
    /// </summary>
    public class LotusInspectorBaseEditor : UnityEditor.Editor, IDisposable
    {
        #region Fields
        protected Dictionary<string, CReorderableList> _reorderableLists;
        protected List<CMethodHolder> _methodHolders;
        #endregion

        #region Unity events
        /// <summary>
        /// Включение скрипта в инспекторе объектов
        /// </summary>
        protected virtual void OnEnable()
        {
            _reorderableLists = new Dictionary<string, CReorderableList>();

            // Работа с методами
            _methodHolders = new List<CMethodHolder>();
            GetMethodHolders();
        }

        /// <summary>
        /// Отключение скрипта в инспекторе объектов
        /// </summary>
        protected virtual void OnDisable()
        {
            XEditorCachedData.ClearEditors();
        }

        /// <summary>
        /// Рисование в инспекторе объектов
        /// </summary>
        public override void OnInspectorGUI()
        {
            EditorGUILayout.LabelField("Custom Inspector Lotus", EditorStyles.centeredGreyMiniLabel);

            // Проверяем на нулевой ссылку
            if (serializedObject.targetObject == null)
            {
                EditorGUILayout.LabelField("Error: Script not found", EditorStyles.centeredGreyMiniLabel);

                serializedObject.Update();
                {
                    var propertyScript = serializedObject.FindProperty("m_Script");
                    EditorGUILayout.PropertyField(propertyScript);
                }
                serializedObject.ApplyModifiedProperties();
            }
            else
            {
                var descriptionType = target.GetType().GetAttribute<LotusDescriptionTypeAttribute>();
                if (descriptionType != null)
                {
                    EditorGUILayout.HelpBox(descriptionType.Description, MessageType.Info);
                }

                serializedObject.Update();
                {
                    var property = serializedObject.GetIterator();
                    var next = property.NextVisible(true);
                    if (next)
                    {
                        do
                        {
                            DisplayingProperty(property);
                        }
                        while (property.NextVisible(false));
                    }
                }
                serializedObject.ApplyModifiedProperties();

                if (_methodHolders.Count > 0)
                {
                    DrawMethodHolders();
                }
            }
        }
        #endregion

        #region IDisposable methods
        /// <summary>
        /// Освобождение управляемых ресурсов
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Освобождение управляемых ресурсов
        /// </summary>
        /// <param name="disposing">Статус освобождения</param>
        protected virtual void Dispose(bool disposing)
        {
            // Освобождаем только управляемые ресурсы
            if (disposing)
            {
                if (_reorderableLists != null)
                {
                    foreach (var item in _reorderableLists.Values)
                    {
                        item.Dispose();
                    }

                    _reorderableLists.Clear();
                    _reorderableLists = null;
                }
            }

            // Освобождаем неуправляемые ресурсы
        }
        #endregion

        #region Main methods
        /// <summary>
        /// Получение и отображение свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        protected void DisplayingProperty(SerializedProperty property)
        {
            // Для ссылки на свой скрипт выключаем доступность
            if (property.IsScript())
            {
                GUI.enabled = false;
                EditorGUILayout.PropertyField(property, property.isExpanded);
            }
            else
            {
                GUI.enabled = true;

                if (property.IsLotusCollection() ||
                    (property.IsStandardCollection() && property.GetAttribute<LotusReorderableAttribute>() != null))
                {
                    // Коллекция Lotus отображаемая в списке
                    DisplayingCollection(property);
                }
                else
                {
                    if (property.GetAttribute<LotusInGroupAttribute>() == null)
                    {
                        XEditorInspector.PrepareContent(property);
                        EditorGUILayout.PropertyField(property, XEditorInspector.Content, property.isExpanded);
                    }
                }
            }
        }

        /// <summary>
        /// Получение и отображение коллекции
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        protected void DisplayingCollection(SerializedProperty property)
        {
            XEditorInspector.DrawDecorateAttributes(property);

            var listData = Get(property);
            listData.IsExpanded = property.isExpanded;
            listData.DrawLayout();
        }

        /// <summary>
        /// Получение управляемого списка для указанного свойства
        /// </summary>
        /// <param name="property">Сериализируемое свойство</param>
        /// <returns>Управляемый список</returns>
        public CReorderableList Get(SerializedProperty property)
        {
            if (_reorderableLists.TryGetValue(property.propertyPath, out var result))
            {
                result.Reset(property);
                return result;
            }

            result = new CReorderableList(property);
            _reorderableLists.Add(property.propertyPath, result);
            return result;
        }
        #endregion

        #region Method holders
        /// <summary>
        /// Получение данных методов которые доступны для вызова из инспектора свойств
        /// </summary>
        public void GetMethodHolders()
        {
            var type = target.GetType();
            var allMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            for (var i = 0; i < allMethods.Length; i++)
            {
                var methodCalls = Attribute.GetCustomAttribute(allMethods[i],
                    typeof(LotusMethodCallAttribute)) as LotusMethodCallAttribute;
                if (methodCalls != null)
                {
                    var method = allMethods[i];
                    var args = method.GetParameters();
                    switch (args.Length)
                    {
                        case 0:
                            {
                                var methodHolder = new CMethodHolder();
                                methodHolder.MethodAttribute = methodCalls;
                                methodHolder.Method = method;
                                methodHolder.Instance = target;
                                _methodHolders.Add(methodHolder);
                            }
                            break;
                        case 1:
                            {
                                var methodHolder = new CMethodHolderArg1();
                                methodHolder.MethodAttribute = methodCalls;
                                methodHolder.Method = method;
                                methodHolder.Instance = target;
                                methodHolder.FirstArgumentName = args[0].Name;
                                methodHolder.FirstArgumentTypeName = args[0].ParameterType.Name;
                                methodHolder.FirstArgument.Set(args[0].ParameterType);
                                _methodHolders.Add(methodHolder);
                            }
                            break;
                        case 2:
                            {
                                var methodHolder = new CMethodHolderArg2();
                                methodHolder.MethodAttribute = methodCalls;
                                methodHolder.Method = method;
                                methodHolder.Instance = target;
                                methodHolder.FirstArgumentName = args[0].Name;
                                methodHolder.FirstArgumentTypeName = args[0].ParameterType.Name;
                                methodHolder.FirstArgument.Set(args[0].ParameterType);
                                methodHolder.SecondArgumentName = args[1].Name;
                                methodHolder.SecondArgumentTypeName = args[1].ParameterType.Name;
                                methodHolder.SecondArgument.Set(args[1].ParameterType);
                                _methodHolders.Add(methodHolder);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            _methodHolders.Sort((x, y) =>
            {
                return x.MethodAttribute.DrawOrder.CompareTo(y.MethodAttribute.DrawOrder);
            });
        }

        /// <summary>
        /// Рисование кнопок
        /// </summary>
        public void DrawMethodHolders()
        {
            for (var i = 0; i < _methodHolders.Count; i++)
            {
                _methodHolders[i].Draw();
            }
        }
        #endregion
    }
    /**@}*/
}
#endif
