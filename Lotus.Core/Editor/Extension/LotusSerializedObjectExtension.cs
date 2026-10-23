
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusSerializedObjectExtension.cs
*		Методы расширений класса SerializedObject.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using UnityEngine;
using UnityEditor;
------------
using Lotus.Core;

namespace Lotus
{
	namespace Editor
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
			/// Эта старая методика для информирования об изменении данных - информирование об этом сцены для возможности 
			/// сохранить данные на диск
			/// </remarks>
			/// <param name="serializedObject">Сериализируемый объект</param>
			
			public static void Save(this SerializedObject serializedObject)
			{
				UnityEditor.EditorUtility.SetDirty(serializedObject.targetObject);
				if (UnityEditor.EditorApplication.isPlaying == false)
				{
                    var component = serializedObject.targetObject as MonoBehaviour;
					if (component != null)
					{
						UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
					}
				}
			}

			
			/// <summary>
			/// Получение значения указанного члена данных
			/// </summary>
			/// <typeparam name="TValue">Тип значения</typeparam>
			/// <param name="serializedObject">Сериализируемый объект</param>
			/// <param name="member_name">Имя члена данных</param>
			/// <param name="member_type">Тип члена объекта для атрибутов поддержки инспектора свойств</param>
			/// <returns></returns>
			
			public static TValue GetValueFromMember<TValue>(this SerializedObject serializedObject, String member_name, TInspectorMemberType member_type)
			{
				System.Object instance = serializedObject.targetObject;

				System.Object result = null;
				switch (member_type)
				{
					case TInspectorMemberType.Field:
						{
							result = XReflection.GetFieldValue(instance, member_name);
						}
						break;
					case TInspectorMemberType.Property:
						{
							result = XReflection.GetPropertyValue(instance, member_name);
						}
						break;
					case TInspectorMemberType.Method:
						{
							result = XReflection.InvokeMethod(instance, member_name);
						}
						break;
					default:
						break;
				}

				if (result is TValue)
				{
					return (TValue)(result);
				}
				else
				{
					Debug.LogErrorFormat("Could not cast type {0} to the correct type for data member {1}", typeof(TValue).Name,
						member_name);
					return default(TValue);
				}
			}
			#endregion

			#region ======================================= РАБОТА С СОХРАНЕНИЕМ/ЗАГРУЗКОЙ ЗНАЧЕНИЯ ===================
			
			/// <summary>
			/// Получение имени свойства привязанного к конкретному компоненту (игровому объекту)
			/// </summary>
			/// <param name="obj">Сериализируемый объект</param>
			/// <returns>Уникальное имя</returns>
			
			public static String GetHashNameInstance(this SerializedObject obj)
			{
				return (obj.targetObject.GetInstanceID().ToString());
			}

			
			/// <summary>
			/// Получение имени свойства привязанного к типу
			/// </summary>
			/// <param name="obj">Сериализируемый объект</param>
			/// <returns>Уникальное имя</returns>
			
			public static String GetHashNameType(this SerializedObject obj)
			{
				return (obj.targetObject.GetType().FullName);
			}

			
			/// <summary>
			/// Сохранение значение по имени свойства привязанного к конкретному компоненту (игровому объекту)
			/// </summary>
			/// <param name="obj">Сериализируемый объект</param>
			/// <param name="prefix">Префикс имени</param>
			/// <param name="value">Значение</param>
			
			public static void SaveBoolEditor(this SerializedObject obj, String prefix, Boolean value)
			{
				UnityEditor.EditorPrefs.SetBool(obj.GetHashNameInstance() + prefix, value);
			}

			
			/// <summary>
			/// Загрузка значение по имени свойства привязанного к конкретному компоненту (игровому объекту)
			/// </summary>
			/// <param name="obj">Сериализируемый объект</param>
			/// <param name="prefix">Префикс имени</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Значение</returns>
			
			public static Boolean LoadEditorBool(this SerializedObject obj, String prefix, Boolean default_value = true)
			{
				return (UnityEditor.EditorPrefs.GetBool(obj.GetHashNameInstance() + prefix, default_value));
			}
			#endregion
		}

		/**@}*/

	}
}
