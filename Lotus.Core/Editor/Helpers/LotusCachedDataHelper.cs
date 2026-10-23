
// Проект: Модуль базового ядра Unity
// Раздел: Подсистема расширения функциональности редактора Unity
// Автор: MagistrBYTE aka DanielDem <dementevds@gmail.com>
------------
/** \file LotusCachedDataHelper.cs
*		Кэширование данные для инспектора свойств.
*/
------------
// Версия: 1.0.0.0
// Последнее изменение от 30.04.2023

using System;
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
		/// Класс для хранения метаданных члена объекта и связанных с ним атрибутов
		/// </summary>

		public class CCachedMember
		{
			#region Fields
			/// <summary>
			/// Полный путь
			/// </summary>
			public String FullPath;

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
#if UNITY_EDITOR
			
			/// <summary>
			/// Конструктор инициализирует объект класса указанными параметрами
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			
			public CCachedMember(UnityEditor.SerializedProperty property)
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
			/// <param name="member_info">Метаданные члена объекта</param>
			
			public CCachedMember(MemberInfo member_info)
			{
				FullPath = member_info.GetMemberPath();
				Member = member_info;
				if (Member != null)
				{
					Attributes = Member.GetAttributes<Attribute>();
				}
			}
#endif
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
						if (Attributes[i] is TAttribute)
						{
							return (Attributes[i] as TAttribute);
						}
					}
				}

				return (null);
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
			private static Dictionary<String, CCachedMember> mMembers = new Dictionary<String, CCachedMember>();
			private static Dictionary<String, UnityEditor.Editor> mEditors = new Dictionary<String, UnityEditor.Editor>();
			#endregion

			#region Properties
			/// <summary>
			/// Словарь кэшированных метаданных членов объекта
			/// </summary>
			public static Dictionary<String, CCachedMember> Members
			{
				get
				{
					if (mMembers == null)
					{
						mMembers = new Dictionary<String, CCachedMember>();
					}

					return (mMembers);
				}
			}

			/// <summary>
			/// Словарь кэшированных редакторов объектов
			/// </summary>
			public static Dictionary<String, UnityEditor.Editor> Editors
			{
				get
				{
					if (mEditors == null)
					{
						mEditors = new Dictionary<String, UnityEditor.Editor>();
					}

					return (mEditors);
				}
			}
			#endregion

			#region Main methods
			
			/// <summary>
			/// Получить кэшированные метаданные для указанного свойства
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Кэширование метаданные</returns>
			
			public static CCachedMember GetMember(UnityEditor.SerializedProperty property)
			{
				var key = property.GetHashNameType();
				CCachedMember result = null;
				if (Members.TryGetValue(key, out result))
				{
					return (result);
				}

				result = new CCachedMember(property);
				Members.Add(key, result);
				return (result);
			}

			
			/// <summary>
			/// Получить кэшированные метаданные для указанного члена объекта
			/// </summary>
			/// <param name="member_info">Метаданные члена объекта</param>
			/// <returns>Кэширование метаданные</returns>
			
			public static CCachedMember GetMember(MemberInfo member_info)
			{
				var key = member_info.GetMemberPath();
				CCachedMember result = null;
				if (Members.TryGetValue(key, out result))
				{
					return (result);
				}

				result = new CCachedMember(member_info);
				Members.Add(key, result);
				return (result);
			}

			
			/// <summary>
			/// Получение кэшированного редактора объекта для указанного свойства
			/// </summary>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Редактор объекта</returns>
			
			public static UnityEditor.Editor GetEditor(UnityEditor.SerializedProperty property)
			{
				var key = property.GetHashNameInstance();
				UnityEditor.Editor result = null;
				if (Editors.TryGetValue(key, out result))
				{
					// Только если ссылаемся на один и тот же объект
					if (result.target == property.objectReferenceValue)
					{
						return (result);
					}
					else
					{
						// Удаляем
						UnityEngine.Object.DestroyImmediate(result);
						Editors.Remove(key);

						// Создаем новый
						result = UnityEditor.Editor.CreateEditor(property.objectReferenceValue);
						Editors.Add(key, result);
						return (result);
					}
				}

				result = UnityEditor.Editor.CreateEditor(property.objectReferenceValue);
				Editors.Add(key, result);
				return (result);
			}

			
			/// <summary>
			/// Принудительная очистка(удаление) редакторов объектов
			/// </summary>
			
			public static void ClearEditors()
			{
				if (mEditors != null && mEditors.Count > 0)
				{
					foreach (var item in Editors.Values)
					{
						if (item != null)
						{
							UnityEngine.Object.DestroyImmediate(item);
						}
					}

					mEditors.Clear();
				}
			}

			
			/// <summary>
			/// Получение атрибута
			/// </summary>
			/// <typeparam name="TAttribute">Тип атрибута</typeparam>
			/// <param name="property">Сериализируемое свойство</param>
			/// <returns>Атрибут</returns>
			
			public static TAttribute GetAttribute<TAttribute>(UnityEditor.SerializedProperty property) where TAttribute : Attribute
			{
				CCachedMember result = GetMember(property);

				return (result.GetAttribute<TAttribute>());
			}
			#endregion
		}

		/**@}*/

	}
}
