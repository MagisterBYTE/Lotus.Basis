
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusSerializedPropertyExtension.cs
*		Методы расширений класса SerializedProperty.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
------------
using Lotus.Core;

namespace Lotus
{
	namespace Editor
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
			#region Const
			/// <summary>
			/// Имя внутреннего массива для всех типов коллекций системы Lotus
			/// </summary>
			public const String NAME_LOTUS_ARRAY = nameof(ListArray<Int32>._arrayOfItems);

			/// <summary>
			/// Имя суффикса массива
			/// </summary>
			public const String NAME_SUFFIX_ARRAY = ".Array.data[";
			#endregion

			#region Main methods
			
			/// <summary>
			/// Проверка на свойство скрипта
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Статус скрипта</returns>
			
			public static Boolean IsScript(this UnityEditor.SerializedProperty property)
			{
				return (property.name.Equals("m_Script") &&
						property.type.Equals("PPtr<MonoScript>") &&
						property.propertyType == UnityEditor.SerializedPropertyType.ObjectReference &&
						property.propertyPath.Equals("m_Script"));
			}

			
			/// <summary>
			/// Проверка на свойство первого/корневого уровня
			/// </summary>
			/// <remarks>
			/// Первый/корневой уровень означает что свойство объявлено непосредственно в компоненте
			/// </remarks>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Статус свойства первого/корневого уровня</returns>
			
			public static Boolean IsRoot(this UnityEditor.SerializedProperty property)
			{
				var index = property.propertyPath.IndexOf(XChar.Dot);
				return (index == -1);
			}

			
			/// <summary>
			/// Получение объекта которому принадлежит указанное сериализуемое свойство
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Объект</returns>
			
			public static System.Object GetInstance(this UnityEditor.SerializedProperty property)
			{
				return (property.serializedObject.targetObject);
			}

			
			/// <summary>
			/// Получение поля данных которое формирует указанное сериализуемое свойство
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Поле данных свойства</returns>
			
			public static FieldInfo GetFieldInfo(this UnityEditor.SerializedProperty property)
			{
				// Получаем объект
				System.Object instance = property.serializedObject.targetObject;

				FieldInfo field_info = null;

				// Проверяем на корневое свойство
				if (property.IsRoot())
				{
					if (instance != null)
					{
						// Получаем поле данных
						field_info = XReflection.GetField(instance, property.propertyPath);
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
							var element_name = element.Substring(0, element.IndexOf("["));
							var index = element.ExtractNumberLast();
							instance = XReflection.GetFieldValue(instance, element_name, index, out field_info);
						}
						else
						{
							instance = XReflection.GetFieldValue(instance, element, out field_info);
						}
					}
				}

				return (field_info);
			}

			
			/// <summary>
			/// Получение строкового значения сериализуемого свойства
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Строковое значение</returns>
			
			public static String AsString(this UnityEditor.SerializedProperty property)
			{
				var result = "";
				switch (property.propertyType)
				{
					case UnityEditor.SerializedPropertyType.Generic:
						{

						}
						break;
					case UnityEditor.SerializedPropertyType.Integer:
						{
							result = property.intValue.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.Boolean:
						{
							result = property.boolValue.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.Float:
						{
							result = property.floatValue.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.String:
						{
							result = property.stringValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.Color:
						{
							result = property.colorValue.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.ObjectReference:
						{
							if (property.objectReferenceValue != null)
							{
								result = property.objectReferenceValue.name;
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.LayerMask:
						{
							result = UnityEngine.LayerMask.LayerToName(property.intValue);
						}
						break;
					case UnityEditor.SerializedPropertyType.Enum:
						{
							result = property.enumDisplayNames[property.enumValueIndex];
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector2:
						{
							result = property.vector2Value.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3:
						{
							result = property.vector3Value.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector4:
						{
							result = property.vector4Value.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.Rect:
						{
							result = property.rectValue.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.ArraySize:
						{
							result = property.arraySize.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.Character:
						{
							result = property.stringValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.AnimationCurve:
						{
							result = property.animationCurveValue.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.Bounds:
						{
							result = property.boundsValue.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.Gradient:
						{
							result = "Not Support";
						}
						break;
					case UnityEditor.SerializedPropertyType.Quaternion:
						{
							result = property.quaternionValue.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.ExposedReference:
						{
							if (property.objectReferenceValue != null)
							{
								result = property.objectReferenceValue.name;
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.FixedBufferSize:
						break;
					case UnityEditor.SerializedPropertyType.Vector2Int:
						{
							result = property.vector2IntValue.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3Int:
						{
							result = property.vector3IntValue.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.RectInt:
						{
							result = property.rectIntValue.ToString();
						}
						break;
					case UnityEditor.SerializedPropertyType.BoundsInt:
						{
							result = property.boundsIntValue.ToString();
						}
						break;
					default:
						break;
				}

				return (result);
			}

			
			/// <summary>
			/// Получение списка дочерних свойств
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Список дочерних свойств</returns>
			
			public static List<UnityEditor.SerializedProperty> GetChildrenProperties(this UnityEditor.SerializedProperty property)
			{
				var prop = property.Copy();
				var depth = prop.depth;
				var result = new List<UnityEditor.SerializedProperty>();

				var enter = true;
				while (prop.NextVisible(enter) && prop.depth > depth)
				{
					result.Add(prop.Copy());
					enter = false;
				}

				return (result);
			}

			
			/// <summary>
			/// Сохранение сериализуемого свойства
			/// </summary>
			/// <remarks>
			/// Эта старая методика для информирования об изменении данных - информирование об этом сцены для возможности 
			/// сохранить данные на диск
			/// </remarks>
			/// <param name="property">Сериализируемое свойство</param>
			
			public static void Save(this UnityEditor.SerializedProperty property)
			{
				UnityEditor.EditorUtility.SetDirty(property.serializedObject.targetObject);
				if (UnityEditor.EditorApplication.isPlaying == false)
				{
					var component = property.serializedObject.targetObject as UnityEngine.MonoBehaviour;
					if (component != null)
					{
						UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(component.gameObject.scene);
					}
				}
			}
			#endregion

			#region ======================================= ВЫЗОВ МЕТОДОВ =============================================
			
			/// <summary>
			/// Вызов метода из родительского свойства или текущего свойства
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <param name="method_name">Имя метода</param>
			/// <returns>Результат выполнения метода</returns>
			
			public static System.Object Invoke(this UnityEditor.SerializedProperty property, String method_name)
			{
				var parent = property.GetParentValue<System.Object>();
				if (parent != null)
				{
					if (XReflection.ContainsMethod(parent, method_name))
					{
						return (XReflection.InvokeMethod(parent, method_name));
					}
				}

				var value = property.GetValue<System.Object>();
				if (XReflection.ContainsMethod(value, method_name))
				{
					return (XReflection.InvokeMethod(value, method_name));
				}

				return (null);
			}

			
			/// <summary>
			/// Вызов метода из родительского свойства или текущего свойства
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <param name="method_name">Имя метода</param>
			/// <param name="arg">Аргумент метода</param>
			/// <returns>Результат выполнения метода</returns>
			
			public static System.Object Invoke(this UnityEditor.SerializedProperty property, String method_name, System.Object arg)
			{
				var parent = property.GetParentValue<System.Object>();
				if (parent != null)
				{
					if (XReflection.ContainsMethod(parent, method_name))
					{
						return (XReflection.InvokeMethod(parent, method_name, arg));
					}
				}

				var value = property.GetValue<System.Object>();
				if (XReflection.ContainsMethod(value, method_name))
				{
					return (XReflection.InvokeMethod(value, method_name, arg));
				}

				return (null);
			}

			
			/// <summary>
			/// Вызов метода из родительского свойства или текущего свойства
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <param name="method_name">Имя метода</param>
			/// <param name="arg1">Первый аргумент метода</param>
			/// <param name="arg2">Второй аргумент метода</param>
			/// <returns>Результат выполнения метода</returns>
			
			public static System.Object Invoke(this UnityEditor.SerializedProperty property, String method_name,
				System.Object arg1, System.Object arg2)
			{
				var parent = property.GetParentValue<System.Object>();
				if (parent != null)
				{
					if (XReflection.ContainsMethod(parent, method_name))
					{
						return (XReflection.InvokeMethod(parent, method_name, arg1, arg2));
					}
				}

				var value = property.GetValue<System.Object>();
				if (XReflection.ContainsMethod(value, method_name))
				{
					return (XReflection.InvokeMethod(value, method_name, arg1, arg2));
				}

				return (null);
			}
			#endregion

			#region ======================================= РАБОТА СО ЗНАЧЕНИЕМ СВОЙСТВА ==============================
			
			/// <summary>
			/// Получение значения сериализуемого свойства
			/// </summary>
			/// <remarks>
			/// http://answers.unity3d.com/questions/425012/get-the-instance-the-serializedproperty-belongs-to.html
			/// </remarks>
			/// <typeparam name="TValue">Тип значения свойства</typeparam>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Значение сериализуемого свойства</returns>
			
			public static TValue GetValue<TValue>(this UnityEditor.SerializedProperty property)
			{
				System.Object instance = property.serializedObject.targetObject;

				var path = property.propertyPath.Replace(NAME_SUFFIX_ARRAY, "[");
				var elements = path.Split('.');

				for (var i = 0; i < elements.Length; i++)
				{
					var element = elements[i];

					if (element.Contains("["))
					{
						var element_name = element.Substring(0, element.IndexOf("["));
						var index = element.ExtractNumberLast();
						instance = XReflection.GetFieldValue(instance, element_name, index);
					}
					else
					{
						instance = XReflection.GetFieldValue(instance, element);
					}
				}

				if (instance is TValue)
				{
					return ((TValue)instance);
				}

				return (default(TValue));
			}

			
			/// <summary>
			/// Получение значения сериализуемого свойства напрямую без использования рефлексии
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Значение сериализуемого свойства</returns>
			
			public static System.Object GetValueDirect(this UnityEditor.SerializedProperty property)
			{
				System.Object result = null;
				switch (property.propertyType)
				{
					case UnityEditor.SerializedPropertyType.Generic:
						{

						}
						break;
					case UnityEditor.SerializedPropertyType.Integer:
						{
							if (property.type == "long")
							{
								result = property.longValue;
							}
							else
							{
								result = property.intValue;
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.Boolean:
						{
							result = property.boolValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.Float:
						{
							if (property.type == "double")
							{
								result = property.doubleValue;
							}
							else
							{
								result = property.floatValue;
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.String:
						{
							result = property.stringValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.Color:
						{
							result = property.colorValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.ObjectReference:
						{
							if (property.objectReferenceValue != null)
							{
								result = property.objectReferenceValue;
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.LayerMask:
						{
							result = property.intValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.Enum:
						{
							result = property.enumValueIndex;
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector2:
						{
							result = property.vector2Value;
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3:
						{
							result = property.vector3Value;
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector4:
						{
							result = property.vector4Value;
						}
						break;
					case UnityEditor.SerializedPropertyType.Rect:
						{
							result = property.rectValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.ArraySize:
						{
							result = property.arraySize;
						}
						break;
					case UnityEditor.SerializedPropertyType.Character:
						{
							result = property.stringValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.AnimationCurve:
						{
							result = property.animationCurveValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.Bounds:
						{
							result = property.boundsValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.Gradient:
						{
							result = "Not Support";
						}
						break;
					case UnityEditor.SerializedPropertyType.Quaternion:
						{
							result = property.quaternionValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.ExposedReference:
						{
							if (property.objectReferenceValue != null)
							{
								result = property.objectReferenceValue;
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.FixedBufferSize:
						break;
					case UnityEditor.SerializedPropertyType.Vector2Int:
						{
							result = property.vector2IntValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3Int:
						{
							result = property.vector3IntValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.RectInt:
						{
							result = property.rectIntValue;
						}
						break;
					case UnityEditor.SerializedPropertyType.BoundsInt:
						{
							result = property.boundsIntValue;
						}
						break;
					default:
						break;
				}
				return (result);
			}

			
			/// <summary>
			/// Установка значения сериализуемого свойства
			/// </summary>
			/// <typeparam name="TValue">Тип значения свойства</typeparam>
			/// <param name="property">Сериализируемое свойство</param>
			/// <param name="value">Значение сериализуемого свойства</param>
			
			public static void SetValue<TValue>(this UnityEditor.SerializedProperty property, TValue value)
			{
				System.Object instance = property.serializedObject.targetObject;
				var path = property.propertyPath.Replace(NAME_SUFFIX_ARRAY, "[");

				var elements = path.Split('.');
				foreach (var element in elements.Take(elements.Length - 1))
				{
					if (element.Contains("["))
					{
						var element_name = element.Substring(0, element.IndexOf("["));
						var index = element.ExtractNumberLast();
						instance = XReflection.GetFieldValue(instance, element_name, index);
					}
					else
					{
						instance = XReflection.GetFieldValue(instance, element);
					}
				}

				if (System.Object.ReferenceEquals(instance, null)) return;

				try
				{
					var element = elements.Last();

					if (element.Contains("["))
					{
						var element_name = element.Substring(0, element.IndexOf("["));
						var index = element.ExtractNumberLast();
						XReflection.SetFieldValue(instance, element_name, value, index);
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
			
			public static void SetValueDirect(this UnityEditor.SerializedProperty property, System.Object value)
			{
				property.serializedObject.Update();
				switch (property.propertyType)
				{
					case UnityEditor.SerializedPropertyType.Generic:
						break;
					case UnityEditor.SerializedPropertyType.Integer:
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
					case UnityEditor.SerializedPropertyType.Boolean:
						{
							property.boolValue = Convert.ToBoolean(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.Float:
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
					case UnityEditor.SerializedPropertyType.String:
						{
							property.stringValue = Convert.ToString(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.Color:
						{
							property.colorValue = XUnityColor.ToColor(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.ObjectReference:
						{
							if (value is UnityEngine.Object)
							{
								property.objectReferenceValue = (UnityEngine.Object)value;
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.LayerMask:
						{
							property.intValue = XUnityLayerMask.ToLayerMask(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.Enum:
						{
							property.SetEnumValue(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector2:
						{
							property.vector2Value = XUnityVector2.ToVector(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3:
						{
							property.vector3Value = XUnityVector3.ToVector(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector4:
						{
							property.vector4Value = XUnityVector4.ToVector(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.Rect:
						{
							property.rectValue = XUnityRect.ToRect(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.ArraySize:
						break;
					case UnityEditor.SerializedPropertyType.Character:
						break;
					case UnityEditor.SerializedPropertyType.AnimationCurve:
						{
							if (value is UnityEngine.AnimationCurve)
							{
								property.animationCurveValue = (UnityEngine.AnimationCurve)value;
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.Bounds:
						{
							property.boundsValue = XUnityBounds.ToBounds(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.Gradient:
						break;
					case UnityEditor.SerializedPropertyType.Quaternion:
						{
							property.quaternionValue = XUnityQuaternion.ToQuaternion(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.ExposedReference:
						{
							property.exposedReferenceValue = (UnityEngine.Object)value;
						}
						break;
					case UnityEditor.SerializedPropertyType.FixedBufferSize:
						break;
					case UnityEditor.SerializedPropertyType.Vector2Int:
						{
							property.vector2IntValue = XUnityVector2Int.ToVector(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3Int:
						{
							property.vector3IntValue = XUnityVector3Int.ToVector(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.RectInt:
						{
							property.rectIntValue = XUnityRectInt.ToRect(value);
						}
						break;
					case UnityEditor.SerializedPropertyType.BoundsInt:
						{
							property.boundsIntValue = XUnityBoundsInt.ToBounds(value);
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
			/// <param name="max_value">Максимальное значение</param>
			
			public static void SetValueDirectMaximum(this UnityEditor.SerializedProperty property, System.Object max_value)
			{
				property.serializedObject.Update();
				switch (property.propertyType)
				{
					case UnityEditor.SerializedPropertyType.Generic:
						break;
					case UnityEditor.SerializedPropertyType.Integer:
						{
							if (property.type == "long")
							{
								var max = Convert.ToInt64(max_value);

								if (property.longValue > max)
								{
									property.longValue = max;
								}
							}
							else
							{
								var max = Convert.ToInt32(max_value);

								if (property.intValue > max)
								{
									property.intValue = max;
								}
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.Boolean:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Float:
						{
							if (property.type == "double")
							{
								var max = Convert.ToDouble(max_value);

								if (property.doubleValue > max)
								{
									property.doubleValue = max;
								}
							}
							else
							{
								var max = Convert.ToSingle(max_value);

								if (property.floatValue > max)
								{
									property.floatValue = max;
								}
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.String:
						break;
					case UnityEditor.SerializedPropertyType.Color:
						{

						}
						break;
					case UnityEditor.SerializedPropertyType.ObjectReference:
						break;
					case UnityEditor.SerializedPropertyType.LayerMask:
						break;
					case UnityEditor.SerializedPropertyType.Enum:
						break;
					case UnityEditor.SerializedPropertyType.Vector2:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector4:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Rect:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.ArraySize:
						break;
					case UnityEditor.SerializedPropertyType.Character:
						break;
					case UnityEditor.SerializedPropertyType.AnimationCurve:
						break;
					case UnityEditor.SerializedPropertyType.Bounds:
						break;
					case UnityEditor.SerializedPropertyType.Gradient:
						break;
					case UnityEditor.SerializedPropertyType.Quaternion:
						break;
					case UnityEditor.SerializedPropertyType.ExposedReference:
						break;
					case UnityEditor.SerializedPropertyType.FixedBufferSize:
						break;
					case UnityEditor.SerializedPropertyType.Vector2Int:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3Int:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.RectInt:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.BoundsInt:
						{
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
			/// <param name="min_value">Максимальное значение</param>
			
			public static void SetValueDirectMinimum(this UnityEditor.SerializedProperty property, System.Object min_value)
			{
				property.serializedObject.Update();
				switch (property.propertyType)
				{
					case UnityEditor.SerializedPropertyType.Generic:
						break;
					case UnityEditor.SerializedPropertyType.Integer:
						{
							if (property.type == "long")
							{
								var min = Convert.ToInt64(min_value);

								if (property.longValue < min)
								{
									property.longValue = min;
								}
							}
							else
							{
								var min = Convert.ToInt32(min_value);

								if (property.intValue < min)
								{
									property.intValue = min;
								}
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.Boolean:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Float:
						{
							if (property.type == "double")
							{
								var min = Convert.ToDouble(min_value);

								if (property.doubleValue < min)
								{
									property.doubleValue = min;
								}
							}
							else
							{
								var min = Convert.ToSingle(min_value);

								if (property.floatValue < min)
								{
									property.floatValue = min;
								}
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.String:
						break;
					case UnityEditor.SerializedPropertyType.Color:
						{

						}
						break;
					case UnityEditor.SerializedPropertyType.ObjectReference:
						break;
					case UnityEditor.SerializedPropertyType.LayerMask:
						break;
					case UnityEditor.SerializedPropertyType.Enum:
						break;
					case UnityEditor.SerializedPropertyType.Vector2:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector4:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Rect:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.ArraySize:
						break;
					case UnityEditor.SerializedPropertyType.Character:
						break;
					case UnityEditor.SerializedPropertyType.AnimationCurve:
						break;
					case UnityEditor.SerializedPropertyType.Bounds:
						break;
					case UnityEditor.SerializedPropertyType.Gradient:
						break;
					case UnityEditor.SerializedPropertyType.Quaternion:
						break;
					case UnityEditor.SerializedPropertyType.ExposedReference:
						break;
					case UnityEditor.SerializedPropertyType.FixedBufferSize:
						break;
					case UnityEditor.SerializedPropertyType.Vector2Int:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3Int:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.RectInt:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.BoundsInt:
						{
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
			/// <param name="delta_value">Шаг приращения</param>
			
			public static void SetValueDirectAdd(this UnityEditor.SerializedProperty property, System.Object delta_value)
			{
				property.serializedObject.Update();
				switch (property.propertyType)
				{
					case UnityEditor.SerializedPropertyType.Generic:
						break;
					case UnityEditor.SerializedPropertyType.Integer:
						{
							if (property.type == "long")
							{
								var delta = Convert.ToInt64(delta_value);
								property.longValue += delta;
							}
							else
							{
								var delta = Convert.ToInt32(delta_value);
								property.intValue += delta;
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.Boolean:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Float:
						{
							if (property.type == "double")
							{
								Double delta = Convert.ToSingle(delta_value);
								property.doubleValue += delta;
							}
							else
							{
								var delta = Convert.ToSingle(delta_value);
								property.floatValue += delta;
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.String:
						break;
					case UnityEditor.SerializedPropertyType.Color:
						{

						}
						break;
					case UnityEditor.SerializedPropertyType.ObjectReference:
						break;
					case UnityEditor.SerializedPropertyType.LayerMask:
						break;
					case UnityEditor.SerializedPropertyType.Enum:
						break;
					case UnityEditor.SerializedPropertyType.Vector2:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector4:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Rect:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.ArraySize:
						break;
					case UnityEditor.SerializedPropertyType.Character:
						break;
					case UnityEditor.SerializedPropertyType.AnimationCurve:
						break;
					case UnityEditor.SerializedPropertyType.Bounds:
						break;
					case UnityEditor.SerializedPropertyType.Gradient:
						break;
					case UnityEditor.SerializedPropertyType.Quaternion:
						break;
					case UnityEditor.SerializedPropertyType.ExposedReference:
						break;
					case UnityEditor.SerializedPropertyType.FixedBufferSize:
						break;
					case UnityEditor.SerializedPropertyType.Vector2Int:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3Int:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.RectInt:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.BoundsInt:
						{
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
			/// <param name="delta_value">Шаг приращения</param>
			
			public static void SetValueDirectSubstract(this UnityEditor.SerializedProperty property, System.Object delta_value)
			{
				property.serializedObject.Update();
				switch (property.propertyType)
				{
					case UnityEditor.SerializedPropertyType.Generic:
						break;
					case UnityEditor.SerializedPropertyType.Integer:
						{
							if (property.type == "long")
							{
								var delta = Convert.ToInt64(delta_value);
								property.longValue -= delta;
							}
							else
							{
								var delta = Convert.ToInt32(delta_value);
								property.intValue -= delta;
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.Boolean:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Float:
						{
							if (property.type == "double")
							{
								Double delta = Convert.ToSingle(delta_value);
								property.doubleValue -= delta;
							}
							else
							{
								var delta = Convert.ToSingle(delta_value);
								property.floatValue -= delta;
							}
						}
						break;
					case UnityEditor.SerializedPropertyType.String:
						break;
					case UnityEditor.SerializedPropertyType.Color:
						{

						}
						break;
					case UnityEditor.SerializedPropertyType.ObjectReference:
						break;
					case UnityEditor.SerializedPropertyType.LayerMask:
						break;
					case UnityEditor.SerializedPropertyType.Enum:
						break;
					case UnityEditor.SerializedPropertyType.Vector2:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector4:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Rect:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.ArraySize:
						break;
					case UnityEditor.SerializedPropertyType.Character:
						break;
					case UnityEditor.SerializedPropertyType.AnimationCurve:
						break;
					case UnityEditor.SerializedPropertyType.Bounds:
						break;
					case UnityEditor.SerializedPropertyType.Gradient:
						break;
					case UnityEditor.SerializedPropertyType.Quaternion:
						break;
					case UnityEditor.SerializedPropertyType.ExposedReference:
						break;
					case UnityEditor.SerializedPropertyType.FixedBufferSize:
						break;
					case UnityEditor.SerializedPropertyType.Vector2Int:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.Vector3Int:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.RectInt:
						{
						}
						break;
					case UnityEditor.SerializedPropertyType.BoundsInt:
						{
						}
						break;
					default:
						break;
				}
				property.serializedObject.ApplyModifiedProperties();
			}
			#endregion

			#region ======================================= РАБОТА С ПЕРЕЧИСЛЕНИЕМ ====================================
			
			/// <summary>
			/// Установить значение сериализуемого свойства указанного типа перечисления
			/// </summary>
			/// <typeparam name="TEnum">Тип перечисления</typeparam>
			/// <param name="property">Сериализируемое свойство</param>
			/// <param name="value">Значение</param>
			
			public static void SetEnumValue<TEnum>(this UnityEditor.SerializedProperty property, TEnum value) where TEnum : struct
			{
				if (property.propertyType == UnityEditor.SerializedPropertyType.Enum)
				{
					property.intValue = Convert.ToInt32(value);
				}
				else
				{
					UnityEngine.Debug.LogErrorFormat("SerializedProperty <{0}> is not an enumeration type", property.name);
				}
			}

			
			/// <summary>
			/// Установить значение сериализуемого свойства перечислением
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <param name="value">Значение</param>
			
			public static void SetEnumValue(this UnityEditor.SerializedProperty property, Enum value)
			{
				if (property.propertyType == UnityEditor.SerializedPropertyType.Enum)
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
					UnityEngine.Debug.LogErrorFormat("SerializedProperty <{0}> is not an enumeration type", property.name);
				}
			}

			
			/// <summary>
			/// Установить значение сериализуемого свойства базовым объектом
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <param name="value">Значение</param>
			
			public static void SetEnumValue(this UnityEditor.SerializedProperty property, Object value)
			{
				if (property.propertyType == UnityEditor.SerializedPropertyType.Enum)
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
					UnityEngine.Debug.LogErrorFormat("SerializedProperty <{0}> is not an enumeration type", property.name);
				}
			}

			
			/// <summary>
			/// Получить значение сериализуемого свойства указанного типа перечисления
			/// </summary>
			/// <typeparam name="TEnum">Тип перечисления</typeparam>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Значение</returns>
			
			public static TEnum GetEnumValue<TEnum>(this UnityEditor.SerializedProperty property) where TEnum : Enum
			{
				if (property.propertyType == UnityEditor.SerializedPropertyType.Enum)
				{
					try
					{
						return (XEnum.ToEnum<TEnum>(property.intValue));
					}
					catch
					{
						return (default(TEnum));
					}
				}
				else
				{
					UnityEngine.Debug.LogErrorFormat("SerializedProperty <{0}> is not an enumeration type", property.name);
					return (default(TEnum));
				}
			}

			
			/// <summary>
			/// Получить значение сериализуемого свойства указанного типа перечисления
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <param name="enum_type">Тип перечисления</param>
			/// <returns>Значение</returns>
			
			public static Enum GetEnumValue(this UnityEditor.SerializedProperty property, Type enum_type)
			{
				if (!enum_type.IsEnum)
				{
					UnityEngine.Debug.LogErrorFormat("Type <{0}> must be an enumerated type", enum_type.Name);
					return (null);
				}

				if (property.propertyType == UnityEditor.SerializedPropertyType.Enum)
				{
					try
					{
						return (XEnum.ToEnumOfType(enum_type, property.intValue));
					}
					catch
					{
						return (Enum.GetValues(enum_type).Cast<Enum>().First());
					}
				}
				else
				{
					UnityEngine.Debug.LogErrorFormat("SerializedProperty <{0}> is not an enumeration type", property.name);
					return (null);
				}
			}
			#endregion

			#region ======================================= РАБОТА С РОДИТЕЛЬСКИМ СВОЙСТВОМ ===========================
			
			/// <summary>
			/// Получение пути родительского свойства
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Путь к родительскому свойству</returns>
			
			public static String GetParentPath(this UnityEditor.SerializedProperty property)
			{
				var last_dot = property.propertyPath.LastIndexOf('.');
				if (last_dot == -1) // No parent property
				{
					return "";
				}

				return (property.propertyPath.Substring(0, last_dot));
			}

			
			/// <summary>
			/// Получение родительского свойства
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Родительское свойство</returns>
			
			public static UnityEditor.SerializedProperty GetParentProperty(this UnityEditor.SerializedProperty property)
			{
				var last_dot = property.propertyPath.LastIndexOf('.');
				if (last_dot == -1) // No parent property
				{
					// Возвращаем это свойство
					return (property);
				}
				else
				{
					var parent_path = property.propertyPath.Substring(0, last_dot);
					return (property.serializedObject.FindProperty(parent_path));
				}

			}

			
			/// <summary>
			/// Получение значения родительского свойства или непосредственно компонента сериализуемого свойства
			/// </summary>
			/// <typeparam name="TValue">Тип значения свойства</typeparam>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Значение родительского свойства сериализуемого свойства</returns>
			
			public static TValue GetParentValue<TValue>(this UnityEditor.SerializedProperty property)
			{
				System.Object instance = property.serializedObject.targetObject;

				var path = property.propertyPath.Replace(NAME_SUFFIX_ARRAY, "[");
				var elements = path.Split('.');

				foreach (var element in elements.Take(elements.Length - 1))
				{
					if (element.Contains("["))
					{
						var element_name = element.Substring(0, element.IndexOf("["));
						var index = element.ExtractNumberLast();
						instance = XReflection.GetFieldValue(instance, element_name, index);
					}
					else
					{
						instance = XReflection.GetFieldValue(instance, element);
					}
				}
				return ((TValue)instance);
			}
			#endregion

			#region ======================================= РАБОТА С СОХРАНЕНИЕМ/ЗАГРУЗКОЙ ЗНАЧЕНИЯ ===================
			
			/// <summary>
			/// Получение имени свойства привязанного к конкретному компоненту (игровому объекту)
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Уникальное имя</returns>
			
			public static String GetHashNameInstance(this UnityEditor.SerializedProperty property)
			{
				return (property.propertyPath + "_" + property.serializedObject.targetObject.GetInstanceID().ToString());
			}

			
			/// <summary>
			/// Получение имени свойства привязанного к типу
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Уникальное имя</returns>
			
			public static String GetHashNameType(this UnityEditor.SerializedProperty property)
			{
				var type_name = property.serializedObject.targetObject.GetType().FullName;
				if (property.propertyPath.IndexOf(NAME_SUFFIX_ARRAY) > -1)
				{
					var property_path = property.propertyPath.Replace(NAME_SUFFIX_ARRAY, "[");
					return (type_name + '.' + property_path.RemoveAllBetweenSymbolWithSymbols('[', ']'));
				}
				else
				{
					return (type_name + '.' + property.propertyPath);
				}
			}

			
			/// <summary>
			/// Сохранение значение по имени свойства привязанного к конкретному компоненту (игровому объекту)
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <param name="prefix">Префикс имени</param>
			/// <param name="value">Значение</param>
			
			public static void SaveBoolEditor(this UnityEditor.SerializedProperty property, String prefix, Boolean value)
			{
				UnityEditor.EditorPrefs.SetBool(property.GetHashNameInstance() + prefix, value);
			}

			
			/// <summary>
			/// Загрузка значение по имени свойства привязанного к конкретному компоненту (игровому объекту)
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <param name="prefix">Префикс имени</param>
			/// <param name="default_value">Значение по умолчанию</param>
			/// <returns>Значение</returns>
			
			public static Boolean LoadEditorBool(this UnityEditor.SerializedProperty property, String prefix, Boolean default_value = true)
			{
				return (UnityEditor.EditorPrefs.GetBool(property.GetHashNameInstance() + prefix, default_value));
			}
			#endregion

			#region ======================================= РАБОТА С КОЛЕКЦИЯМИ =======================================
			
			/// <summary>
			/// Проверка свойства на тип коллекции
			/// </summary>
			/// <remarks>
			/// К коллекциям относятся как стандартные коллекции так и коллекции системы Lotus
			/// </remarks>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Статус коллекции свойства</returns>
			
			public static Boolean IsCollection(this UnityEditor.SerializedProperty property)
			{
				if (property.isArray && property.propertyType != UnityEditor.SerializedPropertyType.String)
				{
					return (true);
				}
				else
				{
					if (property.propertyType == UnityEditor.SerializedPropertyType.Generic)
					{
						if (property.FindPropertyRelative(NAME_LOTUS_ARRAY) != null)
						{
							return (true);
						}
					}

					return (false);
				}
			}

			
			/// <summary>
			/// Проверка на элемент коллекции
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Статус элемент коллекции</returns>
			
			public static Boolean IsElementCollection(this UnityEditor.SerializedProperty property)
			{
				if (property.propertyPath[property.propertyPath.Length - 1] == ']')
				{
					return (true);
				}

				return (false);
			}

			
			/// <summary>
			/// Проверка свойства на тип стандартной коллекции: Array или List
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Статус коллекции свойства</returns>
			
			public static Boolean IsStandardCollection(this UnityEditor.SerializedProperty property)
			{
				if (property.isArray && property.propertyType != UnityEditor.SerializedPropertyType.String)
				{
					return (true);
				}
				else
				{
					return (false);
				}
			}

			
			/// <summary>
			/// Проверка свойства на тип коллекции Lotus: производные коллекции от <see cref="ListArray{TItem}"/>
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Статус коллекции свойства</returns>
			
			public static Boolean IsLotusCollection(this UnityEditor.SerializedProperty property)
			{
				if (property.propertyType == UnityEditor.SerializedPropertyType.Generic)
				{
					if (property.FindPropertyRelative(NAME_LOTUS_ARRAY) != null)
					{
						return (true);
					}
				}

				return (false);
			}

			
			/// <summary>
			/// Получение сериализуемого свойства внутреннего массива данных от коллекции Lotus
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Сериализируемое свойство внутреннего массива данных</returns>
			
			public static UnityEditor.SerializedProperty GetLotusInnerCollection(this UnityEditor.SerializedProperty property)
			{
				return (property.FindPropertyRelative(NAME_LOTUS_ARRAY));
			}
			#endregion

			#region ======================================= МЕТОДЫ ПОЛУЧЕНИЕ АТРИБУТОВ ================================
			
			/// <summary>
			/// Получение атрибута
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Атрибут</returns>
			
			public static TAttribute GetAttribute<TAttribute>(this UnityEditor.SerializedProperty property) where TAttribute : Attribute
			{
				//
				// Кэшированная реализация
				//
				return (XEditorCachedData.GetMember(property).GetAttribute<TAttribute>());

				//
				// Старая реализация
				//
				//FieldInfo field_info = property.GetFieldInfo();
				//if(field_info != null)
				//{
				//	return (Attribute.GetCustomAttribute(field_info, typeof(TAttribute)) as TAttribute);
				//}

				//return (null);
			}
			#endregion
		}

		/**@}*/

	}
}
